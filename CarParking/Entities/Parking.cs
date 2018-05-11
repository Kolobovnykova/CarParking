using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarParking.Exceptions;
using CarParking.Helpers;
using CarParking.Interfaces;

namespace CarParking.Entities
{
    public class Parking : IParking
    {
        private static readonly Lazy<Parking> instance = new Lazy<Parking>(() => new Parking());
        private readonly Timer paymentTimer;
        private readonly Timer logTimer;

        public List<Car> Cars { get; }
        public List<Transaction> Transactions { get; }
        public double Balance { get; private set; }

        public delegate void ParkingStateHandler(object sender, ParkingEventArgs e);

        public event ParkingStateHandler CarAdded;
        public event ParkingStateHandler CarRemoved;
        public event ParkingStateHandler BalanceAdded;

        private Parking()
        {
            Cars = new List<Car>();
            paymentTimer = new Timer(PaymentAction, new object(), 0, Settings.Timeout);
            logTimer = new Timer(LogTransaction, new object(), 0, Settings.TransactionTimeout);
            Transactions = new List<Transaction>();
            if (File.Exists(Settings.PathToLogFile))
            {
                File.Delete(Settings.PathToLogFile);
            }
        }

        public static Parking Instance => instance.Value;

        public double GetParkingBalance() => Balance;

        public void AddCar(CarType carType, double balance)
        {
            if (Cars.Count != Settings.ParkingSpace)
            {
                var car = new Car(AssignId(), balance, carType);

                if (balance <= 0)
                {
                    throw new NegativeBalanceException("Car balance should be positive. Failed to add a car.");
                }

                Cars.Add(car);
                if (CarAdded != null)
                {
                    CarAdded(this, new ParkingEventArgs("been added", car));
                }
            }
            else
            {
                throw new NotEnoughSpaceException("Not enough space to place a car.");
            }
        }

        public void RemoveCar(int carId)
        {
            var car = Cars.FirstOrDefault(x => x.Id == carId);
            if (car == null)
            {
                throw new InvalidIdException($"Invalid Id {carId}. No cars with such Id found.");
            }

            if (car.Balance < 0)
            {
                throw new NegativeBalanceException(
                    $"Can't remove car with id {carId}, it has negative balance: {car.Balance}.");
            }

            Cars.Remove(car);
            if (CarRemoved != null)
            {
                CarRemoved(this, new ParkingEventArgs("been removed", car));
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

        public List<Car> GetListOfParkedCars()
        {
            if (Cars.Count == 0)
            {
                throw new EmptyParkingException("No cars at the parking.");
            }

            return Cars;
        }

        public void ReplenishCarBalance(int carId, double amount)
        {
            if (CarExistInParking(carId))
            {
                var car = Cars.FirstOrDefault(x => x.Id == carId);
                car.ReplenishBalance(amount);
                if (BalanceAdded != null)
                {
                    BalanceAdded(this, new ParkingEventArgs("its balance replenished", car));
                }
            }
            else
            {
                throw new InvalidIdException($"Invalid Id {carId}. No cars with such Id found.");
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

        public double GetParkingIncomeForPastMinute()
        {
            var income = Transactions.Where(t => t.TransactionTime > DateTime.Now.AddMinutes(-1))
                .Sum(s => s.Withdrawal);
            return income;
        }

        private async void PaymentAction(object o)
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
                        Transactions.Add(new Transaction(DateTime.Now, car.Id, amountToWithdraw));
                    }
                });
            });
        }

        private async void LogTransaction(object o)
        {
            await Task.Run(() =>
                {
                    using (StreamWriter sw = new StreamWriter(Settings.PathToLogFile, true))
                    {
                        sw.WriteLine($"{DateTime.Now}, income: {GetParkingIncomeForPastMinute()}");
                    }
                }
            );
        }

        public void StopTimers()
        {
            paymentTimer.Dispose();
            logTimer.Dispose();
        }
    }
}