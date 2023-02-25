namespace VV.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoGenresController : ControllerBase
{
	private readonly IDbService _db;

	public VideoGenresController(IDbService dbService)
	{
		_db = dbService;
	}

	[HttpGet]
	public async Task<IResult> Get()
	{
		try
		{
			var videoGenres = await _db.GetAsync<VideoGenre, VideoGenreDTO>();
			if (videoGenres is null)
			{
				return Results.NotFound();
			}
			return Results.Ok(videoGenres);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IResult> Post([FromBody] VideoGenreDTO dto)
	{
		try
		{
			if (dto is null)
			{
				return Results.BadRequest();
			}
			var similar = await _db.AddAsync<VideoGenre, VideoGenreDTO>(dto);
			var success = await _db.SaveChangesAsync();
			if (success is false)
			{
				return Results.BadRequest("Could not add the genre connection.");
			}
			return Results.Ok();
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpDelete]
	public async Task<IResult> Delete([FromBody] VideoGenreDTO dto)
	{
		try
		{
			var success = _db.Delete<VideoGenre, VideoGenreDTO>(dto);
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
