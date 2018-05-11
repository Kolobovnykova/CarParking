using CarParking.Exceptions;
using CarParking.Interfaces;

namespace CarParking.Entities
{
    public class Car : ICar
    {
        public int Id { get; }
        public double Balance { get; private set; }
        public CarType CarType { get; }

        public void ReplenishBalance(double amount)
        {
            if (amount < 0)
            {
                throw new NegativeBalanceException("Amount to replenish should be positive.");
            }

            Balance += amount;
        }

        public void WithdrawBalance(double amount)
        {
            if (amount < 0)
            {
                throw new NegativeBalanceException("Amount to withdraw should be positive.");
            }

            Balance -= amount;
        }

        public Car(int id, double balance, CarType carType)
        {
            Id = id;
            Balance = balance;
            CarType = carType;
        }

        public override string ToString()
        {
            return $"Id: {Id}, type: {CarType}, balance: {Balance}";
        }
    }
}