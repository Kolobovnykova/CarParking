using System;
using System.Collections.Generic;
using System.IO;

namespace CarParking
{
    public static class Settings
    {
        public static int Timeout { get; }
        public static int ParkingSpace { get; }
        public static int Fine { get; }
        public static Dictionary<CarType, int> Prices { get; }
        public static string PathToLogFile { get; }
        public static int TransactionTimeout { get; }

        static Settings()
        {
            Timeout = 3000;
            TransactionTimeout = 60000;
            ParkingSpace = 12;
            Fine = 2;
            PathToLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Transactions.log");

            Prices = new Dictionary<CarType, int>
            {
                {CarType.Motorcycle, 1},
                {CarType.Bus, 2},
                {CarType.Passenger, 3},
                {CarType.Truck, 5}
            };
        }
    }
}