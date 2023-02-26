namespace VV.Common.Services;

public class UserService : IUserService
{
	public readonly VVHttpClient Http;

	public UserService(VVHttpClient httpClient)
	{
		Http = httpClient;
	}

	public async Task<List<TDto>> GetAsync<TDto>(string uri)
	{
		try
		{
			using var response = await Http.Client.GetAsync(uri);
			response.EnsureSuccessStatusCode();
			var dtoList = await response.Content.ReadFromJsonAsync<List<TDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return dtoList ?? new List<TDto>();
		}
		catch (Exception)
		{
			throw;
		}
	}
	public async Task<TDto>? SingleAsync<TDto>(string uri)
	{
		try
		{
			using var response = await Http.Client.GetAsync(uri);
			response.EnsureSuccessStatusCode();
			var dto = await response.Content.ReadFromJsonAsync<TDto>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return dto ?? default;
		}
		catch (Exception)
		{
			throw;
		}
	}

	
}
