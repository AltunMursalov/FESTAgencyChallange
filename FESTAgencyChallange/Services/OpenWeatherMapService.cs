using System;
using System.Net.Http;
using System.Threading.Tasks;
using FESTAgencyChallange.Helpers;
using FESTAgencyChallange.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FESTAgencyChallange.Services
{
    /// <summary>
    /// Implementation of IWeatherInfoService. For more information visit: https://openweathermap.org/api && https://openweathermap.org/current
    /// </summary>
    public class OpenWeatherMapService : IWeatherInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherMapSettings _openWeatherMapSettings;

        public OpenWeatherMapService(IOptions<OpenWeatherMapSettings> openWeatherMapOptions)
        {
            this._openWeatherMapSettings = openWeatherMapOptions.Value;
            this._httpClient = new HttpClient
            {
                BaseAddress = new Uri(this._openWeatherMapSettings.ApiEndpoint),
                Timeout = TimeSpan.FromSeconds(45)
            };
        }

        public async Task<DataTransferObjects.WeatherInfoResult.RootObject> GetInfoByZipCode(string zipCode, bool isMetric)
        {
            var url = $"weather?zip={zipCode}&APPID={this._openWeatherMapSettings.ApiKey}";
            if (isMetric)
                url += $"&units=metric";
            var response = await this._httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DataTransferObjects.WeatherInfoResult.RootObject>(data);
            }
            else
            {
                return null;
            }
        }
    }
}