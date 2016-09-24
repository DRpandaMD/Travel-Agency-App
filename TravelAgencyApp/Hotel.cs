using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TravelAgencyApp
{
   
    class Hotel //equivilant to chickenfarm in the textbook
    {
        //our events and delegatsfor hotel class
        public delegate void PriceCutEventHandler(object sender, EventArgs e);
        public event PriceCutEventHandler PriceCut;

        // our data members
        private string hotelName;
        private int previousPrice;
        private int priceCutCounter;

        //reference to our buffer
        private MultiCellBuffer buffer;

        //class object constructor
        public Hotel(string name, MultiCellBuffer buffer)
        {
            this.hotelName = name;
            previousPrice = -1; // we are letting -1 show no price has yet to be set
            priceCutCounter = 0; // obviously we also need to initialize the count to 0
            this.buffer = buffer;
        }

        //getter
        public int getPreviousPrice()
        {
            return previousPrice;
        }
        
        //setter
        public void setPreviousPrice(int prevPrice)
        {
            this.previousPrice = prevPrice;
        }

        //Main Object methods
        public void OperateHotel()
        {
            while(priceCutCounter < 20)
            {
                int newPrice = pricingModel();
                Console.WriteLine("New price in: {0}", newPrice);
                if(checkPriceCut(newPrice))
                {
                    Console.WriteLine("ATTN:  ***PRICE CUT OCCURED***");
                    OnPriceCut(newPrice);
                }
                Thread.Sleep(500); 
            }
        }

        //Here is our pricing model as a function as part of the Hotel class
        //Since I am working solo this is as complex as the pricing needs to be.
        public int pricingModel()
        {
            Random rng = new Random();
            return rng.Next(50, 500); // according to the specs we are picking a random number between 50 and 500 
        }

        //we also need a method to check if the price has been dropped or not since the previous price
        public bool checkPriceCut(int price)
        {
            if(previousPrice == -1) //fresh object no price has been set
            {
                previousPrice = price;
                return false;
            }
            else if (price < previousPrice) // when the price is lower
            {
                priceCutCounter++;          // increment the counter
                return true;                // return true
            }
            previousPrice = price; //the price has gone up 
            return false;
        }

        // we also need to have a method that emits the price cut message
        public virtual void OnPriceCut(int newPrice)
        {
            //check for subscribers to the event
            if(PriceCut != null)
            {
                //create args and info to be sent to the TravelAgency
                PriceCutEventArgs args = new PriceCutEventArgs(previousPrice, newPrice);
                PriceCut(this, args); //emit the event
            }
        }

        //
        public void OnOrderSubmitted(object sender, EventArgs agrs)
        {
            try
            {
                //set up to get our data
                string encodedOrderString;
                Order decodedOrderObject;

                // here we have gotten the order out of the buffer and are ready to  make a thread to start processing
                encodedOrderString = buffer.getACell();
                //**TEST  Used to debug and issue with my string
                //Console.WriteLine("printing out the encodedOrderString from inside OnOrderSubmitted in the Hotel Class: {0}", encodedOrderString);
                //**TEST
                decodedOrderObject = Decoder.decodeOrder(encodedOrderString);
                OrderProcessing orderProcessor = new OrderProcessing(decodedOrderObject); //make an object for it

                //init thread
                Thread t1 = new Thread(orderProcessor.processOrder);// make a thread and start processOrder()
                t1.Start();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }


        /*
         * A sub class to create info about current and previous prices
         * Creates notification when price cut accures
         * Used to send price information whenever the event occures
         */

        public class PriceCutEventArgs : EventArgs //C# inheritance
        {
            int previousPrice;
            int newLowPrice;
            
            //class constructor
            public PriceCutEventArgs(int prev, int lowPrice)
            {
                this.previousPrice = prev;
                this.newLowPrice = lowPrice;
            }

            public int getPreviousPrice()
            {
                return previousPrice;
            }

            public int getLowPrice()
            {
                return newLowPrice;
            }
        }

    }
}
