using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TravelAgencyApp
{
    class MultiCellBuffer
    {
        //declare delegates and events
        public delegate void OrderSubmitHandler(object sender, EventArgs e);
        public event OrderSubmitHandler OrderSubmit;

        //Data members for our MultiCellBuffer
        private static Semaphore _semPool;
        //add buffer here
        private TypeBuffer buffer;

        public MultiCellBuffer()
        {
            buffer = new TypeBuffer();
            _semPool = new Semaphore(2, 2); //make a semaphore with two open slots
        }

        //function emits an OrderSubmit event when called
        public virtual void OnOrderSubmit()
        {
            if(OrderSubmit != null)
            {
                OrderSubmit(this, EventArgs.Empty);
            }
        }

        //function sets on cell of the buffer to take the argument string
        public void setACell(string encodedOrderString)
        {
            //Let the semaphore help get access to cell data
            _semPool.WaitOne(); //wait 
            ReaderWriterLock readWriteLock = new ReaderWriterLock();
            readWriteLock.AcquireReaderLock(Timeout.Infinite); //lockout indefinitaly
            buffer.setCell(encodedOrderString); //we want to write to the cell with out interuption 
            readWriteLock.ReleaseReaderLock(); // release the lock
            _semPool.Release(); //exits the semaphore and releases the previous count
            OnOrderSubmit(); //emit the event out to signal an order has been submitted to the MultiCellBuffer
        }

        //function gets a cell of the buffer using semaphore access
        public string getACell()
        {
            string returnString;
            _semPool.WaitOne(); //blocks the thread and waits
            ReaderWriterLock readWriteLock = new ReaderWriterLock();
            readWriteLock.AcquireReaderLock(Timeout.Infinite);
            returnString = buffer.getCell();// get a cell that has stuff in it
            readWriteLock.ReleaseLock();
            _semPool.Release();
            return returnString;
        }
    }
}
