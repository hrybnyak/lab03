using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace lab03_Hrybniak
{
    public class ProcessManager
    {
        private List<Process> _processes = new List<Process>();

        //number of bytes that can be transfered between RAM and ROM
        private const int BytesTransferedInTimeQ = 10;
        private const int MaxPriority = 32;

        public int AwerageWaitingTime { get; private set; }
        public Dictionary<int, int> AwerageWaitingTimeByPriority { get; private set; } = new Dictionary<int, int>();

        public void AddProcess(int programSize)
        {
            int priority = (int)(Math.Log2((double)programSize / (double)BytesTransferedInTimeQ) + 1);
            if(priority > MaxPriority)
            {
                priority = MaxPriority;
            }
            var process = new Process(programSize, priority);
            _processes.Add(process);
            _processes = _processes.OrderBy(p => p.Priority).ToList();
        }

        public void ExecuteAll()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach(var process in _processes)
            {
                process.WaitTimeInSeconds = stopwatch.ElapsedMilliseconds;
                process.Execute();
            }
            stopwatch.Stop();
            CalculateAwerages();
        }

        private void CalculateAwerages()
        {
            AwerageWaitingTime = (int)(_processes.Sum(p => p.WaitTimeInSeconds) / _processes.Count);

            for (int i = 1; i <= MaxPriority; i++)
            {
                if(_processes.Any(p => p.Priority == i))
                {
                    var currentPriorityProcessors = _processes.Where(p => p.Priority == i);
                    var awerageByPriority = (int)(currentPriorityProcessors.Sum(p => p.WaitTimeInSeconds) / currentPriorityProcessors.Count());
                    AwerageWaitingTimeByPriority.Add(i, awerageByPriority);
                }
            }
        }
        public void PrintAllProcesses()
        {
            Console.WriteLine("Id,ProgramLength,Priority,Wait Time");
            foreach (var process in _processes)
            {
                Console.WriteLine(process.ToString());
            }
        }
    }
}
