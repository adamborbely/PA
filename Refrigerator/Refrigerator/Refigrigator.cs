using System;
using System.Collections.Generic;
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
        public string Name;
        public int numOfShelfs;
        public Shelf[] shelfContainer;
        public Size fridgeSize;
        private bool isOpen = false;
        public Shelf RemovedShelf = null;

        public abstract Shelf CoolBigItem(Food food);
        public abstract bool CoolSmallItem(Food food);


        public void AddShelf(Shelf shelf)
        {
            if (isOpen)
            {
                if (fridgeSize.Equals(shelf.ShelfSize))
                {
                    if (shelfContainer[shelf.ID - 1] == null)
                    {
                        if (shelfContainer[shelf.ID].NotContainsBigItem())
                        {
                            shelfContainer[shelf.ID - 1] = shelf;
                        }
                        else
                        {
                            throw new BigItemCoolingException();
                        }
                    }
                    else
                    {
                        throw new SizeNotCompatableException();
                    }
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
                if (number < numOfShelfs)
                {
                    if (shelfContainer[number] != null)
                    {
                        var ShelfToRemove = shelfContainer[number];
                        shelfContainer[number] = null;
                        return ShelfToRemove;
                    }
                    else
                    {
                        throw new ShelfAlreadyRemovedException();
                    }
                }
                else
                {
                    throw new NotEnoughShelfException();
                }
            }
            throw new FridgeIsClosedException();
        }

        public void AddFood(Food food)
        {
            if (isOpen)
            {
                if (food._size < CheckMaxShelfCapacity())
                {
                    var isFull = true;
                    if (food.Size < 20)
                    {
                        isFull = CoolSmallItem(food);
                    }
                    else if (food.Size > 80)
                    {
                        RemovedShelf = CoolBigItem(food);
                        isFull = false;
                    }
                    else
                    {
                        foreach (var shelf in shelfContainer)
                        {
                            if (shelf != null)
                            {
                                if (shelf.AddFood(food))
                                {
                                    isFull = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (isFull)
                    {
                        throw new FridgeIsFullException();
                    }
                }
                else
                {
                    throw new BigItemCoolingException();
                }
            }
            else
            {
                throw new FridgeIsClosedException();
            }
        }
        public Food GetFoodFromFridge(string name)
        {
            if (isOpen)
            {
                foreach (var shelf in shelfContainer)
                {
                    if (shelf != null)
                    {
                        if (shelf.ContainsFood(name))
                        {
                            var resultFood = shelf.FindFood(name);
                            shelf.RemoveFood(name);
                            return resultFood;
                        }
                    }
                }
                throw new FoodNotExistsException();
            }
            throw new FridgeIsClosedException();
        }

        public List<Food> ListFood()
        {
            if (isOpen)
            {
                var result = new List<Food>();

                foreach (var shelf in shelfContainer)
                {
                    if (shelf != null)
                    {
                        result.AddRange(shelf.foodList);
                    }
                }
                return result;
            }
            throw new FridgeIsClosedException();
        }
        public int GetFreeSpace()
        {
            if (isOpen)
            {
                var count = 0;
                foreach (var shelf in shelfContainer)
                {
                    if (shelf != null)
                    {
                        count += shelf.GetFreeSpace();
                    }
                }
                return count;
            }
            else
            {
                throw new FridgeIsClosedException();
            }
        }
        public List<int> FindEmptySlots()
        {
            var empty = shelfContainer.Select((value, index) => new { value, index })
                      .Where(pair => pair.value == null)
                      .Select(pair => pair.index)
                      .ToList();
            if (empty.Count > 0)
            {
                return empty;
            }
            throw new NoEmptyShelfPlaceException();
        }

        public int CheckMaxShelfCapacity()
        {
            foreach (var shelf in shelfContainer)
            {
                if (shelf != null)
                {
                    return shelf.capacity;
                }
            }
            throw new NoShelfInTheFridgeException();
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


