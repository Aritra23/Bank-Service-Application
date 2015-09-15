using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace threadingtest2
{
    //Class for travel agency details 
    class Agency
    {
        private int agencyId = 0;
        private int agencyCardNo = 0;
        private int noOfRooms;
        private int price;
        
        //generating the get set methods
        public int AgencyId
        {
            get
            {
                return agencyId;
            }
            set
            {
                agencyId = value;
            }
        }

        public int AgencyCardNo
        {
            get
            {
                return agencyCardNo;
            }
            set
            {
                agencyCardNo = value;
            }
        }

        //returns true if new rate less than old rate  
        private Boolean isGoodDeal(int newRoomRate)
        {
            return true;
        }

        //writing the time stamp to the order
        private string attachTimeStamp(string orderString)
        {
            orderString = orderString + "\t"+(DateTime.Now.ToString("h:mm:ss tt"));
            //Console.WriteLine("Encoded order string: "+orderString);
            return orderString;
        }

        //placing the order for rooms
        private void placeOrder(int hotelId)
        {
            // new order object
            Order order = new Order(this.agencyId, hotelId, this.agencyCardNo, this.noOfRooms);

            string orderString = order.toString();

            string encodedOrderString = EncodeDecode.encodeOrder(orderString,"ABCDEFGHIJKLMNOP"); //Encoding order


            string encodedOrderStringWithDate = attachTimeStamp(encodedOrderString); //Attach timestamp when order placed.
               
            Program.orderBuffer.enQueueOrder(encodedOrderStringWithDate); //Adding order to the buffer

            Console.WriteLine("OrderFormat-> AgencyId:HotelId:creditcardNo:RoomsOrdered");
            
            Console.WriteLine("Agency[" + agencyId.ToString() + "]: " + "Placed Order " + orderString + " for Hotel[" + hotelId.ToString() +"]");

        }

        //method to handle the price cut event
        public void handlePriceCutEvent(int hotelId, int newRoomRate)
        {
            // assess new price and if good deal, place order
            price = newRoomRate;
            Console.WriteLine("Agency[" + agencyId.ToString() + "]: " + "Handling price cut event" + "for Hotel[" + hotelId.ToString() + "]");
            check_noOfRooms_toOrder();
            if (isGoodDeal(newRoomRate))
            {
                placeOrder(hotelId);
            }

        }

        //method for receiving confirmation of order
        public void handleOrederCompletedEvent(int agencyId, int hotelId)
        {
            if (this.agencyId != agencyId) return;

            Console.WriteLine("Agency[" + agencyId.ToString() + "]: " + "Received Order Confirmation from Hotel["+ hotelId.ToString() + "] at time: "+(DateTime.Now.ToString("h:mm:ss tt")));

        }

        //Starting the travel agency thread
        public void agencyThreadMain()
        {
             while(true)
             {
                // Console.WriteLine("Agency Thread Main: heartbeat");
                    Thread.Sleep(500);
                 
             }
        }

        //Logic to decide no of rooms to order based on price of the rooms
        public void check_noOfRooms_toOrder()
        {
            if ( price < 500)
            {
                Random rng = new Random();
                noOfRooms = rng.Next(0,500);
            }
            else if ( price < 600 && price >=500)
            {
                Random rng = new Random();
                noOfRooms = rng.Next(0,300);
            }
            else
            {
                Random rng = new Random();
                noOfRooms = rng.Next(0,100);
            }

         }

    }
}
