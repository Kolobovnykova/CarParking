using System.Collections.Generic;
using CarParking.Entities;

namespace CarParking.Interfaces
{
    public interface IParking
    {
        List<Car> Cars { get; }
        List<Transaction> Transactions { get; }
        double Balance { get; }

        double GetParkingBalance();
        void AddCar(CarType carType, double balance);
        void RemoveCar(int carId);
        int GetFreeSpacesNumber();
        int GetTakenSpacesNumber();
        void ReplenishCarBalance(int carId, double amount);
        double GetParkingIncomeForPastMinute();
    }
}