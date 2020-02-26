using System;

namespace com.codecool.api
{
    class Program
    {
        static void Main(string[] args)
        {
            //var f = new NormalRefig();
            //var g = new MiniRefig();
            //var gf = new Food(323, 45, "csoki");
            //g.Open();
            //g.AddFood(gf);
            //g.AddFood(gf);
            //g.AddFood(gf);
            //g.AddFood(gf);
            //g.AddFood(gf);
            //g.AddFood(gf);


            //var p = new DoubleRefig();

            //p.Open();

            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);

            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);

            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            //p.AddFood(gf);
            var xml = new XmlLoader();
            //xml.Save(new System.Collections.Generic.List<Refigrigator> { f, g, p });

            var h = xml.Load("Inventory.xml");

            foreach (var item in h)
            {

                Console.WriteLine(item);
                foreach (var ii in item.shelfContainer)
                {
                    Console.WriteLine(ii.shelfSize);
                    foreach (var ize in ii.foodList)
                    {
                        Console.WriteLine(ize.Name);
                    }
                }
            }


        }
    }
}
