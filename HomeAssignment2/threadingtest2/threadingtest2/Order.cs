using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threadingtest2
{
    //Class for storing the order details 
    class Order
    {
        private int agencyId = 0;
        private int hotelId = 0;
        private int agencyCardNo = 0;
        private int noOfRooms = 0;
 
        //initializing the order class object with order details 
        public Order(int agencyId, int hotelId, int agencyCardNo, int noOfRooms)
        {
            this.agencyId = agencyId;
            this.hotelId = hotelId;
            this.agencyCardNo = agencyCardNo;
            this.noOfRooms = noOfRooms;
        }

        //Get and set methods for order
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

        public int NoOfRooms
        {
            get
            {
                return noOfRooms;
            }
            set
            {
                noOfRooms = value;
            }
        }

        //converting the order placed into a string
        public string toString()
        {
            return  this.agencyId.ToString() +"::" + this.hotelId.ToString() +"::" + this.agencyCardNo.ToString() +"::" + this.noOfRooms.ToString();
        }

        
    
    }
}
