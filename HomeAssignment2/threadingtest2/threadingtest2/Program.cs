using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace threadingtest2
{
    //Main Class
    class Program
    {

        public static Buffer orderBuffer = new Buffer(); //creating an object of the Buffer class

        public const int HOTEL_COUNT = 4;    //creating a constant variable for the number of hotels 
        public const int AGENCY_COUNT = 4;   //creating a constant variable for the number of travel agency

          
        static void Main(string[] args)
        {

            Hotel[] hotels = new Hotel[HOTEL_COUNT];  // creating the object of the hotel class
            Thread[] hotelThreadsToStart = new Thread[HOTEL_COUNT]; //creating the thread object of the hotel

            Agency[] agencies = new Agency[AGENCY_COUNT]; //creating the object of the travel agency
            Thread[] agencyThreadsToStart = new Thread[AGENCY_COUNT];//creating the thread object of the agent 

            bankService.Service1Client apply_service = new bankService.Service1Client(); //creating client for bank service
            
            OrderDispatcher orderDispatcher = new OrderDispatcher();



            Thread orderDispatcherThread = new Thread(orderDispatcher.processOrderQueue);
            
            //initialising the price cut thread 
            for (int i = 0; i < HOTEL_COUNT; i++)  
            {
                hotels[i] = new Hotel();
                hotels[i].HotelId = i;

                Thread hotelPriceCutThread = new Thread(hotels[i].calculateNewRoomRate);
                hotelThreadsToStart[i] = hotelPriceCutThread;
                //creating a delegate for the the OrderDispatcher class
                OrderDispatcher.orderReceivedEvent += new OrderDispatcher.orderEventDelegate(hotels[i].handleOrderReceivedEvent);
          
            }
            //initialising the bank service thread connecting to the travel agency
            for (int i = 0; i < AGENCY_COUNT; i++)
            {
                agencies[i] = new Agency();
                agencies[i].AgencyId = i; 

                int ccNumber = apply_service.applyCCNumber(); //providing agency with a new credit card number by calling bank service
                agencies[i].AgencyCardNo = ccNumber;

                agencyThreadsToStart[i] = new Thread(agencies[i].agencyThreadMain);

                Hotel.priceCutEvent += new Hotel.hotelEventDelegate(agencies[i].handlePriceCutEvent);
                Hotel.orderCompletedEvent += new Hotel.orderCompletedEventDelegate(agencies[i].handleOrederCompletedEvent);
            }
            //starting the travel agency thread
            for (int i = 0; i < AGENCY_COUNT; i++)
            {
                agencyThreadsToStart[i].Start();
            }
            

            orderDispatcherThread.Start();
            //starting the hotel threads
            for (int i = 0; i < HOTEL_COUNT; i++)
            {
                hotelThreadsToStart[i].Start();
            }

               
        
        }
    }
}
