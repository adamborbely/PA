using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    class MiniRefig : Refigrigator
    {
        public MiniRefig()
        {
            numOfShelfs = 3;
            shelfContainer = new Shelf[] { new Shelf(1, 100, Size.Mini), new Shelf(2, 100, Size.Mini), new Shelf(2, 100, Size.Mini) };
            fridgeSize = Size.Mini;
        }

        public override Shelf CoolBigItem(Food food)
        {
            throw new NotImplementedException();
            //throw new CannotCoolItException();
        }

        public override void CoolSmallItem(Food food)
        {
            throw new NotImplementedException();
        }
        //public 
    }
}
