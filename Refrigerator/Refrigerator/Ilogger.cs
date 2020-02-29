using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    interface Ilogger
    {
        public void Save(List<Refigrigator> refigrigators);
        public void SaveFood(List<Food> foodlist, string path);
        public void SaveShelf(List<Shelf> shelves, string path);
        public List<Refigrigator> Load(string path);
        public List<Food> LoadFood(string path);
        public List<Shelf> LoadShelf(string path);
    }
}
