using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FESTAgencyChallange.DataTransferObjects.GoogleTimeZoneResult;
using FESTAgencyChallange.Helpers;
using FESTAgencyChallange.Services.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FESTAgencyChallange.Services
{
    public class GoogleTimeZoneService : ITimeZoneService
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleTimeZoneSettings _googleTimeZoneSettings;

        public GoogleTimeZoneService(IOptions<GoogleTimeZoneSettings> googleTimeZoneSettings)
        {
            this._googleTimeZoneSettings = googleTimeZoneSettings.Value;
            this._httpClient = new HttpClient
            {
                BaseAddress = new Uri(this._googleTimeZoneSettings.ApiEndpoint),
                Timeout = TimeSpan.FromSeconds(45)
            };
        }

        public async Task<TimeZoneResult> GetTimeZone(double latitude, double longitude, int timestamp)
        {
            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };
            string invariantLatitude = latitude.ToString(nfi);
            string invariantLongitude = longitude.ToString(nfi);
            var response = await this._httpClient.GetAsync($"json?location={invariantLatitude},{invariantLongitude}" +
                                                           $"&timestamp={timestamp}" +
                                                           $"&key={this._googleTimeZoneSettings.ApiKey}");
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TimeZoneResult>(data);
        }
    }
}
