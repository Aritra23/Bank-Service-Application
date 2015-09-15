using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace threadingtest2
{
    //class for the handling the buffer
    class Buffer
    {

        public delegate void orderDelegate();
        //public static event orderDelegate orderAvailableEvent;

        private string orderString;
        //private bool bufferFull = false;

        Semaphore write = new Semaphore(3, 3);  //semaphore for writing in the buffer
        Semaphore read = new Semaphore(2, 2);   //semaphore for reading from the buffer

        const int SIZE = 3;  //size of the buffer
        int head = 0, tail = 0, nElements = 0;
        string[] buffer = new string[SIZE];

        //get and set for the order string
        public string OrderString
        {
            get
            {
                return orderString;
            }
            set
            {
                orderString = value;
            }
        }

        //adding the order in the buffer
        public void enQueueOrder(string orderString)
        {
    
                setBuffer(orderString);    

        }

        //removing the order from buffer
        public string deQueueOrder()
        {
            string returnValue = null;

                returnValue =  getBuffer();

            return returnValue;
         
     
        }

        public string getBuffer()
        {



             read.WaitOne();
            //Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Entred Read");
            lock (this)
            {
                
                while (nElements == 0)
                {
                    Monitor.Wait(this);
                }

                orderString = buffer[head];
                head = (head + 1) % SIZE;
                nElements--;
               // Console.WriteLine("Read from the buffer: {0} , {1}, {2}", element, DateTime.Now, nElements);
                //Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "leaving Read");
                read.Release();
                Monitor.Pulse(this);
                return orderString;
            }

         }

        //writing elements from travel agency to the buffer
        public void setBuffer(string val)
        {


            write.WaitOne();
            //Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Entred Write");
            lock (this)
            {
                while (nElements == SIZE)
                {
                    Monitor.Wait(this);
                }

                buffer[tail] = val;
                tail = (tail + 1) % SIZE;
                //Console.WriteLine("Write to the buffer: {0}, {1}, {2}", val, DateTime.Now, nElements);
                nElements++;
                //Console.WriteLine("Thread : " + Thread.CurrentThread.Name + "Leaving Write");
                write.Release();
                Monitor.Pulse(this);

            }
         
        }


    }


}

