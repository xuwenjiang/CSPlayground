using System;
using CSPlayground.Examples;

namespace CSPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            string control = null;
            do
            {
                try
                {
                    PrintExampleList();
                    control = Console.ReadLine();
                    AbstractExample example = CreateExampleClass(control);
                    example.RunExample();
                } catch(Exception) {}
            }
            while (control != "exit");
        }

        static AbstractExample CreateExampleClass(string select)
        {
            switch (select)
            {
                case "1":
                    return new Threading();
            }

            throw new Exception("Select invalid");
        }

        static void PrintExampleList()
        {
            Console.WriteLine("****** Please Select a Category ******");
            Console.WriteLine("*** 1. Threading                   ***");
            Console.WriteLine("*******   Type exit to Exit    *******");
        }
    }
}
