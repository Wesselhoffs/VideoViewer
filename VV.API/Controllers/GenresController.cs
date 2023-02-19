﻿namespace VV.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
	private readonly IDbService _db;

	public GenresController(IDbService dbService)
	{
		_db = dbService;
	}
	[HttpGet]
	public async Task<IResult> Get(bool getFull = false)
	{
		try
		{
			if (getFull)
			{
				await _db.Include<Genre>();
				await _db.Include<VideoGenre>();
				var genreFull = await _db.GetAsync<Genre, GenreFullDTO>();
				if (genreFull is null)
				{
					return Results.NotFound();
				}
				return Results.Ok(genreFull);
			}
			var genre = await _db.GetAsync<Genre, GenreDTO>();
			if (genre is null)
			{
				return Results.NotFound();
			}
			return Results.Ok(genre);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}")]
	public async Task<IResult> Get(int id, bool getFull = false)
	{
		try
		{
			if (getFull)
			{
				await _db.Include<Genre>();
				await _db.Include<VideoGenre>();
				var genreFull = await _db.SingleAsync<Genre, GenreFullDTO>(v => v.Id == id);
				if (genreFull is null)
				{
					return Results.NotFound("No matching id found.");
				}
				return Results.Ok(genreFull);
			}
			var genre = await _db.SingleAsync<Genre, GenreDTO>(v => v.Id == id);
			if (genre is null)
			{
				return Results.NotFound("No matching id found.");
			}
			return Results.Ok(genre);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IResult> Post([FromBody] GenreCreateDTO dto)
	{
		try
		{
			if (dto is null)
			{
				return Results.BadRequest();
			}
			var genre = await _db.AddAsync<Genre, GenreCreateDTO>(dto);
			var success = await _db.SaveChangesAsync();
			if (success is false)
			{
				return Results.BadRequest();
			}
			return Results.Created(_db.GetURI(genre), genre);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	[HttpPut("{id}")]
	public async Task<IResult> Put(int id, [FromBody] GenreDTO dto)
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
			var exists = await _db.AnyAsync<Genre>(v => v.Id.Equals(id));
			if (exists is false)
			{
				return Results.NotFound("Could not find an Entity with id: " + id);
			}
			_db.Update<Genre, GenreDTO>(dto, dto.Id);
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
			var success = await _db.DeleteAsync<Genre>(id);
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
