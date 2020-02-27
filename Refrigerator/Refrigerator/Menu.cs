using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using com.codecool.api;

namespace com.codecool.cmd
{
    class Menu
    {
        private List<Refigrigator> refigrigators = new List<Refigrigator>();
        private List<Shelf> shelves = new List<Shelf>();
        private List<Food> foods = new List<Food>();
        private Ilogger logger;
        public int consumedCalories = 0;
        private bool inFridge = false;
        private MenuHelper mh = new MenuHelper();
        public List<Refigrigator> Refigrigators
        {
            get { return refigrigators; }
            set { refigrigators = value; }
        }
        public List<Shelf> Shelves
        {

            get { return shelves; }
        }
        public List<Food> Foods
        {
            get { return foods; }
        }

        public Menu(Ilogger logger)
        {
            this.logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Welcome to the FridgeSimulator! Press \n" +
                    "1) to create a new fridge\n" +
                    "2) to use a fridge\n" +
                    "3) to list all food outside of the fridges\n" +
                    "4) to list all available fridges\n" +
                    "5) to list all shelfes \n" +
                    "6) to delete a fridge \n" +
                    "7) to save\n" +
                    "8) to load previous\n" +
                    "9) to check consumed calories\n" +
                    "0) to exit");
                var ans = Console.ReadLine();
                if (ans == "1")
                {
                    try
                    {
                        var newFridge = AddRefig();

                    }
                    catch (FridgeAlreadyExistsException)
                    {
                        Console.Clear();
                        Console.WriteLine("Please choose an uniqe name for your fridge!");
                    }
                }
                else if (ans == "2")
                {
                    try
                    {
                        Console.WriteLine("Which Fridge you want to use?");
                        var fridgeName = Console.ReadLine();
                        var refig = mh.FindFrifgeByName(fridgeName, Refigrigators);
                        inFridge = true;
                        while (inFridge)
                        {
                            FridgeMenuLogic(refig, FridgeMenu(refig));
                        }
                    }
                    catch (NoSuchRefigrigatorException)
                    {

                        Console.WriteLine("There is no Fridge called like that!");
                    }
                }
                else if (ans == "3")
                {
                    foreach (var food in Foods)
                    {
                        Console.WriteLine(food.Name);
                    }
                }
                else if (ans == "4")
                {
                    foreach (var fridge in Refigrigators)
                    {
                        Console.WriteLine(fridge.Name);
                    }
                }
                else if (ans == "5")
                {
                    foreach (var shelf in Shelves)
                    {
                        Console.WriteLine($"The ID of the shelf: {shelf.ID}, the size is: {shelf.ShelfSize}  ");
                    }
                }
                else if (ans == "6")
                {
                    try
                    {
                        Console.WriteLine("Which Fridge you want to remove?");
                        var fridgeName = Console.ReadLine();
                        var refig = mh.FindFrifgeByName(fridgeName, Refigrigators);
                        Refigrigators.Remove(refig);
                    }
                    catch (NoSuchRefigrigatorException)
                    {

                        Console.WriteLine("There is no Fridge called like that!");
                    }
                }
                else if (ans == "7")
                {
                    //save
                    logger.Save(Refigrigators);
                }
                else if (ans == "8")
                {
                    //Load
                    Refigrigators = logger.Load("Inventory.xml");
                }
                else if (ans == "9")
                {
                    Console.Clear();
                    Console.WriteLine($"You consumed {consumedCalories} calories");
                }
                else if (ans == "0")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    Thread.Sleep(600);
                    Console.Clear();
                }
            }
        }

        public string FridgeMenu(Refigrigator refigrigator)
        {
            Console.WriteLine($"Welcome to the {refigrigator.Name} Fridge! Press\n" +
              $"1) to list all food in the fridge\n" +
              $"2) to open the fridge\n" +
              $"3) to add new food to the fridge\n" +
              $"4) to eat some food\n" +
              $"5) to check the free space in the fridge\n" +
              $"6) to check if the plaza is open or not\n" +
              $"7) to close the fridge\n" +
              $"0) to leave and close the fridge");
            return Console.ReadLine();
        }

        public void FridgeMenuLogic(Refigrigator refig, string option)
        {
            switch (option)
            {
                case "1":
                    Console.Clear();
                    try
                    {
                        if (refig.ListFood().Count > 0)
                        {
                            foreach (var food in refig.ListFood())
                            {
                                Console.WriteLine(food.Name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no food in the fridge");
                        }
                    }
                    catch (FridgeIsClosedException)
                    {
                        Console.WriteLine("Open the fridge first!"); ;
                    }
                    break;
                case "2":
                    refig.Open();
                    break;
                case "3":
                    try
                    {
                        if (refig.IsOpen())
                        {
                            Console.WriteLine("What is the name of the food?");
                            var foodName = Console.ReadLine();
                            Console.WriteLine("How many calories it has?");
                            var calories = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("What is the size of the food?");
                            var size = Convert.ToInt32(Console.ReadLine());
                            refig.AddFood(new Food(calories, size, foodName));

                            if (refig.RemovedShelf != null)
                            {
                                shelves.Add(refig.RemovedShelf);
                                foods.AddRange(refig.RemovedShelf.foodList);
                                refig.RemovedShelf.foodList = new List<Food>();
                                refig.RemovedShelf = null;
                            }
                        }
                        else
                        {
                            throw new FridgeIsClosedException();
                        }
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Invalid input - calories and size must be numbers");
                    }
                    catch (FridgeIsClosedException)
                    {
                        Console.WriteLine("Open the fridge first!");
                    }
                    catch (FridgeIsFullException)
                    {
                        Console.WriteLine("This fridge is full!");
                    }
                    catch (CannotCoolItException)
                    {
                        Console.WriteLine("The size of the food not good for this fridge.");
                    }
                    break;
                case "4":
                    try
                    {
                        Console.WriteLine("What you want to eat?");
                        var foodToEat = Console.ReadLine();
                        consumedCalories += refig.ConsumeFood(foodToEat);
                    }
                    catch (FoodNotExistsException)
                    {

                        Console.WriteLine("You have no food called like that!");
                    }
                    catch (FridgeIsClosedException)
                    {

                        Console.WriteLine("Open the fridge first!");
                    }

                    break;
                case "7":
                    refig.Close();
                    break;
                case "0":
                    refig.Close();
                    inFridge = false;
                    break;

            }
        }
        public Refigrigator AddRefig()
        {
            while (true)
            {
                Console.WriteLine("Press \n1) to create a Mini Frige\n2) to create a Normal Fridge\n3) to create a Double Door Fridge");
                var fridgeSize = Console.ReadLine();
                Console.Clear();
                if (fridgeSize == "1")
                {
                    Console.WriteLine("Give a name to your Mini Fridge!");
                    var fridgeName = Console.ReadLine();
                    if (mh.CheckRefigAlreadyExist(Refigrigators, fridgeName))
                    {
                        var fridge = new MiniRefig(fridgeName);
                        refigrigators.Add(fridge);
                        return fridge;
                    }
                    else
                    {
                        throw new FridgeAlreadyExistsException();
                    }

                }
                else if (fridgeSize == "2")
                {
                    Console.WriteLine("Give a name to your Normal Fridge!");
                    var fridgeName = Console.ReadLine();
                    if (mh.CheckRefigAlreadyExist(Refigrigators, fridgeName))
                    {
                        var fridge = new NormalRefig(fridgeName);
                        refigrigators.Add(fridge);
                        return fridge;
                    }
                    else
                    {
                        throw new FridgeAlreadyExistsException();
                    }
                }
                else if (fridgeSize == "3")
                {
                    Console.WriteLine("Give a name to your Double Door Fridge!");
                    var fridgeName = Console.ReadLine();
                    if (mh.CheckRefigAlreadyExist(Refigrigators, fridgeName))
                    {
                        var fridge = new DoubleRefig(fridgeName);
                        refigrigators.Add(fridge);
                        return fridge;
                    }
                    else
                    {
                        throw new FridgeAlreadyExistsException();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    Thread.Sleep(600);
                    Console.Clear();
                }
            }
        }
    }

    internal class MenuHelper
    {
        public Refigrigator FindFrifgeByName(string name, List<Refigrigator> Refigrigators)
        {

            foreach (var refig in Refigrigators)
            {
                if (name == refig.Name)
                {
                    return refig;
                }
            }
            throw new NoSuchRefigrigatorException();
        }
        public bool CheckRefigAlreadyExist(List<Refigrigator> refigs, string name)
        {
            foreach (var fridge in refigs)
            {
                if (fridge.Name.Equals(name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
