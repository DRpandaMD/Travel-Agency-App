using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TravelAgencyApp
{
    //I made a seperate Buffer class to handle defining what a buffer is for the mulitCellBuffer class
    class TypeBuffer
    {
        //data members
        private string cellOne;
        private string cellTwo;
        private bool cellOneFull;
        private bool cellTwoFull;

        //set a cell if the cells has no undread strings
        public bool setCell(string stringIn)
        {
            //check first cell
            if (!cellOneFull)
            {
                cellOne = stringIn;
                cellOneFull = true;
                return true;
            }
            else if (!cellTwoFull)
            {
                cellTwo = stringIn;
                cellTwoFull = true;
                return true;
            }
            return false;
        }

        //we also need to get a string from a cell and make sure it sets a flag marking it has been read.
        public string getCell()
        {
            if(cellOneFull)  // if it is full
            {
                cellOneFull = false; //set flag saying its empty
                return cellOne; // return that cell
            }
            else if(cellTwoFull)
            {
                cellTwoFull = false;
                return cellTwo;
            }
            return null; //return null out otherwise
        }
    }
}
