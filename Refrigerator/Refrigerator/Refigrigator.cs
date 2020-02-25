using System;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace com.codecool.api
{
    [XmlInclude(typeof(MiniRefig))]
    [XmlInclude(typeof(NormalRefig))]
    [XmlInclude(typeof(DoubleRefig))]
    [XmlInclude(typeof(Shelf))]
    [XmlInclude(typeof(Food))]
    public abstract class Refigrigator
    {
        public int numOfShelfs;
        public Shelf[] shelfContainer;
        public Size fridgeSize;
        public bool isOpen = false;

        //protected Refigrigator(int numOfShelfs, Size size)
        //{
        //    this.numOfShelfs = numOfShelfs;
        //    shelfContainer = new Shelf[numOfShelfs];
        //    this.fridgeSize = size;
        //}
        //public Refigrigator()
        //{

        //}
        public abstract Shelf CoolBigItem(Food food);
        public abstract void CoolSmallItem(Food food);
        public int FindEmptyShelfPlace()
        {
            if (shelfContainer.Contains(null))
            {
                return Array.IndexOf(shelfContainer, null);
            }
            else
            {
                throw new NoEmptyShelfPlaceException();
            }
        }
        public void AddShelf(Shelf shelf)
        {
            if (isOpen)
            {
                if (fridgeSize.Equals(shelf.ShelfSize))
                {
                    shelfContainer[FindEmptyShelfPlace()] = shelf;
                }
                else
                {
                    throw new SizeNotCompatableException();
                }
            }
            else
            {
                throw new FridgeIsClosedException();
            }
        }
        public Shelf RemoveShelf(int number)
        {
            if (isOpen)
            {
                var ShelfToRemove = shelfContainer[number];
                shelfContainer[number] = null;
                return ShelfToRemove;
            }
            throw new FridgeIsClosedException();
        }

        public void AddFood(Food food)
        {
            if (isOpen)
            {
                var isFull = true;
                foreach (var shelf in shelfContainer)
                {
                    if (shelf.AddFood(food))
                    {
                        isFull = false;
                        break;
                    }
                    //else
                    //{
                    //    
                    //}
                }
                if (isFull)
                {
                    throw new FridgeIsFullException();
                }
            }
            else
            {
                throw new FridgeIsClosedException();
            }
        }
        public int ConsumeFood(string name)
        {
            if (isOpen)
            {
                foreach (var shelf in shelfContainer)
                {
                    if (shelf.ContainsFood(name))
                    {
                        var calories = shelf.FindFood(name).Calories;
                        shelf.RemoveFood(name);
                        return calories;
                    }
                }
                throw new FoodNotExistsException();
            }
            throw new FridgeIsClosedException();
        }
        public void Open()
        {
            isOpen = true;
        }
        public void Close()
        {
            isOpen = false;
        }
        public bool IsOpen()
        {
            return isOpen;
        }
    }
}


