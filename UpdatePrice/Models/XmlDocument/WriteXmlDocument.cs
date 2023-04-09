using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace UpdatePrice.Models.XmlDocument
{
    public class WriteXmlDocument
    {
        private static (string fileName, bool result) Write(XDocument document, string path)
        {
            try
            {
                var fileName = $"{path}{Guid.NewGuid()}.xml";
                document.Save(fileName);
                return (fileName, true);
            }
            catch (Exception)
            {
                return ("", false);
            }
        }
        private static string GetPath()
        {
            return ConfigurationManager.AppSettings["DocumentPath"];
        }
        private static XElement GenerateElements<T>(T model)
        {
            var textXml = Serialize(model);
            return XElement.Parse(textXml);
        }
        private static XDocument GenerateExceptionXml(ExceptionModel model)
        {
            try
            {
                return new XDocument(
                    new XComment("This is a Xml Exception"),
                    new XProcessingInstruction("xml-stylesheet", "href='mystyle.css' title='Compact' type='text/css'"),
                    new XElement("Info",
                        new XElement("XmlType", "Exception"),
                        new XElement("Address", model.Address),
                        new XElement("Title", model.Title),
                        new XElement("Date", DateTime.Now),
                        new XElement("Exception",
                            GenerateElements(model))));
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static XDocument GenerateErrorXml(ErrorModel model)
        {
            try
            {
                return new XDocument(
                    new XComment("This is a Xml Error"),
                    new XProcessingInstruction("xml-stylesheet", "href='mystyle.css' title='Compact' type='text/css'"),
                    new XElement("Info",
                        new XElement("XmlType", "Error"),
                        new XElement("Address", model.Address),
                        new XElement("Title", model.Title),
                        new XElement("Date", DateTime.Now),
                        new XElement("Error",
                            GenerateElements(model))));
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static XDocument GenerateLogXml(LogModel model)
        {
            try
            {
                return new XDocument(
                    new XComment("This is a Xml Log"),
                    new XProcessingInstruction("xml-stylesheet", "href='mystyle.css' title='Compact' type='text/css'"),
                    new XElement("Info",
                        new XElement("XmlType", "Log"),
                        new XElement("Address", model.Address),
                        new XElement("Title", model.Title),
                        new XElement("Date", DateTime.Now),
                        new XElement("Log",
                            GenerateElements(model))));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void AddException(string address, Exception ex)
        {
            var xml = GenerateExceptionXml(new ExceptionModel
            {
                Address = address,
                HelpLink = ex.HelpLink,
                HResult = ex.HResult,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                Title = ex.ToString()
            });
            if (xml != null)
            {
                Write(xml, GetPath());
            }
        }
        public static void AddError(string address, string title, string errorMessage)
        {
            var xml = GenerateErrorXml(new ErrorModel
            {
                Address = address,
                Message = errorMessage,
                Title = title
            });
            if (xml != null)
            {
                Write(xml, GetPath());
            }
        }
        public static void AddLog(LogModel log)
        {
            var xml = GenerateLogXml(log);
            if (xml != null)
            {
                Write(xml, GetPath());
            }
        }
        private static string Serialize<T>(T dataToSerialize)
        {
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, dataToSerialize);
                var result = stringwriter.ToString();
                result = result.Replace("\r\n", "");
                result = result.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                result = result.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                result = result.Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                return result;
            }
            catch (Exception ex)
            {
                return $"<Error>error in Serialize xml exception message: {ex.Message} </Error>";
            }
        }
    }
}
