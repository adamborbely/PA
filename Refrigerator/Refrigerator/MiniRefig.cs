using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    public class MiniRefig : Refigrigator
    {
        public MiniRefig(string name)
        {
            Name = name;
            numOfShelfs = 3;
            shelfContainer = new Shelf[] { new Shelf(1, 100, Size.Mini), new Shelf(2, 100, Size.Mini), new Shelf(3, 100, Size.Mini) };
            fridgeSize = Size.Mini;
        }
        public MiniRefig() { }

        public override Shelf CoolBigItem(Food food)
        {
            throw new CannotCoolItException();
        }

        public override bool CoolSmallItem(Food food)
        {

            foreach (var shelf in shelfContainer)
            {
                if (shelf.AddFood(food))
                {
                    return false;

                }
            }
            return true;
        }
        //public 
    }
}
