using System;
using CSPlayground.Examples;

namespace CSPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            Threading threadingExample = new Threading();

            //threadingExample.CreateAThread();
            //threadingExample.JoinAndSleep();
            threadingExample.Blocking();
        }
    }
}
