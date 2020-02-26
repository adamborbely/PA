using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.codecool.api
{

    public class NormalRefig : Refigrigator
    {
        public NormalRefig(string name)
        {
            Name = name;
            numOfShelfs = 6;
            shelfContainer = new Shelf[numOfShelfs];
            for (int i = 0; i < numOfShelfs; i++)
            {
                shelfContainer[i] = new Shelf(i + 1, 150, Size.Normal);
            }
            fridgeSize = Size.Normal;
        }
        public NormalRefig() { }

        public override Shelf CoolBigItem(Food food)
        {
            if (shelfContainer.Contains(null))
            {
                if (FindEmptyShelfPlace() + 3 < shelfContainer.Length)
                {
                    shelfContainer[FindEmptyShelfPlace() + 3].AddFood(food);
                    return RemoveShelf(FindEmptyShelfPlace() + 2);
                }
                throw new CannotCoolItException();
            }
            else
            {
                shelfContainer[1].AddFood(food);
                return RemoveShelf(0);
            }
        }

        public override bool CoolSmallItem(Food food)
        {
            foreach (var shelf in shelfContainer)
            {
                if (shelf != null)
                {
                    if (shelf.AddFood(food))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
