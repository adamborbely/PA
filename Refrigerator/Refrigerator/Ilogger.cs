using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    interface Ilogger
    {
        public void Save(List<Refigrigator> refigrigators);
        public List<Refigrigator> Load(string path);
    }
}
