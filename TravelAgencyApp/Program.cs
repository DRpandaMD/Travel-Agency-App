using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TravelAgencyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // declare a buffer to be used by our Hotel and Travel
            MultiCellBuffer buffer = new MultiCellBuffer();

            // This is an individual assignment K = 1
            Hotel hotel1 = new Hotel("Zarate's Fancy Hotel", buffer);
            Thread hotelThread = new Thread(hotel1.OperateHotel);  //singular hotel thread

            //Now I need 5 travel agency threads
            TravelAgency travelAgency1 = new TravelAgency("HotelPlanner", hotelThread, buffer);
            TravelAgency travelAgency2 = new TravelAgency("Vantage", hotelThread, buffer);
            TravelAgency travelAgency3 = new TravelAgency("BookIt", hotelThread, buffer);
            TravelAgency travelAgency4 = new TravelAgency("CheapOAir", hotelThread, buffer);
            TravelAgency travelAgency5 = new TravelAgency("Global Work & Travel", hotelThread, buffer);

            //now run the agency threads
            Thread agencyThread1 = new Thread(travelAgency1.operateAgency);
            Thread agencyThread2 = new Thread(travelAgency2.operateAgency);
            Thread agencyThread3 = new Thread(travelAgency3.operateAgency);
            Thread agencyThread4 = new Thread(travelAgency4.operateAgency);
            Thread agencyThread5 = new Thread(travelAgency5.operateAgency);

            //subscribe to the events
            hotel1.PriceCut += travelAgency1.OnPriceCut;
            hotel1.PriceCut += travelAgency2.OnPriceCut;
            hotel1.PriceCut += travelAgency3.OnPriceCut;
            hotel1.PriceCut += travelAgency4.OnPriceCut;
            hotel1.PriceCut += travelAgency5.OnPriceCut;

            //subscribe the hotel to the orderSubmit event in the buffer
            buffer.OrderSubmit += hotel1.OnOrderSubmitted;

            //start the threads
            hotelThread.Start();
            agencyThread1.Start();
            agencyThread2.Start();
            agencyThread3.Start();
            agencyThread4.Start();
            agencyThread5.Start();
        }
    }
}
