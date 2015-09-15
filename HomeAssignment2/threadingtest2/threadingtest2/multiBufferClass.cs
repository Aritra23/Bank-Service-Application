using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threadingtest2
{
    //class for holding the value of the order string
    class multiBufferClass
    {
        //creating the delegate for the multi buffer to check buffer value
        public delegate void multiBufferDelegate(string value);
        public event multiBufferDelegate bufferFull;

        string bufferValue;

        //Constructor
        public multiBufferClass()
        {
            this.bufferValue = null;
        }

        //Set the buffer with the transmitted value.
        public void setBuffer(string value)
        {
            this.bufferValue = value;
            //Console.WriteLine("Multibuffer-Value:" + value);

            alertBufferFull(value);
            //checkBuffer(value);
           
        }

        //alerting if the buffer is full
        public void alertBufferFull(string value)
        {
            if(bufferFull != null)
            {
                bufferFull(value);
            }
            
        }

        //get the value that was stored in the buffer.
        public string getBuffer()
        {
            return bufferValue;
        }
    }
}
