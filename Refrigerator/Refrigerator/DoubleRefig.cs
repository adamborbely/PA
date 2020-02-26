using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    public class DoubleRefig : Refigrigator
    {
        public DoubleRefig(string name)
        {
            Name = name;
            numOfShelfs = 10;
            shelfContainer = new Shelf[numOfShelfs];
            for (int i = 0; i < numOfShelfs; i++)
            {
                shelfContainer[i] = new Shelf(i + 1, 170, Size.DoubleDoor);
            }
            fridgeSize = Size.DoubleDoor;
        }
        public DoubleRefig() { }

        public override Shelf CoolBigItem(Food food)
        {
            throw new NotImplementedException();
        }

        public override bool CoolSmallItem(Food food)
        {
            throw new CannotCoolItException();
        }
    }
}