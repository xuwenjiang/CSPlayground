using System;
using System.Threading;

namespace CSPlayground.Examples
{
    /// <summary>
    /// Threading
    /// A thread is an execution path that can proceed independently of others.
    /// 
    /// 
    /// </summary>
    class Threading
    {
        /// <summary>
        /// Example 1
        /// You will see X and Y blocks. This depends on Operation System.
        /// 1.1 Thread can have Name
        /// </summary>
        public void CreateAThread()
        {
            // kick off a new thread
            Thread t1 = new Thread(WriteY);
            t1.Name = "1";
            // running WirteY()
            t1.Start();

            // kick off a new thread
            Thread t2 = new Thread(WriteY);
            t2.Name = "2";
            // running WirteY()
            t2.Start();

            // Simultaneously, main thread write X
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("X");
            }
        }

        /// <summary>
        /// Example 2
        /// Use Join method to let current thread to wait another thread to end
        /// you can specify timeout
        /// </summary>
        public void JoinAndSleep()
        {
            Thread t1 = new Thread(WriteY);
            t1.Name = "1";
            t1.Start();
            
            Thread t2 = new Thread(WriteY);
            t2.Name = "2";
            t2.Start();

            t1.Join(3);
            t2.Join(3);
            Console.WriteLine();
            Console.WriteLine("Thread t1 has ended!");
            Console.WriteLine("Thread t2 has ended!");

            WriteY();
        }

        /// <summary>
        /// Example 3
        /// 
        /// An I/O-bound operation works in one of two ways: 
        /// 1. either waits synchronously on the current thread until the operation is complete (such as Console.ReadLine, Thread.Sleep, or Thread.Join), 
        /// 2. or it operates asynchronously, firing a callback when the operation finishes some time later.
        /// 
        /// Can do "spin" like this: while (DateTime.Now < nextStartTime) Thread.Sleep(100);
        /// </summary>
        public void Blocking()
        {
            Thread t1 = new Thread(WriteY);
            t1.Name = "1";
            t1.Start();

            while(t1.IsAlive)
            {
                bool blocked = (t1.ThreadState & ThreadState.WaitSleepJoin) != 0;
                if (blocked)
                {
                    // if you comment WriteY()'s Thread.Sleep(1), you will see this line printed.
                    Console.WriteLine("Blocked");
                }
            }
        }

        public void LocalAndShared()
        {
            
        }

        private void WriteFive()
        {
            for (int i = 0; i < 5; i++)
            {
                
            }
        }

        private void WaitInput(out string read)
        {
            read = Console.ReadLine();
        }

        private void WriteY()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("Y"+Thread.CurrentThread.Name);
                //Thread.Sleep(1);
            }
        }
    }
}
