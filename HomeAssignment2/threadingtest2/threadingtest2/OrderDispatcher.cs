using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace threadingtest2
{
    //Class for handling the order dispatching for hotels 
    class OrderDispatcher
    {
        //delegate for processing the order recieved in the buffer
        public delegate void orderEventDelegate(int hotelId, string orderString);
        public static event orderEventDelegate orderReceivedEvent;

        //Processing the order placed in the buffer
        public void processOrderQueue()
        {
            string orderString = null;
            int hotelId;

            while (true)
            {
                //retrieving the order from the buffer
                orderString = Program.orderBuffer.deQueueOrder();

                //Split to seperate encoded value from the date.
                string[] orderStringSplit = Regex.Split(orderString, "\t"); 

                //Decode order from multi-buffer
                orderString = EncodeDecode.decodeOrder(orderStringSplit[0], "ABCDEFGHIJKLMNOP");

                orderString = orderString +"::"+ orderStringSplit[1]; // Create new string with decoded value and date.

                Console.WriteLine("Order Dispatcher: Processing Order: " + orderString);

                //Code to figure out which hotel's order.... and dispatch event

                hotelId = getHotelIdFromOrderString(orderString);
                
                if(orderReceivedEvent !=null)
                { 
                    orderReceivedEvent(hotelId, orderString); 
                }

            }
                
        }

        //retrieving the individual order items from the order string
        private int getHotelIdFromOrderString(string orderString)
        {
            string[] lines = Regex.Split(orderString, "::");

   

            int agencyId = Convert.ToInt32(lines[0]);
            int hotelId = Convert.ToInt32(lines[1]);
            int agencyCardNo = Convert.ToInt32(lines[2]);
            int noOfRooms    = Convert.ToInt32(lines[3]);

            return hotelId;
          
        }

    }

    
}
