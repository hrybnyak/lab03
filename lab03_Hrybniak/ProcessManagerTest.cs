using System;
using System.Collections.Generic;
using System.Text;

namespace lab03_Hrybniak
{
    public static class ProcessManagerTest
    {
        public static void Start_Test_AwerageByPriority()
        {
            var processManager = new ProcessManager();
            for (int i = 0; i < 20; i++)
            {
                Random programSizeGenerator = new Random();
                processManager.AddProcess(programSizeGenerator.Next(0, 10000));
            }
            processManager.ExecuteAll();
            processManager.PrintAllProcesses();

            Console.WriteLine();
            Console.WriteLine("priority,awerageTime");
            foreach(var keyValue in processManager.AwerageWaitingTimeByPriority)
            {
                Console.WriteLine($"{keyValue.Key},{keyValue.Value}");
            }
        }

        public static void Start_TestAverageByLoad()
        {
            var awerageTimeByLoad = new Dictionary<int, int>();
            for (int i = 10; i <= 80; i = i + 10)
            {
                var processManager = new ProcessManager();
                for (int j = 0; j < i; j++)
                {
                    Random programSizeGenerator = new Random();
                    processManager.AddProcess(programSizeGenerator.Next(0, 1000));
                }
                processManager.ExecuteAll();
                processManager.PrintAllProcesses();
                awerageTimeByLoad.Add(i, processManager.AwerageWaitingTime);
            }

            Console.WriteLine();
            Console.WriteLine("load,awerageTime");
            foreach (var keyValue in awerageTimeByLoad)
            {
                Console.WriteLine($"{keyValue.Key},{keyValue.Value}");
            }
        }
    }
}
