using System;

namespace CarParking
{
    public class Transaction
    {
        public Transaction(DateTime dateTime, int carId, int withdrawal)
        {
            DateTime = dateTime;
            CarId = carId;
            Withdrawal = withdrawal;
        }

        public DateTime DateTime { get; }
        public int CarId { get; }
        public int Withdrawal { get; }
    }
}