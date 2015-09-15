using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BankService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //List for storing the credit card number
        static List<int> ccNumberList = new List<int>();
        //new credit card number starts from 5000
        static int newCCNumber = 5000;

        //Method for giving a new credit card number to the travel agency
        public int applyCCNumber()
        {
            newCCNumber++;
            ccNumberList.Add(newCCNumber);
            return newCCNumber;
        }

        //Validation of credit card number is done here
        public string validateCCNumber(string ccNumber)
        {
            try
            {
                //Decryption of the credit card number done by the decryption service
                decryptService.ServiceClient decrypt_client = new decryptService.ServiceClient();
                String decrypted_ccNumber = decrypt_client.Decrypt(ccNumber);
                
                //parsing the credit card number string into integer
                int toCheck_ccNumber = int.Parse(decrypted_ccNumber);

                //checks if credit card no is in the list
                if (ccNumberList.Contains(toCheck_ccNumber))
                    return "valid";
                else
                    return "not valid";
            }
            catch (Exception e)
            {
                return "not valid";
            }
        }
    }
}
