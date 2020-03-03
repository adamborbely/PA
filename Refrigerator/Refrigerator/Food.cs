namespace com.codecool.api
{
    public class Food
    {
        public int _calories;
        public int _size;
        public string _name;
        public string Name => _name;
        public int Size => _size;
        public int Calories => _calories;

        public Food() { }
        public Food(int calories, int size, string name)
        {
            _calories = calories;
            _size = size;
            _name = name;
        }
    }
}
