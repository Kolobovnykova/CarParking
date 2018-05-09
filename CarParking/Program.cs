using System;

namespace CarParking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parking = Parking.GetInstance();

            do
            {
                Console.WriteLine(" What would you like to do?\n '1' - add a car to parking" +
                                  "\n '2' remove car from parking" +
                                  "\n press escape to exit application");

                ConsoleKeyInfo enteredKey = Console.ReadKey();
                
                switch (enteredKey.Key)
                {
                    case ConsoleKey.D1:

                        // enter car settings
                        // console.read settings
                        var car1 = new Car(1, -100, CarType.Passenger);
                        parking.AddCar(car1);
                        Console.WriteLine("Added a car");
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Removed a car");
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
           
            }
            while (true);
            

               

           

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