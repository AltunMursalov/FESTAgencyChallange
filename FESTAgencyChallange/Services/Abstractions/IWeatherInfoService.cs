using System.Threading.Tasks;

namespace FESTAgencyChallange.Services.Abstractions
{
    public interface IWeatherInfoService
    {
        Task<DataTransferObjects.WeatherInfoResult.RootObject> GetInfoByZipCode(string zipCode, bool isMetric);
    }
}