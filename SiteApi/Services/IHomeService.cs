using SiteApi.DTOs;

namespace SiteApi.Services
{
    public interface IHomeService
    {
        Task<HomeDto> GetHomeDataAsync();
    }
}
