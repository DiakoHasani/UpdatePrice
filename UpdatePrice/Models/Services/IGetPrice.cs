using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdatePrice.Models.Services
{
    public interface IGetPrice
    {
        void ShowMessage(string message);
        void ClearMessage();
    }
}
