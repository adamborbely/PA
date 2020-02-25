using System;
using System.Collections.Generic;

namespace com.codecool.api
{
    public class Shelf
    {
        public int id;
        private int capacity;
        private Size shelfSize;
        public List<Food> foodList;
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
        public Shelf() { }
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


