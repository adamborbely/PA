using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace com.codecool.api
{
    class XmlLoader : Ilogger
    {
        string path = "Inventory.xml";
        public List<Refigrigator> Load(string path)
        {
            var result = new List<Refigrigator>();
            XmlSerializer serializer3 = new XmlSerializer(typeof(List<Refigrigator>));
            using (FileStream fs2 = File.OpenRead(path))
            {
                result = (List<Refigrigator>)serializer3.Deserialize(fs2);
            }
            return result;
        }

        public void Save(List<Refigrigator> refigrigators)
        {
            using (Stream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Refigrigator>));
                serializer2.Serialize(fs, refigrigators);
            }
        }
        public List<Food> LoadFood(string path)
        {
            var result = new List<Food>();
            XmlSerializer serializer3 = new XmlSerializer(typeof(List<Food>));
            using (FileStream fs2 = File.OpenRead(path))
            {
                result = (List<Food>)serializer3.Deserialize(fs2);
            }
            return result;
        }
        public void SaveFood(List<Food> foodList, string path)
        {
            using (Stream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Food>));
                serializer2.Serialize(fs, foodList);
            }
        }

        public List<Shelf> LoadShelf(string path)
        {
            var result = new List<Shelf>();
            XmlSerializer serializer3 = new XmlSerializer(typeof(List<Shelf>));
            using (FileStream fs2 = File.OpenRead(path))
            {
                result = (List<Shelf>)serializer3.Deserialize(fs2);
            }
            return result;
        }
        public void SaveShelf(List<Shelf> shelves, string path)
        {
            using (Stream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Shelf>));
                serializer2.Serialize(fs, shelves);
            }
        }
    }
}
