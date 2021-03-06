using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSPlayground.Examples
{
    /// <summary>
    /// Threading
    /// A thread is an execution path that can proceed independently of others.
    /// 
    /// </summary>
    class Threading : AbstractExample
    {
        private bool _done;
        private readonly object _locker = new object();

        protected override void DisplayExampleListAndRunBasedOnSelection()
        {
            Console.WriteLine("************ Please Select an Example ************");
            Console.WriteLine("*** 1. Create a Thread                         ***");
            Console.WriteLine("*** 2. Join And Sleep                          ***");
            Console.WriteLine("*** 3. Blocking                                ***");
            Console.WriteLine("*** 4. Locking And ThreadSafe                  ***");
            Console.WriteLine("*** 5. Passing Data To Thread                  ***");
            Console.WriteLine("*** 6. Foreground Background Threads           ***");
            Console.WriteLine("*** 7. Signaling                               ***");
            Console.WriteLine("*** 7. ThreadPool                              ***");
            Console.WriteLine("**************************************************");
            switch (Console.ReadLine())
            {
                case "1":
                    CreateAThread();
                    break;
                case "2":
                    JoinAndSleep();
                    break;
                case "3":
                    Blocking();
                    break;
                case "4":
                    LockingAndThreadSafe();
                    break;
                case "5":
                    PassingDataToThread();
                    break;
                case "6":
                    ForegroundBackgroundThreads();
                    break;
                case "7":
                    Signaling();
                    break;
                case "8":
                    ThreadPool();
                    break;
            }
        }

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
            Thread t1 = new Thread(WriteYWithSleep);
            t1.Name = "1";
            t1.Start();

            while(t1.IsAlive)
            {
                bool blocked = (t1.ThreadState & ThreadState.WaitSleepJoin) != 0;
                if (blocked)
                {
                    Console.WriteLine("Blocked");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Example 4
        /// 
        /// lock can be upon any reference-type object
        /// </summary>
        public void LockingAndThreadSafe()
        {
            Thread t1 = new Thread(WriteWithLock);
            t1.Name = "t1";
            t1.Start();

            Thread t2 = new Thread(WriteWithLock);
            t2.Name = "t2";
            t2.Start();
        }

        /// <summary>
        /// Example 5
        ///
        /// passing data by lambda expression and by calling start method.
        /// 
        /// </summary>
        public void PassingDataToThread()
        {
            // passing by lambda
            Thread t1 = new Thread(()=> { PrintWithString("Hello from Lambda"); });
            t1.Start();

            // passing by lambda with any number 
            Thread t2 = new Thread(() =>
            {
                Console.WriteLine("I'm running on another thread!");
                Console.WriteLine("This is so easy!");
            });
            t2.Start();

            // passing to thread's start method (less flexible)
            Thread t3 = new Thread(PrintWithObj);
            t3.Start("Hello from t3!");

            t1.Join();
            t2.Join();
            t3.Join();
        }

        /// <summary>
        /// Example 6
        /// 
        /// By default, threads are foreground threads.
        /// 
        /// any foreground thread is running ==> application running.
        /// all foreground threads finish ==> application ends ==> alll backgrounds terminate.
        /// 
        /// You can query or change a thread’s background status using its IsBackground.
        /// 
        /// In this example, you will find that if the thread is in foreground, when you type exit, the Y will still be printed to screen.
        /// </summary>
        public void ForegroundBackgroundThreads()
        {
            Console.WriteLine("Will Print Y 100 times with a pulse of 1 time per second");
            Console.WriteLine("in put 1 if you want that thread to run in background, otherwise it will run in foreground");
            Console.WriteLine("The application itself will terminate if you type exit");
            string input = Console.ReadLine();
            Thread t = new Thread(() => { for (int i = 0; i < 100; i++) { Console.Write("Y"); Thread.Sleep(1000); } });
            if (input == "1")
            {
                t.IsBackground = true;
            }
            t.Start();
        }

        /// <summary>
        /// Example 7
        ///
        /// Sometimes, you need a thread to wait until receiving notification(s) from other thread(s).
        /// </summary>
        public void Signaling()
        {
            Console.WriteLine("There will be a thread started, so press 1 to let it continue.");
            var signal = new ManualResetEvent(false);

            Thread t = new Thread(() =>
            {
                Console.WriteLine("Waiting for signal...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine("Got signal!");
            });
            t.Start();

            while (Console.ReadLine() != "1")
            {

            }

            signal.Set();
            t.Join();
        }

        /// <summary>
        /// Example 8
        ///
        /// Whenever you start a thread, a few hundred microseconds are spent organizing such things as a fresh local variable stack.
        /// The thread pool cuts this overhead by having a pool of pre-created recyclable threads.
        ///
        /// Thread pool also can do hygience in the thread pool, why?
        /// Ensure that a temporary excess of compute-bound work does not cause CPU oversubscription
        /// Oversubscription is the condition of there being more active threads than CPU cores, with the OS having to time-slice threads.
        /// Oversubscription hurts performance because time-slicing requires expensive context switches and can invalidate the CPU caches that have become essential in delivering performance to modern processors.
        /// 
        ///  
        /// </summary>
        public void ThreadPool()
        {
            Task.Run(() => Console.WriteLine("The easiest way to explicitly run something on a pooled thread is to use Task.Run"));
        }

        private void PrintWithString(string message) => Console.WriteLine(message);

        private void PrintWithObj(object messageObj)
        {
            string message = (string)messageObj;   // We need to cast here
            Console.WriteLine(message);
        }

        private void WriteWithLock()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} started");
            lock (_locker)
            {
                if (!_done)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} done");
                    _done = true;
                }
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} finished");
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
            }
        }

        private void WriteYWithSleep()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("Y" + Thread.CurrentThread.Name);
                Thread.Sleep(1);
            }
        }
    }
}
