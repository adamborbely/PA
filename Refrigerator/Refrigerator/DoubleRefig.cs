using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    class DoubleRefig : Refigrigator
    {
        public DoubleRefig()
        {
            numOfShelfs = 10;
            shelfContainer = new Shelf[numOfShelfs];
            for (int i = 0; i < numOfShelfs; i++)
            {
                shelfContainer[i] = new Shelf(i + 1, 170, Size.DoubleDoor);
            }
            fridgeSize = Size.DoubleDoor;
        }

        public override Shelf CoolBigItem(Food food)
        {
            throw new NotImplementedException();
        }

        public override void CoolSmallItem(Food food)
        {
            throw new NotImplementedException();
        }
    }
}