using DNTScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdatePrice.Models.Services;
using UpdatePrice.Models.XmlDocument;

namespace UpdatePrice.Models.Schedules
{
    public class ScheduledTasksRegistry
    {
        public static void Init(IGetPrice getPrice)
        {
            ScheduledTasksCoordinator.Current.AddScheduledTasks(
                new GetPriceCrypto(getPrice));

            ScheduledTasksCoordinator.Current.OnUnexpectedException = (exception, scheduledTask) =>
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, exception);
            };

            ScheduledTasksCoordinator.Current.Start();
        }

        public static void End()
        {
            ScheduledTasksCoordinator.Current.Dispose();
        }

        public static void WakeUp(string pageUrl)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    client.Headers.Add("User-Agent", "ScheduledTasks 1.0");
                    client.DownloadData(pageUrl);
                }
            }
            catch (Exception ex)
            {
                WriteXmlDocument.AddException(MethodBase.GetCurrentMethod().DeclaringType.FullName, ex);
            }
        }
    }
}
