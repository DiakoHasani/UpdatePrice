using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatePrice.Models.ViewModels
{
    public class ResultNobitexViewModel
    {
        public bool ResultApi { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public StatsViewModel Stats { get; set; }
    }
}
