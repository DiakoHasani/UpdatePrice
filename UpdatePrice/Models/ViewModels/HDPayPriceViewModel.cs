using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatePrice.Models.ViewModels
{
    public class HDPayPriceViewModel
    {
        public decimal Price { get; set; }
        public bool Result { get; set; } = false;
        public string Message { get; set; } = "";
    }
}
