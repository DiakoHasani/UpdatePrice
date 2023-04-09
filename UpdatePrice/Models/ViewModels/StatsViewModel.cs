using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatePrice.Models.ViewModels
{
    public class StatsViewModel
    {
        [JsonProperty("usdt-rls")]
        public RlsViewModel USDT_RLS { get; set; }
    }
}
