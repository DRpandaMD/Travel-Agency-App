using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApp
{

    // This class will handle how orders are processed
    class OrderProcessing
    {
        static double SALES_TAX = 0.05; //AZ sales tax is 5%

        Order order; //declare and order object

        public OrderProcessing(Order order)
        {
            this.order = order;
        }

        public void processOrder()
        {
            if(validateCardNumber(order.getCardNumber()))
            {
                sendCompletedOrder(order, calcTotalPrice(order));
            }
        }

        private double calcTotalPrice(Order order)
        {
            double priceWithoutTaxes = order.getAmount() * order.getUnitPrice();
            double taxAmount = priceWithoutTaxes * SALES_TAX;
            return priceWithoutTaxes + taxAmount;
        }

        private bool validateCardNumber(int cardNumber)
        {
            if(5000 <= cardNumber && cardNumber <= 7000) //check to see if the cardnumber is between 5000 and 7000
            {
                return true;
            }
            return false;
        }

        private void sendCompletedOrder(Order order, double totalPrice)
        {
            Console.WriteLine("Completed a Order: {0}\t {1} tickets at ${2} each.\t The Total Cost is: ${3}",
                                order.getSenderId(), order.getAmount(), order.getUnitPrice(), totalPrice);
        }
    }
}
