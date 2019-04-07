namespace FESTAgencyChallange.DataTransferObjects.GoogleTimeZoneResult
{
    public class TimeZoneResult
    {
        public int DstOffset { get; set; }
        public int RawOffset { get; set; }
        public string Status { get; set; }
        public string TimeZoneId { get; set; }
        public string TimeZoneName { get; set; }
    }
}