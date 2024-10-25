using Newtonsoft.Json;

namespace CNSMarketing.Service.Helpers
{
    public static class Helpers
    {
        public static DateTime ConvertSecondsToDate(long expires_in)
        {
            DateTime expireDate = DateTime.Now.AddSeconds(expires_in);
            return expireDate;
        }

        public static DateTime ConvertTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddMilliseconds(unixTimeStamp);
        }

        public static string ConvertToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}

