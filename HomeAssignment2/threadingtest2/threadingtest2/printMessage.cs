using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace threadingtest2
{
    //class for printing messages to decide later if to use this or not 
    class printMessage
    {
        public static void printMessageHandler(int p)
        {
            Console.WriteLine("Printing Thread message"+Thread.CurrentThread.Name+p);
        }
    }
}
