using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RTConfiguration.Util
{
    public static class XmlSerializerHelper
    {
        public static void SaveToXml<T>(string filePath, T sourceObj)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
        }

        public static string ToString<T>(T source)
        {
            if (source == null) throw new NullReferenceException();
            var serializer = new XmlSerializer(source.GetType());
            var sb = new StringBuilder();
            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, source);
            }
            return sb.ToString();
        }

        public static T ToInstance<T>(string source)
        {
            using(StringReader reader=new StringReader(source))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var obj = serializer.Deserialize(reader);
                return (T)obj;
            }
        }

        public static object LoadFromXml(string filePath, Type type)
        {
            object result = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                    result = xmlSerializer.Deserialize(reader);
                }
            }

            return result;
        }
    }
}
