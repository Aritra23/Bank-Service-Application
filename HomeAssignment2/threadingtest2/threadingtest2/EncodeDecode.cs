using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace threadingtest2
{
    //Class for implementing the encryption and decryption of the order placed
    class EncodeDecode
    {
 
        //Encoding the order
        public static String encodeOrder(string orderObjectString, string Key)
        {
            string resultEncode = null;
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(orderObjectString);
            try
            {
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(Key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                resultEncode = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
            }
            return resultEncode;
            
        }

        //Decoding the order
        public static string decodeOrder(string orderObjectEncodedString, string Key)
        {
            string resultDecode = null;
            
            Byte[] inputArray = Convert.FromBase64String(orderObjectEncodedString);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(Key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            resultDecode = UTF8Encoding.UTF8.GetString(resultArray);

            return resultDecode;
        }
    
    }
}
