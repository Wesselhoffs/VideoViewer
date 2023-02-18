namespace VV.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase
{
	private readonly IDbService _db;

	public VideosController(IDbService dbService)
	{
		_db = dbService;
	}
	[HttpGet]
	public async Task<IResult> Get(bool freeOnly)
	{
		try
		{
			await _db.Include<Video>();
			await _db.Include<VideoGenre>();
			List<VideoDTO>? videos = freeOnly ? await _db.GetAsync<Video, VideoDTO>(c => c.Free.Equals(freeOnly))
												: await _db.GetAsync<Video, VideoDTO>();
			return Results.Ok(videos);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}")]
	public async Task<IResult> Get(int id)
	{
		try
		{
			await _db.Include<Video>();
			await _db.Include<VideoGenre>();
			var video = await _db.SingleAsync<Video, VideoDTO>(v => v.Id == id);
			return Results.Ok(video);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IResult> Post([FromBody] VideoCreateDTO dto)
	{
		try
		{
			if (dto is null)
			{
				return Results.BadRequest();
			}
			var video = await _db.AddAsync<Video, VideoCreateDTO>(dto);
			var success = await _db.SaveChangesAsync();
			if (success is false)
			{
				return Results.BadRequest();
			}
			return Results.Created(_db.GetURI(video), video);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpPut("{id}")]
	public async Task<IResult> Put(int id, [FromBody] VideoEditDTO dto)
	{
		try
		{
			if (dto is null)
			{
				return Results.BadRequest("No entity provided");
			}
			if (id.Equals(dto.Id) is false)
			{
				return Results.BadRequest($"URL Id: {id} is not equal to Entity Id: {dto.Id}");
			}
			var exists = await _db.AnyAsync<Video>(v => v.Id.Equals(id));
			if (exists is false)
			{
				return Results.NotFound("Could not find an Entity with id: " + id);
			}
			_db.Update<Video, VideoEditDTO>(dto, dto.Id);
			var success = await _db.SaveChangesAsync();
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

	[HttpDelete("{id}")]
	public async Task<IResult> Delete(int id)
	{
		try
		{
			await _db.Include<Video>();
			var success = await _db.DeleteAsync<Video>(id);
			if (success is false)
			{
				return Results.NotFound("Could not find an Entity with id: " + id);
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
