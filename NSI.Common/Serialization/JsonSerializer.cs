using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Serialization
{
    public static class JsonHelper
    {
        /// <summary>
        /// Serializes object into JSON string
        /// </summary>
        /// <typeparam name="T">Type of object to be serialized</typeparam>
        /// <param name="item">Object to be serialized</param>
        /// <returns>Serialized object as string</returns>
        public static string Serialize<T>(T item)
        {
            if (item == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(item);
        }

        /// <summary>
        /// Deserializes JSON string into target object
        /// </summary>
        /// <typeparam name="T">Object type to deserialize to</typeparam>
        /// <param name="xmlString">JSON string to deserialize</param>
        /// <returns>Deserialized object</returns>
        public static T Deserialize<T>(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
