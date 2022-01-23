using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace NSI.Common.Serialization
{
    [ExcludeFromCodeCoverage]
    public static class XmlHelper
    {
        /// <summary>
        /// Serializes object into XML string
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

            DataContractSerializer serializer = new DataContractSerializer(item.GetType());
            using (StringWriter backing = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(backing))
                {
                    serializer.WriteObject(writer, item);
                    return backing.ToString();
                }
            }
        }

        /// <summary>
        /// Deserializes XML string into target object
        /// </summary>
        /// <typeparam name="T">Object type to deserialize to</typeparam>
        /// <param name="xmlString">XML string to deserialize</param>
        /// <returns>Deserialized object</returns>
        public static T Deserialize<T>(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
            {
                return default(T);
            }

            using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                T obj = (T)serializer.ReadObject(memStream);
                return obj;
            }
        }
    }
}
