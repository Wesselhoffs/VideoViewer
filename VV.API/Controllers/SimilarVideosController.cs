using System.Data;

namespace VV.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SimilarVideosController : ControllerBase
{
	private readonly IDbService _db;
	public SimilarVideosController(IDbService dbService)
	{
		_db = dbService;
	}
	[HttpGet]
	public async Task<IResult> Get()
	{
		try
		{
			var simVids = await _db.GetAsync<SimilarVideo, SimilarVideoDTO>();
			if (simVids is null)
			{
				return Results.NotFound();
			}
			return Results.Ok(simVids);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}
	[HttpGet("{videoId}")]
	public async Task<IResult> Get(int videoId, bool freeOnly = false)
	{
		try
		{
			await _db.Include<Video>();
			await _db.Include<VideoGenre>();
			var videos = freeOnly ? await _db.GetAsync<Video, VideoDTO>(c => c.Free.Equals(freeOnly))
												: await _db.GetAsync<Video, VideoDTO>();

			var simVidQuery = from vid in videos
							  from sim in vid.SimilarVideos
							  where sim.VideoId == videoId || sim.SimilarVideoId == videoId
							  select sim;

			var resultVideos = new List<VideoDTO>();
			foreach (var simVideo in simVidQuery)
			{
				resultVideos.AddRange(videos.Where(v => v.Id.Equals(simVideo.VideoId)));
				resultVideos.AddRange(videos.Where(v => v.Id.Equals(simVideo.SimilarVideoId)));
			}

			resultVideos.RemoveAll(v => v.Id.Equals(videoId));
			return Results.Ok(resultVideos.ToHashSet().ToList());
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IResult> Post([FromBody] SimilarVideoDTO dto)
	{
		try
		{
			if (dto is null)
			{
				return Results.BadRequest();
			}
			var similar = await _db.AddAsync<SimilarVideo, SimilarVideoDTO>(dto);
			var success = await _db.SaveChangesAsync();
			if (success is false)
			{
				return Results.BadRequest("Could not add the similar video connection.");
			}
			return Results.Ok();
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpDelete]
	public async Task<IResult> Delete([FromBody] SimilarVideoDTO dto)
	{
		try
		{
			var success = _db.Delete<SimilarVideo, SimilarVideoDTO>(dto);
			if (success is false)
			{
				return Results.NotFound("Could not find matching Entity");
			}
			success = await _db.SaveChangesAsync();
			if (success is false)
			{
				return Results.BadRequest();
			}
			return Results.NoContent();
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}
}
