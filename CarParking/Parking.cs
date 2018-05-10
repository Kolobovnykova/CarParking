using System;
using System.Collections.Generic;
using System.Linq;

namespace CarParking
{
    public class Parking
    {
        private static Parking instance;

        public List<Car> Cars { get; private set; }
        private List<Transaction> Transactions { get; set; }
        public int Balance { get; private set; }

        private Parking()
        {
            Cars = new List<Car>();
            Transactions = new List<Transaction>();
        }

        public static Parking GetInstance()
        {
            if (instance == null)
            {
                instance = new Parking();
            }

            return instance;
        }

        public void AddCar(CarType carType)
        {
            if (Cars.Count != Settings.ParkingSpace)
            {
                var car = new Car(AssignId(), 0, carType);

                Console.WriteLine($"Added car {car}");

                Cars.Add(car);
            }
            else
            {
                Console.WriteLine("Not enough space to place a car.");
            }
        }

        public void RemoveCar(int carId)
        {
            var car = Cars.FirstOrDefault(x => x.Id == carId);
            if (car != null && car.Balance >= 0)
            {
                Cars.Remove(car);
                Console.WriteLine($"Removed car: {car}");
            }
            else
            {
                Console.WriteLine("Invalid Id. No cars with such Id found.");
            }
        }

        public int GetFreeSpacesNumber()
        {
            return Settings.ParkingSpace - Cars.Count;
        }

        public int GetTakenSpacesNumber()
        {
            return Cars.Count;
        }

        public void ShowParkedCars()
        {
            if (Cars.Count == 0)
            {
                Console.WriteLine("No cars at the parking.");
            }
            else
            {
                Console.WriteLine("List of cars parked:");
                foreach (var car in Cars)
                {
                    Console.WriteLine(car);
                }
            }
        }

        public void ReplenishCarBalance(int carId, double amount)
        {
            if (CarExistInParking(carId))
            {
                var car = Cars.FirstOrDefault(x => x.Id == carId);
                car.ReplenishBalance(amount);
                Console.WriteLine("Added to car balance.");
            }
            else
            {
                Console.WriteLine("Invalid Id. No cars with such Id found.");
            }
        }

        private bool CarExistInParking(int carId)
        {
            return Cars.Any(cus => cus.Id == carId);
        }

        private int AssignId()
        {
            if (Cars.Count == 0)
            {
                return 1;
            }

            return Cars.Last().Id + 1;
        }

        public void ShowParkingBalance()
        {
            Console.WriteLine($"Parking balance is: {Balance}");
        }
    }
}