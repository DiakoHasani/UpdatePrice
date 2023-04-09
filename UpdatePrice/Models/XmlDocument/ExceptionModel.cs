using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatePrice.Models.XmlDocument
{
    public class ExceptionModel : BaseXmlDocumentBody
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string HelpLink { get; set; }
        public string Source { get; set; }
        public int HResult { get; set; }
    }
}
