using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.codecool.api
{
    class NormalRefig : Refigrigator
    {
        public NormalRefig()
        {
            numOfShelfs = 6;
            shelfContainer = new Shelf[numOfShelfs];
            for (int i = 0; i < numOfShelfs; i++)
            {
                shelfContainer[i] = new Shelf(i + 1, 150, Size.Normal);
            }
            fridgeSize = Size.Normal;
        }

        public override Shelf CoolBigItem(Food food)
        {
            if (shelfContainer.Contains(null))
            {
                if (FindEmptyShelfPlace() + 2 < shelfContainer.Length)
                {
                    return RemoveShelf(FindEmptyShelfPlace() + 2);
                }
                throw new CannotCoolItException();
            }
            else
            {
                return RemoveShelf(0);
            }
        }

        public override void CoolSmallItem(Food food)
        {
            AddFood(food);
        }
    }
}
