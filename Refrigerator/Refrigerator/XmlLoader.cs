using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (Stream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Refigrigator>));
                serializer2.Serialize(fs, refigrigators);
            }

        }

    }
}
