using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Veeam_Window_Monitoring
{
    public class Process_Monitor
    {
        public Process_Monitor() { }
        public string Execute(string processName, string maxLifetimeInMinutes, string monitoringFrequencyInMinutes)
        {
            return Execute(processName, maxLifetimeInMinutes, monitoringFrequencyInMinutes, CancellationToken.None);
        }
        public string Execute(string processName, string maxLifetimeInMinutes, string monitoringFrequencyInMinutes, CancellationToken cancellationToken)
        {

            String Processkilled = "processkilled";
            String ProcessNotKilled = "processNotkilled";
            String noprocess = "noprocess";
            while (true)
            {
                // Check if the user has pressed the 'q' key to exit
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                //Cancellationtoken to cancel the method if needed
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Process monitoring canceled.");
                    return noprocess;
                }

                Process[] processes = Process.GetProcessesByName(processName);
                //process is running more than Maxlifetime
                if (processes.Length > 0)
                {
                    foreach (Process process in processes)
                    {
                        if ((DateTime.Now - process.StartTime).TotalMinutes > Convert.ToInt32(maxLifetimeInMinutes))
                        {
                            Console.WriteLine($"Process {process.ProcessName} (PID {process.Id}) has been running for more than {maxLifetimeInMinutes} minutes. Killing...");
                            process.Kill();
                            return Processkilled;
                        }
                        //process is running less than Maxlifetime
                        else
                        {
                            return ProcessNotKilled;

                        }

                    }

                }
                //no process is running
                else
                {
                    Console.WriteLine($"No {processName} process is currently running.");
                }

                Thread.Sleep(Convert.ToInt32(monitoringFrequencyInMinutes) * 60000);

            }
            return noprocess;

        }
    }
}