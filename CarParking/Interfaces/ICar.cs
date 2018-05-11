using CarParking.Entities;

namespace CarParking.Interfaces
{
    public interface ICar
    {
        int Id { get; }
        double Balance { get; }
        CarType CarType { get; }

        void ReplenishBalance(double amount);
        void WithdrawBalance(double amount);
    }
}