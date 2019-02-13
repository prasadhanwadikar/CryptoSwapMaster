using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.Service
{
    static class Program
    {
        private static BotsManager _botsManager = null;

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        public delegate bool HandlerRoutine(ControlTypes CtrlType);

        public enum ControlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        private static bool HandleConsoleControl(ControlTypes ctrlType)
        {
            if (_botsManager != null) _botsManager.Stop();
            return true;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (true || System.Diagnostics.Debugger.IsAttached)
            {
                HandlerRoutine handler = new HandlerRoutine(HandleConsoleControl);
                SetConsoleCtrlHandler(handler, true);

                _botsManager = new BotsManager();
                _botsManager.Start();
                Console.ReadKey();
                _botsManager.Stop();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new BotsManager()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
