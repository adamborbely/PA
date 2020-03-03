using com.codecool.api;

namespace com.codecool.cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            new Menu(new XmlLoader()).Run();
        }
    }
}
