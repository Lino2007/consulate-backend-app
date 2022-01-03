using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NSI.Common.Serialization
{
    public static class JsonHelper
    {
        /// <summary>
        /// Serializes object into JSON string
        /// </summary>
        /// <typeparam name="T">Type of object to be serialized</typeparam>
        /// <param name="item">Object to be serialized</param>
        /// <param name="camelCase">Whether to use camel case in serialization</param>
        /// <returns>Serialized object as string</returns>
        public static string Serialize<T>(T item, bool camelCase = true)
        {
            if (item == null)
            {
                return null;
            }

            if (camelCase)
            {
                return JsonConvert.SerializeObject(item, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });
            }

            return JsonConvert.SerializeObject(item, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// Deserializes JSON string into target object
        /// </summary>
        /// <typeparam name="T">Object type to deserialize to</typeparam>
        /// <param name="jsonString">JSON string to deserialize</param>
        /// <param name="camelCase">Whether to use camel case in deserialization</param>
        /// <returns>Deserialized object</returns>
        public static T Deserialize<T>(string jsonString, bool camelCase = true)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(T);
            }

            if (camelCase)
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }

            return JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            });
        }
    }
}
