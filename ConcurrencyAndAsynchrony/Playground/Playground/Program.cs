using System;
using Playground.Examples;

namespace Playground
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
