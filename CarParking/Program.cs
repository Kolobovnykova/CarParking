using System;

namespace CarParking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What would you like to do?\n'1' - add a car to parking");
            }

            var parking = Parking.GetInstance();

            var c1 = new Car(1, -100, CarType.Passenger);
            var c2 = new Car(2, 100, CarType.Passenger);
            var c3 = new Car(3, 100, CarType.Passenger);

            parking.AddCar(c1);
            parking.AddCar(c1);
            parking.AddCar(c1);
            parking.AddCar(c2);
            parking.AddCar(c3);

            Console.WriteLine("Cars added");
            foreach (var car in parking.Cars)
            {
                Console.WriteLine(car.Id);
            }

            parking.RemoveCar(c1);

            Console.WriteLine("Car1 removed");
            foreach (var car in parking.Cars)
            {
                Console.WriteLine(car.Id);
            }


            Console.ReadKey();
        }
    }
}