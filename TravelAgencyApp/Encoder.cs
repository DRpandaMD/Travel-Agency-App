using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TravelAgencyApp
{
    //This class serves as a converter for an object into a string
    class Encoder
    {
        //encodes the order into a string to be sent.
        public static string encodeOrder(Order order)
        {
            string encodedOrderStringOut = string.Format("{0}, {1}, {2}, {3}", order.getSenderId(), order.getCardNumber(), order.getAmount(), order.getUnitPrice());
            return encodedOrderStringOut;
        }
    }
}
