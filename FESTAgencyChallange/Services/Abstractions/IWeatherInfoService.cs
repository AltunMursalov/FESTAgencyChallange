using System.Threading.Tasks;

namespace FESTAgencyChallange.Services.Abstractions
{
    public interface IWeatherInfoService
    {
        /// <summary>
        /// Getting weather info by zip code.
        /// </summary>
        /// <param name="zipCode">Zip code of postal office for identify location.</param>
        /// <param name="isMetric">Specify that data will be in metric measure.</param>
        /// <returns></returns>
        Task<DataTransferObjects.WeatherInfoResult.RootObject> GetInfoByZipCode(string zipCode, bool isMetric);
    }
}