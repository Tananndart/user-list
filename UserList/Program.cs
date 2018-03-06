using System;

namespace UserList
{
    class Program
    {
        static void Main(string[] args)
        {
            UserList<int> list = new UserList<int> { 0, 1, 2, 3, 4, 5 };

            Console.WriteLine($"list.Count = {list.Count}");
            Console.WriteLine($"list.Capacity = {list.Capacity}");
            foreach (int val in list)
                Console.Write(val.ToString());

            Console.ReadKey();
        }
    }
}
