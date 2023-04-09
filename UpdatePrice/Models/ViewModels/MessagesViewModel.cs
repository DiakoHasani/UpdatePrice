using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatePrice.Models.ViewModels
{
    public class MessagesViewModel
    {
        public bool Result { get; set; } = false;
        public List<string> Messages { get; set; } = new List<string>();
        public int Code { get; set; }
    }
}
