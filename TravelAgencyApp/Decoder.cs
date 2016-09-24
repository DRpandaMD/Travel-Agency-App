using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApp
{
    //This class will serve as the Decoder of order string messages
    class Decoder
    {
        public static Order decodeOrder(string orderIn)
        {
            // first we need to split up the string and put the elements into an array
            string[] parts = orderIn.Split(',');  //**TEST found a bug here where the string was being split by '.' not ','

            //put the parts of the string into the object
            string id = parts[0];
            int cardNumber = Convert.ToInt32(parts[1]);
            int amount = Convert.ToInt32(parts[2]);
            double unitCost = Convert.ToDouble(parts[3]);
            return new Order(id, cardNumber, amount, unitCost);
        }
    }
}
