using System;
using System.Threading;

namespace lab03_Hrybniak
{
    public class Process
    {
        public Guid Id { get; set; }
        public int Priority { get; private set; }
        public int ProgramLengthInBytes { get; private set; }

        //should be calculated by process manager
        public long WaitTimeInSeconds { get; internal set; } = 0;

        public Process(int programLength, int priority)
        {
            Id = Guid.NewGuid();
            Priority = priority;
            ProgramLengthInBytes = programLength;
        }

        public override string ToString()
        {
            return $"{Id},{ProgramLengthInBytes},{Priority},{WaitTimeInSeconds}";
        }

        public void Execute()
        {
            Thread.Sleep(ProgramLengthInBytes);
        }
    }
}
