using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TravelAgencyApp
{
    //This class will handle the operations of a travel agenecy
    class TravelAgency
    {
        // class data members
        private string agencyName;
        private Thread hotel;
        private MultiCellBuffer buffer;
        private bool priceCutOccured;
        private EventArgs currentEventArgs;
        private List<Order> completedOrders;

        // class object constructor

        public TravelAgency(string agencyName, Thread hotel, MultiCellBuffer buffer)
        {
            this.agencyName = agencyName;
            this.hotel = hotel;
            this.completedOrders = new List<Order>();
            this.buffer = buffer;
            this.priceCutOccured = false;
        }

        //Price cut handler
        public void OnPriceCut(object sender, EventArgs args)
        {
            priceCutOccured = true;
            currentEventArgs = args;
        }

        //The only spec here is in Order Processing range (5k - 7k)  Included numbers below and above to see it fail
        public int genCreditCardNumber()
        {
            Random rng = new Random();
            return rng.Next(4000, 8000);
        }

        /*
         * This is our operational function.  This function runs inside of a thread and will remain alive until 
         * all of the hotel threads have finshed.  This function also checks if a price cut has occured by chekcing the value of
         * "priceCutOccured".  If a price cut has occured then this function also creates an Order and sends it to the multicell buffer
         * 
         */
        public void operateAgency()
        {
            // Run this thread while the Hotel Thread still lives
            while (hotel.IsAlive)
            {
                if (priceCutOccured)
                {
                    //get the prev price and the new price to calculate ticket need
                    int previousPrice = ((Hotel.PriceCutEventArgs)currentEventArgs).getPreviousPrice();
                    int lowPrice = ((Hotel.PriceCutEventArgs)currentEventArgs).getLowPrice();
                    int ticketNeed = calculateTicketNeed(previousPrice, lowPrice);
                    Order order = new Order(agencyName, genCreditCardNumber(), ticketNeed, lowPrice);
                    string encodedOrderString = Encoder.encodeOrder(order);
                    //lock the buffer preventing anyother thread from writing into it
                    lock(buffer)
                    {
                        buffer.setACell(encodedOrderString);
                    }
                    //reset the priceCutOccured data member since we are submiting a new order to the buffer
                    priceCutOccured = false;
                }
            }

        }

        //Method to calculate the ticket need
        public int calculateTicketNeed(int prevPrice, int currentPrice)
        {
            int priceDifference = prevPrice - currentPrice;
            Random rng = new Random(DateTime.Now.Millisecond);
            int ticketNeed = 0;

            //prices range from $50 - $500
            if (0 < priceDifference && priceDifference <= 50)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (51 < priceDifference && priceDifference <= 100)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (101 < priceDifference && priceDifference <= 150)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (151 < priceDifference && priceDifference <= 200)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (201 < priceDifference && priceDifference <= 250)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (251 < priceDifference && priceDifference <= 300)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (301 < priceDifference && priceDifference <= 350)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (351 < priceDifference && priceDifference <= 400)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }
            else if (401 < priceDifference && priceDifference <= 500)
            {
                ticketNeed = RandomNeedGenerator.next(1, 15);
            }

            return ticketNeed;
        }
    }

    //We also need a need generator that will help us generate how many to order up
    static class RandomNeedGenerator
    {
        static Random rng = new Random();
        public static int next (int lowNum, int highNum)
        {
            return rng.Next(lowNum, highNum);
        }
    }

}

