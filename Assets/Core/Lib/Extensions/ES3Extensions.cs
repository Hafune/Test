using System;

namespace Lib
{
    public static class ES3Functions
    {
        public static string SerializeToBase64(object data)
        {
            var serializeData = ES3.Serialize(data);
            return Convert.ToBase64String(serializeData);
        }

        public static T DeserializeFromBase64<T>(string data)
        {
            try
            {
                var decodeByte = Convert.FromBase64String(data);
                return ES3.Deserialize<T>(decodeByte);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}