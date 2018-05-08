using System;

namespace CarParking
{
    class Program
    {
        static void Main(string[] args)
        {
            var parking = Parking.GetInstance();

            Console.WriteLine("Hello Parking!");
            Console.ReadKey();
        }
    }
}
