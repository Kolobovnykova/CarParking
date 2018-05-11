using System;

namespace CarParking.Interfaces
{
    public interface ITransaction
    {
        DateTime TransactionTime { get; }
        int CarId { get; }
        double Withdrawal { get; }
    }
}