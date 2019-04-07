using System.Threading.Tasks;
using FESTAgencyChallange.DataTransferObjects.GoogleTimeZoneResult;

namespace FESTAgencyChallange.Services.Abstractions
{
    public interface ITimeZoneService
    {
        Task<TimeZoneResult> GetTimeZone(double latitude, double longitude, int timestamp);
    }
}