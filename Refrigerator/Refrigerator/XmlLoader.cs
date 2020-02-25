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
            throw new NotImplementedException();
        }

        public void Save(List<Refigrigator> refigrigators)
        {
            using (Stream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Refigrigator>));
                serializer2.Serialize(fs, refigrigators);
            }
        }
    }
}
