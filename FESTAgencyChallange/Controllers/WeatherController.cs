using System;
using System.Globalization;
using System.Threading.Tasks;
using FESTAgencyChallange.DataTransferObjects.WeatherAndTimeZoneResult;
using FESTAgencyChallange.Helpers;
using FESTAgencyChallange.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FESTAgencyChallange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        #region Services
        private readonly IWeatherInfoService _weatherInfoService;
        private readonly ITimeZoneService _timeZoneService;
        #endregion

        public WeatherController(IWeatherInfoService weatherInfoService, ITimeZoneService timeZoneService)
        {
            this._weatherInfoService = weatherInfoService;
            this._timeZoneService = timeZoneService;
        }

        public async Task<IActionResult> Get(string zipCode, bool isMetric)
        {
            var weatherInfoResult = await this._weatherInfoService.GetInfoByZipCode(zipCode, isMetric);
            var unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var timeZoneResult = await this._timeZoneService.GetTimeZone(weatherInfoResult.coord.lat,
                                                                         weatherInfoResult.coord.lon, unixTimestamp);
            if (weatherInfoResult != null)
            {
                var time = DateTimeExtensions.GetDateTimeFromUnixTimeStamp(unixTimestamp + timeZoneResult.DstOffset +
                                                                            timeZoneResult.RawOffset);
                double gmt = timeZoneResult.RawOffset / 3600d;
                NumberFormatInfo nfi = new NumberFormatInfo
                {
                    NumberDecimalSeparator = ":"
                };
                return Ok(new WeatherTimeZoneResult
                {
                    City = weatherInfoResult.name,
                    Country = weatherInfoResult.sys.country,
                    Temperature = weatherInfoResult.main.temp.ToString(CultureInfo.InvariantCulture),
                    TimeZone = time.ToString("yyyy-MM-dd HH:mm:ss"),
                    GMT = gmt.ToString(nfi)
                });
            }

            return BadRequest();
        }
    }
}