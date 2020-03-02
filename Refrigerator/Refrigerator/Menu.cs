using System;
using System.Collections.Generic;
using System.IO;
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
            set { shelves = value; }
        }
        public List<Food> Foods
        {
            get { return foods; }
            set { foods = value; }
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
                    " 1) to create a new fridge\n" +
                    " 2) to use a fridge\n" +
                    " 3) to list all food on the table\n" +
                    " 4) to list all available fridges\n" +
                    " 5) to list all shelfes \n" +
                    " 6) to delete a fridge \n" +
                    " 7) to save\n" +
                    " 8) to load previous\n" +
                    " 9) to check consumed calories\n" +
                    "10) to eat food from table\n" +
                    "11) to throw out food from table\n" +
                    "12) to throw out a shelf\n" +
                    " 0) to exit");
                var ans = Console.ReadLine();
                if (ans == "1")
                {
                    Console.Clear();
                    try
                    {
                        var newFridge = AddRefig();
                        Console.WriteLine($"You have created a new {newFridge.fridgeSize} fridge it has {newFridge.shelfContainer.Length} shelves, {newFridge.shelfContainer[0].Capacity} unit free space each!");

                    }
                    catch (FridgeAlreadyExistsException)
                    {
                        Console.WriteLine("Please choose an uniqe name for your fridge!");
                    }
                }
                else if (ans == "2")
                {
                    Console.Clear();
                    try
                    {
                        if (Refigrigators.Count > 0)
                        {
                            Console.WriteLine("Which Fridge you want to use?");
                            var fridgeName = Console.ReadLine();
                            var refig = mh.FindFrifgeByName(fridgeName, Refigrigators);
                            inFridge = true;
                            Console.Clear();
                            while (inFridge)
                            {
                                FridgeMenuLogic(refig, FridgeMenu(refig));
                            }
                        }
                        Console.WriteLine("You have no fridge yet!");
                    }
                    catch (NoSuchRefigrigatorException)
                    {

                        Console.WriteLine("There is no Fridge called like that!");
                    }
                }
                else if (ans == "3")
                {
                    Console.Clear();
                    if (Foods.Count > 0)
                    {
                        foreach (var food in Foods)
                        {
                            if (food.Size > 80)
                            {
                                Console.WriteLine($"{food.Name} it's a big item!");
                            }
                            else
                            {
                                Console.WriteLine(food.Name);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no foods on the table!");
                    }
                }
                else if (ans == "4")
                {
                    Console.Clear();
                    if (Refigrigators.Count > 0)
                    {
                        foreach (var fridge in Refigrigators)
                        {
                            Console.WriteLine(fridge.Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no fridges yet!");
                    }
                }
                else if (ans == "5")
                {
                    Console.Clear();
                    if (Shelves.Count > 0)
                    {
                        foreach (var shelf in Shelves)
                        {
                            Console.WriteLine($"The ID of the shelf: {shelf.ID}, the size is: {shelf.ShelfSize}  ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No shelves outside of the fridge!");
                    }
                }
                else if (ans == "6")
                {
                    Console.Clear();
                    try
                    {
                        if (Refigrigators.Count > 0)
                        {
                            Console.WriteLine("Which Fridge you want to remove?");
                            var fridgeName = Console.ReadLine();
                            var refig = mh.FindFrifgeByName(fridgeName, Refigrigators);
                            Refigrigators.Remove(refig);
                            Console.WriteLine($"The {fridgeName} has been removed!");
                        }
                        else
                        {
                            Console.WriteLine("You have no fridge yet!");
                        }
                    }
                    catch (NoSuchRefigrigatorException)
                    {
                        Console.WriteLine("There is no Fridge called like that!");
                    }
                }
                else if (ans == "7")
                {
                    Console.Clear();
                    //save
                    logger.Save(Refigrigators);
                    if (Shelves.Count > 0)
                    {
                        logger.SaveShelf(Shelves, "shelfInventory.xml");
                    }
                    if (Foods.Count > 0)
                    {
                        logger.SaveFood(Foods, "foodInventory.xml");
                    }
                    Console.WriteLine("We saved your work!");
                }
                else if (ans == "8")
                {
                    //Load
                    Console.Clear();
                    try
                    {
                        Refigrigators = logger.Load("Inventory.xml");
                        if (File.Exists("shelfInventory.xml"))
                        {
                            Shelves = logger.LoadShelf("shelfInventory.xml");
                        }
                        if (File.Exists("foodInventory.xml"))
                        {
                            Foods = logger.LoadFood("foodInventory.xml");
                        }
                        Console.WriteLine("We loaded your previous simulation");
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.WriteLine("No previous save!");
                    }
                }
                else if (ans == "9")
                {
                    Console.Clear();
                    Console.WriteLine($"You consumed {consumedCalories} calories");
                }
                else if (ans == "10")
                {
                    Console.Clear();
                    try
                    {
                        if (Foods.Count > 0)
                        {
                            Console.WriteLine("What you want to eat?");
                            var foodToEatFromTable = Console.ReadLine();
                            consumedCalories += mh.ConsumFoodFromTable(Foods, foodToEatFromTable);
                            Console.WriteLine($"You have eat {foodToEatFromTable} from table!");
                        }
                        else
                        {
                            Console.WriteLine("There are no foods on the table!");
                        }
                    }
                    catch (FoodNotExistsException)
                    {
                        Console.WriteLine("You have no food called like that in the table!");
                    }
                }
                else if (ans == "11")
                {
                    Console.Clear();
                    try
                    {
                        if (Foods.Count > 0)
                        {
                            Console.WriteLine("What you want to throw out?");
                            var foodToRemoveFromTable = Console.ReadLine();
                            mh.ConsumFoodFromTable(Foods, foodToRemoveFromTable);
                            Console.WriteLine($"You have thrown out {foodToRemoveFromTable} from table!");
                        }
                        else
                        {
                            Console.WriteLine("There are no foods on the table!");
                        }
                    }
                    catch (FoodNotExistsException)
                    {
                        Console.WriteLine("You have no food called like that in the table!");
                    }
                }
                else if (ans == "12")
                {
                    Console.Clear();
                    try
                    {
                        if (Shelves.Count > 0)
                        {
                            Console.WriteLine("Which shelf you want to remove?\n(If you still have the fridge better to keep the shelf, it can't be recreate later!)\n");
                            for (int i = 0; i < Shelves.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}) {Shelves[i].ShelfSize} shelf.");
                            }
                            var shelfToRemove = Convert.ToInt32(Console.ReadLine());
                            Shelves.Remove(Shelves[shelfToRemove - 1]);
                            Console.WriteLine("You removed a shelf!");
                        }
                        else
                        {
                            Console.WriteLine("No shelf outside!");
                        }
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Invalid input!");
                    }
                }
                else if (ans == "0")
                {
                    Console.WriteLine("Bye!");
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
              $"4) to add food from the table\n" +
              $"5) to take food to the table\n" +
              $"6) to eat some food\n" +
              $"7) to check the free space in the fridge\n" +
              $"8) to take out a shelf from the fridge\n" +
              $"9) to take back a shelf to the fridge\n" +
              $"10) to close the fridge\n" +
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
                                if (food.Size > 80)
                                {
                                    Console.WriteLine($"{food.Name} it's a big item!");
                                }
                                else
                                {
                                    Console.WriteLine(food.Name);
                                }
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
                    Console.Clear();
                    if (refig.IsOpen())
                    {
                        Console.WriteLine($"The {refig.Name} is already open!");
                    }
                    else
                    {
                        Console.WriteLine($"You opened the {refig.Name} fridge!");
                        refig.Open();
                    }
                    break;
                case "3":
                    Console.Clear();
                    try
                    {
                        if (refig.IsOpen())
                        {
                            Console.WriteLine("What is the name of the food?");
                            var foodName = Console.ReadLine();
                            Console.WriteLine("How many calories it has?");
                            var calories = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("What is the size of the food?(If it'size bigger than 80 - it's a big item, if smaller than 20 -it's a small item.)");
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
                    catch (BigItemCoolingException)
                    {
                        Console.WriteLine("This food so huge, you cannat place it to this fridge!");
                    }
                    break;
                case "4":
                    Console.Clear();
                    if (Foods.Count > 0)
                    {
                        Console.WriteLine("Which food you want to add from the table: ");
                        foreach (var food in Foods)
                        {
                            if (food.Size > 80)
                            {
                                Console.WriteLine($"{food.Name} it's a big item!");
                            }
                            else
                            {
                                Console.WriteLine(food.Name);
                            }
                        }
                        try
                        {
                            if (refig.IsOpen())
                            {
                                var foodName = Console.ReadLine();
                                var foodToAddFromTable = mh.FindFoodInTable(foodName, Foods);
                                refig.AddFood(foodToAddFromTable);

                                if (refig.RemovedShelf != null)
                                {
                                    shelves.Add(refig.RemovedShelf);
                                    foods.AddRange(refig.RemovedShelf.foodList);
                                    refig.RemovedShelf.foodList = new List<Food>();
                                    refig.RemovedShelf = null;
                                }
                                Foods.Remove(foodToAddFromTable);
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
                    }
                    else
                    {
                        Console.WriteLine("There is no food in the table!");
                    }
                    break;
                case "5":
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("What food you want to take to the table?");
                        var foodToAddName = Console.ReadLine();
                        Foods.Add(refig.GetFoodFromFridge(foodToAddName));
                    }
                    catch (FoodNotExistsException)
                    {
                        Console.WriteLine("Fridge doesn't contains it.");
                    }

                    break;
                case "6":
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("What you want to eat?");
                        var foodToEat = Console.ReadLine();
                        consumedCalories += refig.GetFoodFromFridge(foodToEat).Calories;
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
                    Console.Clear();
                    try
                    {
                        Console.WriteLine($"Your {refig.Name} fridge has {refig.GetFreeSpace()} unit free space.");

                    }
                    catch (FridgeIsClosedException)
                    {
                        Console.WriteLine("Please open the fridge first");
                    }
                    break;
                case "8":
                    Console.Clear();
                    try
                    {
                        Console.WriteLine("Which shelf you want to remove?");
                        for (int i = 0; i < refig.shelfContainer.Length; i++)
                        {
                            if (refig.shelfContainer[i] != null)
                            {
                                Console.WriteLine(i + 1);
                            }
                        }
                        var shelfToRemove = Convert.ToInt32(Console.ReadLine());
                        var removedShelf = refig.RemoveShelf(shelfToRemove - 1);

                        shelves.Add(removedShelf);
                        foods.AddRange(removedShelf.foodList);
                        removedShelf.foodList = new List<Food>();
                        Console.WriteLine($"You have removed the number {shelfToRemove} shelf!");
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Invalid Input, pls give a number!");
                    }
                    catch (FridgeIsClosedException)
                    {
                        Console.WriteLine("Please open the fridge first");
                    }
                    catch (ShelfAlreadyRemovedException)
                    {
                        Console.WriteLine("The choosen shelf already out of the fridge");
                    }
                    catch (NotEnoughShelfException)
                    {
                        Console.WriteLine("The fridge doesn't have as many shelves!");
                    }
                    break;

                case "9":
                    Console.Clear();
                    try
                    {
                        var emptyslots = refig.FindEmptySlots();

                        Console.WriteLine("This fridge misses the following shelfes:");
                        foreach (var num in emptyslots)
                        {
                            Console.WriteLine(num + 1);
                        }
                        Console.WriteLine("Which shelf you want to take back?");
                        var id = Convert.ToInt32(Console.ReadLine());

                        var shelfToTakeBack = mh.FindShelfByIdANdSize(id, refig.fridgeSize, shelves);
                        refig.AddShelf(shelfToTakeBack);
                        shelves.Remove(shelfToTakeBack);

                    }
                    catch (BigItemCoolingException)
                    {
                        Console.WriteLine("There are a big food which dont let you take the shelf back");
                    }
                    catch (NoEmptyShelfPlaceException)
                    {
                        Console.WriteLine("There are no empty shelf place!");
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("The input must be a number!");
                    }
                    catch (FridgeIsClosedException)
                    {
                        Console.WriteLine("Please open the fridge first");
                    }
                    break;
                case "10":
                    Console.Clear();
                    Console.WriteLine("The fridge is closed");
                    refig.Close();
                    break;
                case "0":
                    Console.Clear();
                    refig.Close();
                    inFridge = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input");
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
        public int ConsumFoodFromTable(List<Food> foods, string foodname)
        {
            foreach (var food in foods)
            {
                if (food.Name.Equals(foodname))
                {
                    var result = food.Calories;
                    foods.Remove(food);
                    return result;
                }
            }
            throw new FoodNotExistsException();
        }
        public Food FindFoodInTable(string name, List<Food> foodList)
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
        public Shelf FindShelfByIdANdSize(int id, Size size, List<Shelf> shelfList)
        {
            foreach (var shelf in shelfList)
            {
                if (id == shelf.ID && size.Equals(shelf.ShelfSize))
                {
                    return shelf;
                }
            }
            throw new NoEmptyShelfPlaceException();
        }
    }
}
