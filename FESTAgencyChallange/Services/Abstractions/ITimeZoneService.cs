using System.Threading.Tasks;
using FESTAgencyChallange.DataTransferObjects.GoogleTimeZoneResult;

namespace FESTAgencyChallange.Services.Abstractions
{
    public interface ITimeZoneService
    {
        /// <summary>
        /// Getting time zone info of location.
        /// </summary>
        /// <param name="latitude">Location's latitude.</param>
        /// <param name="longitude">Location's longitude</param>
        /// <param name="timestamp">UNIX timestamp from 1.1.1970</param>
        /// <returns></returns>
        Task<TimeZoneResult> GetTimeZone(double latitude, double longitude, int timestamp);
    }
}