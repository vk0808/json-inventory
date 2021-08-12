using System;

namespace JSONInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            FetchDetails fetch = new FetchDetails();

            Console.WriteLine(fetch.welcome());
            fetch.menu();
        }
    }
}
