namespace VV.Common.Services
{
    public interface IUserService
    {
        Task<List<TDto>> GetAsync<TDto>(string uri);
        Task<TDto>? SingleAsync<TDto>(string uri);
    }
}