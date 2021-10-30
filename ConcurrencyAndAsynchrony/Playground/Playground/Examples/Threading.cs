using System;
using System.Threading;

namespace Playground.Examples
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

        private void WriteY()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("Y"+Thread.CurrentThread.Name);
                Thread.Sleep(1);
            }
        }
    }
}
