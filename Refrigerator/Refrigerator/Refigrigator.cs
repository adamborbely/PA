using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace com.codecool.api
{
    abstract class Refigrigator
    {
        protected int numOfShelfs;
        protected Shelf[] shelfContainer;
        protected Size fridgeSize;
        private bool isOpen = false;

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
                foreach (var shelf in shelfContainer)
                {
                    if (shelf.AddFood(food))
                    {
                        break;
                    }
                    else
                    {
                        throw new FridgeIsFullException();
                    }
                }
            }
            throw new FridgeIsClosedException();
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
    internal class Shelf
    {
        private int id;
        private int capacity;
        private Size shelfSize;
        private List<Food> foodList;
        public Size ShelfSize
        {
            get { return shelfSize; }
        }
        public int ID
        {
            get { return id; }
        }
        public int Capacity
        {
            get { return capacity; }
        }

        public Shelf(int id, int capacity, Size size)
        {
            this.id = id;
            this.capacity = capacity;
            this.shelfSize = size;
            foodList = new List<Food>();
        }

        public int GetFreeSpace()
        {
            var result = capacity;
            foreach (var food in foodList)
            {
                result -= food.Size;
            }
            return result;
        }
        public bool AddFood(Food food)
        {
            if (GetFreeSpace() > food.Size)
            {
                foodList.Add(food);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Food RemoveFood(string name)
        {
            var result = FindFood(name);
            foodList.Remove(result);

            return result;
        }
        public bool ContainsFood(string name)
        {
            foreach (var food in foodList)
            {
                if (food.Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
        public Food FindFood(string name)
        {
            foreach (var food in foodList)
            {
                if (food.Name.Equals(name))
                {
                    return food;
                }
            }
            throw new FoodNotExistsException();
        }
    }
}


