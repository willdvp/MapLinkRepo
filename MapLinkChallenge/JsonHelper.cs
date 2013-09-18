using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace MapLinkChallenge
{
    static class JsonHelper
    {
        public static string Serialize<T>(T t)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            string jsonOutput;

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, t);
                jsonOutput = Encoding.UTF8.GetString(ms.ToArray());
            }

            return jsonOutput;
        }

        public static T Deserialize<T>(string jsonInput)
        {
            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
            T result;

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput)))
            {
                result = (T)deserializer.ReadObject(ms);
            }

            return result;
        }
    }
}
