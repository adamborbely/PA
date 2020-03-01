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
            for (int i = 1; i < shelfContainer.Length; i++)//start from 1 because if the first fit cannot take the upper one out  
            {
                if (shelfContainer[i] != null && shelfContainer[i - 1] != null && shelfContainer[i].GetFreeSpace() > food.Size && shelfContainer[i - 1].NotContainsBigItem())
                {
                    shelfContainer[i].AddFood(food);
                    return RemoveShelf(i - 1);
                }

            }
            //if (shelfContainer.Contains(null))
            //{
            //    if (FindEmptyShelfPlace() + 3 < shelfContainer.Length)
            //    {
            //        shelfContainer[FindEmptyShelfPlace() + 3].AddFood(food);
            //        return RemoveShelf(FindEmptyShelfPlace() + 2);
            //    }
            //    throw new CannotCoolItException();
            //}
            //else
            //{
            //    shelfContainer[1].AddFood(food);
            //  return RemoveShelf(0);
            //}
            throw new FridgeIsFullException();
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
