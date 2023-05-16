using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AA();
            Console.WriteLine("22222");
            Console.ReadLine();
        }
        static async void AA()
        {
            Task.Delay(5000);
        }
    }
}
