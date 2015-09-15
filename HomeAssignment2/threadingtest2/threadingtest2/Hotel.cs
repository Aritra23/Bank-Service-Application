using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace threadingtest2
{
    //Class for hotel supplier details
    class Hotel
    {
        public delegate void hotelEventDelegate(int hotelId, int newRoomRate);
        public static event hotelEventDelegate priceCutEvent;

        public delegate void orderCompletedEventDelegate(int agencyId, int hotelId);
        public static event orderCompletedEventDelegate orderCompletedEvent;



        private int hotelId = 0;
        private int currentRoomRate = 99999;
        private const int PRICE_CUT_COUNT_LIMIT = 10;//initialising the constant value for the number of times the price cut event will be called
        private int count=0;
        private int totalNoRooms = 1000;
        Random rng = new Random();
    
        //method for calculating the new rates of hotel rooms   
        public void calculateNewRoomRate()
        {
            while (count <= PRICE_CUT_COUNT_LIMIT)   
            {
                
                int newRoomRate;
                string day = DateTime.Now.DayOfWeek.ToString();

                //Logic for number of rooms available-> depends on weekends and depends on no of room available
                if (day == "Sunday" && totalNoRooms <= 500)
                {
                    newRoomRate = rng.Next(600, 800);

                }
                else if (day == "Saturday" && totalNoRooms <= 500)
                {
                    //Random rng = new Random();
                    newRoomRate = rng.Next(600, 700);

                }

                else if (day == "Sunday" && totalNoRooms > 500)
                {
                    //Random rng = new Random();
                    newRoomRate = rng.Next(500, 650);
                }

                else if (day == "Saturday" && totalNoRooms > 500)
                {
                    //Random rng = new Random();
                    newRoomRate = rng.Next(500, 600);

                }

                else
                {
                    //Random rng = new Random();
                    newRoomRate = rng.Next(400, 550);
                }

                Console.WriteLine("Hotel[" + hotelId.ToString() + "]:" + "Room Rate: " + newRoomRate);
                //Console.WriteLine("Hotel[" + hotelId.ToString() + "]: " + "Calculated new room rate");

                //launching the price cut event
                if (newRoomRate < currentRoomRate)
                {
                    if (priceCutEvent != null)
                    {
                        Console.WriteLine("Hotel[" + hotelId.ToString() + "]: " + "Generating Price Cut Event "+count);
                        
                        priceCutEvent(hotelId, newRoomRate);
                        count++;
                       
                    }

                }

                currentRoomRate = newRoomRate;
                
                              
            }
        }


        //generating the getter and setter methods
        public int HotelId
        {
            get
            {
                return hotelId;
            }
            set
            {
                hotelId = value;
            }
        }

        public int CurrentRoomRate
        {
            get
            {
                return currentRoomRate;
            }
            set
            {
                currentRoomRate = value;
            } 
        }

        //handling the order sent by the travel agency to the hotel
        public void handleOrderReceivedEvent(int hotelId, string orderString)
        {

            if (this.hotelId != hotelId) return;   //check whether the hotel id is correct or not

            // spawn a thread to process order

            processSingleOrder(orderString);

        }

//method for processing the order
        public void processSingleOrder(string orderString)  
        {
            string[] lines = Regex.Split(orderString, "::");

            int agencyId = Convert.ToInt32(lines[0]);
            int hotelId = Convert.ToInt32(lines[1]);
            int agencyCardNo = Convert.ToInt32(lines[2]);
            int noOfRooms = Convert.ToInt32(lines[3]);

            //calling encryption service and encrypting the credit card number
            encryptService.ServiceClient encrypt_client = new encryptService.ServiceClient(); 
            string ccNumber = agencyCardNo.ToString();
            string encrypted_ccNumber = encrypt_client.Encrypt(ccNumber);

            //verification of the credit card number by the bank service
            bankService.Service1Client bank_client = new bankService.Service1Client();
            string validation = bank_client.validateCCNumber(encrypted_ccNumber);

            if (validation == "valid")
            {
                Console.WriteLine("Hotel[" + hotelId.ToString() + "]: " + "Processing Single Order: " + orderString);

                if (orderCompletedEvent != null)
                {
                    
                    //checks if no of rooms ordered greater than available rooms
                    if (noOfRooms > totalNoRooms)
                    {
                        Console.WriteLine("Sorry.Rooms Not available in hotel[" + hotelId.ToString() + "]" + " for order by agency[" + agencyId.ToString() + "]");
                    }
                    else
                    {
                        totalNoRooms = totalNoRooms - noOfRooms;
                        Console.WriteLine("Hotel[" + hotelId.ToString() + "]:" + "Rooms available: " + totalNoRooms);
                        orderCompletedEvent(agencyId, hotelId);
                    }


                }

            }
            else
            {
                Console.WriteLine("The credit card number is not valid.Denied placing the order");
            }
        }
        



    }
}
