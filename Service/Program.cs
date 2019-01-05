using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BotsManagerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var botsManager = new BotsManager();
            botsManager.Start();
            Console.Read();
            botsManager.Stop();

            /*
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BotsManager()
            };
            ServiceBase.Run(ServicesToRun);
            */
        }
    }
}
