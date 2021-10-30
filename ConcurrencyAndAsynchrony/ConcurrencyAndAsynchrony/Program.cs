using System;
using ConcurrencyAndAsynchrony.Examples;

namespace ConcurrencyAndAsynchrony
{
    class Program
    {
        static void Main(string[] args)
        {
            Threading threadingExample = new Threading();

            //threadingExample.CreateAThread();
            threadingExample.JoinAndSleep();
        }
    }
}
