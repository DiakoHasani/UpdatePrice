using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.Schedules;
using UpdatePrice.Models.Services;

namespace UpdatePrice
{
    class Program
    {
        static void Main(string[] args)
        {
            new GetPrice().Start();
            Console.ReadKey();
        }
    }

    class GetPrice : IGetPrice
    {
        public void Start()
        {
            ScheduledTasksRegistry.Init(this);
        }
        public void ClearMessage()
        {
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
