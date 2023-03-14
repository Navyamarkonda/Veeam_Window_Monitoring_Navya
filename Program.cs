namespace Veeam_Window_Monitoring
{
    internal class Process_Execution
    {

        public static void Main(string[] args)
        {
            //Give the values
            Console.WriteLine("Please enter the Process Name");
            string processName = Console.ReadLine(); ;
            Console.WriteLine("Please enter the MaxLifetime in minutes");
            string maxLifetimeInMinutes = Console.ReadLine();
            Console.WriteLine("Please enter the Monitoring Frequency");
            string monitoringFrequencyInMinutes = Console.ReadLine();

            //Display
            Console.WriteLine($"Monitoring {processName} every {monitoringFrequencyInMinutes} minute(s).");
            Process_Monitor monitor = new Process_Monitor();
            //Method Execution
            monitor.Execute(processName, maxLifetimeInMinutes, monitoringFrequencyInMinutes);

        }

    }
}