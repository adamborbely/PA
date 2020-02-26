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
        public List<Refigrigator> Refigrigators
        {
            get { return refigrigators; }
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
                Console.WriteLine("Welcome to the FridgeSimulator! Press \n1) to create a new fridge\n2) to load previous\n0) to exit");
                var ans = Console.ReadLine();
                if (ans == "1")
                {
                    var newFridge = AddRefig();

                    while (true)
                    {
                        FridgeMenuLogic(newFridge, FridgeMenu(newFridge));
                    }
                }
                else if (ans == "2")
                {

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
                    var fridge = new MiniRefig(fridgeName);
                    refigrigators.Add(fridge);
                    return fridge;
                }
                else if (fridgeSize == "2")
                {
                    Console.WriteLine("Give a name to your Normal Fridge!");
                    var fridgeName = Console.ReadLine();
                    var fridge = new MiniRefig(fridgeName);
                    refigrigators.Add(fridge);
                    return fridge;
                }
                else if (fridgeSize == "3")
                {
                    Console.WriteLine("Give a name to your Double Door Fridge!");
                    var fridgeName = Console.ReadLine();
                    var fridge = new MiniRefig(fridgeName);
                    refigrigators.Add(fridge);
                    return fridge;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    Thread.Sleep(600);
                    Console.Clear();
                }
            }
        }
        public string FridgeMenu(Refigrigator refigrigator)
        {
            Console.WriteLine($"Welcome to the {refigrigator.Name} Fridge! Press\n" +
              $"1) to list all food in the fridge\n" +
              $"2) to add a new shop\n" +
              $"3) to remove an existing shop\n" +
              $"4) enter a shop by name\n" +
              $"5) to open the plaza\n" +
              $"6) to close the plaza\n" +
              $"7) to check if the plaza is open or not\n" +
              $"8) list items in the cart\n" +
              $"9) leave plaza");
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
                        if (plaza.GetShops().Count > 0)
                        {
                            foreach (var shop in plaza.GetShops())
                            {
                                Console.WriteLine(shop.GetName());
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no shops yet");
                        }
                    }
                    catch (PlazaIsClosedException)
                    {
                        Console.WriteLine("Open the plaza first!"); ;
                    }
                    break;
            }
        }
    }
