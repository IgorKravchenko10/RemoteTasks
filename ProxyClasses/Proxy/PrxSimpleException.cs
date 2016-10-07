using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProxyClasses
{
    [DataContract()]
    public class PrxSimpleException
    {
        [DataMember()]
        public string Message { get; set; }
        [DataMember()]
        public string InnerMessage { get; set; }
        [DataMember()]
        public string Source { get; set; }
        [DataMember()]
        public string ClassName { get; set; }
        [DataMember()]
        public string StackTrace { get; set; }
        [DataMember()]
        public string Version { get; set; }

        public PrxSimpleException(Exception exception, string version)
        {
            if (exception != null)
            {
                Source = exception.Source;
                Message = exception.Message;
                this.Version = version;
                ClassName = exception.TargetSite.ReflectedType.FullName;
                if (exception.InnerException != null)
                {
                    if (exception.InnerException.InnerException != null)
                    {
                        InnerMessage = exception.InnerException.InnerException.Message;
                    }
                    else
                    {
                        InnerMessage = exception.InnerException.Message;
                    }
                }
                StackTrace = exception.StackTrace;
            }
        }

        //для сериализации в XML
        public PrxSimpleException()
        {

        }

        public string ToXML()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.OmitXmlDeclaration = true;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());
            StringBuilder sb = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                serializer.Serialize(writer, this);
            }
            return sb.ToString();
        }

    }
}
