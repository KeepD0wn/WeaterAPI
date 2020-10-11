using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static int Plus(int x,int y)
        {
            Thread.Sleep(10000);
            return x + y;           
        }

        public static async Task<int> PlusAsync(int x, int y)
        {
            Console.WriteLine("Начало метода");
            int o= await Task.Run(()=> Plus(x,y));
            return o;
        }

        static void Main(string[] args)
        {
            Task<int> x = Task.Factory.StartNew(()=>Plus(1,2));
            Console.WriteLine("da");
           // x.Wait();
           // Console.WriteLine(x.Result);
            Plus(1,1);
            Console.WriteLine("vse");
           

            Console.ReadKey();
        }
    }
}
