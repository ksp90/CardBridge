using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace UtilityBox
{
    public enum MessageType
    {
        Clientlist,
        Messages,
        Error,
        Warning
    }

    public enum Command
    {
        Login,
        Broadcast,
        Unicast,
        EnterConference, 
        LeaveConference,
        Bidding, 
        Logoff
    }

    public class UtilityBoxHelper
    {
        public static string XmlSerializeToString(object objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }
            sb.Replace(Environment.NewLine, string.Empty);
            return sb.ToString();
        }

        public static T XmlDeserializeFromString<T>(string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        public static object XmlDeserializeFromString(string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
