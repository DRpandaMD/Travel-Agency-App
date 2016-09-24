using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApp
{   
    //This class servers to organize our order between our systems
    class Order
    {
        private string senderID;        //name of the hotel 
        private int cardNumber;         //credit card number
        private int amount;             // number of Rooms
        private double unitPrice;       //the price of the room reiceved from the hotel.


        //initialize the order object
        public Order(string id, int card, int amt, double price)
        {
            this.senderID = id;
            this.cardNumber = card;
            this.amount = amt;
            this.unitPrice = price;
        }

        //set up getters and setters
        public void setSenderId(string id)
        {
            this.senderID = id;
        }

        public string getSenderId()
        {
            return senderID;
        }

        public void setCardNumber(int card)
        {
            this.cardNumber = card;
        }

        public int getCardNumber()
        {
            return cardNumber;
        }

        public void setAmount(int amt)
        {
            this.amount = amt;
        }

        public int getAmount()
        {
            return amount;
        }

        public void setUnitPrice(double price)
        {
            this.unitPrice = price;
        }

        public double getUnitPrice()
        {
            return unitPrice;
        }

        //we also need to overide the class ToString method
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", senderID, cardNumber, amount, unitPrice);
        }
    }
}
