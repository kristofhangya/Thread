using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;


    class Test
    {
        public void ThreadInfo(object parameter)
        {
            Console.WriteLine("Szál-név: {0}", Thread.CurrentThread.Name);  //thread neve
            if (parameter is string)
            {
                Console.WriteLine("Paraméter: {0}", parameter);
            }
        }
    }

    class Diag
    {
        public void cpuCounter(object parameter)
        {
        //Console.WriteLine("Thread Fut: {0}",Thread.CurrentThread.Name);
        while (true)
         {
            double cpuUsageValue = Convert.ToDouble(back());
            double memUsageValue = Convert.ToDouble(backm());
            //double allmemUsageValue = Convert.ToDouble(backtm());
            Console.Clear();
            Console.WriteLine("Thread Fut: {0}", Thread.CurrentThread.Name);
            Console.WriteLine("CPU_Total : {0} %", cpuUsageValue);
            Console.WriteLine("MEM_Total : {0} GB", memUsageValue);
            //Console.WriteLine("MEM_Total : {0} GB", allmemUsageValue);
        }
        }


        private object back()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter();
            
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            // will always start at 0
            dynamic firstValue = cpuCounter.NextValue();
            Thread.Sleep(500);
            // now matches task manager reading
            dynamic secondValue = cpuCounter.NextValue();

            return secondValue;
        }

    private object backm()
    {

        PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        dynamic secondValue = ramCounter.NextValue();

        return secondValue;
    }


        /*private object backtm()
         {
        
        PerformanceCounter ramCounter = new PerformanceCounter("Memory",???);
        dynamic secondValue = ramCounter.NextValue();
        return secondValue;
         }
         */


}
    

    class Program
    {
        static void Main(string[] args)
        {
        Diag t = new Diag();
        Thread bThread = new Thread(new ParameterizedThreadStart(t.cpuCounter));
        bThread.Name = "Background-Thread";
        bThread.Start("Hello");
        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        bThread.Abort();
        Console.WriteLine("Thread is dead");
     
        Console.ReadKey();
        }
        
    }

