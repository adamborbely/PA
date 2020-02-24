using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    class Food
    {
        private int _calories;
        private int _size;
        private string _name;
        public string Name => _name;
        public int Size => _size;
        public int Calories => _calories;

        public Food(int calories, int size, string name)
        {
            _calories = calories;
            _size = size;
            _name = name;
        }
    }
}
