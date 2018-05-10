using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarParking
{
    public class Parking
    {
        private static Parking instance;
        private Timer timer;

        public List<Car> Cars { get; private set; }
        private List<Transaction> Transactions { get; }
        public double Balance { get; private set; }

        private Parking()
        {
            Cars = new List<Car>();
            Transactions = new List<Transaction>();
            timer = new Timer(PaymentAction, new object(), 0, Settings.Timeout);
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
            if (car == null)
            {
                Console.WriteLine("Invalid Id. No cars with such Id found.");
                return;
            }

            if (car.Balance < 0)
            {
                Console.WriteLine($"Can't remove car with id {carId}, it has negative balance: {car.Balance}.");
                return;
            }

            Cars.Remove(car);
            Console.WriteLine($"Removed car: {car}");
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

        private async void PaymentAction(object obj)
        {
            await Task.Run(() =>
            {
                Cars.ForEach(car =>
                {
                    if (Settings.Prices.TryGetValue(car.CarType, out var price))
                    {
                        double amountToWithdraw = car.Balance >= price ? price : price * Settings.Fine;
                        car.WithdrawBalance(amountToWithdraw);
                        Balance += amountToWithdraw;
                    }
                });
            });
        }

        public void StopTimer()
        {
            timer.Dispose();
        }
    }
}