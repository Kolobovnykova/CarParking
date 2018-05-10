using System.Collections.Generic;

namespace CarParking
{
    public static class Settings
    {
        public static int Timeout { get; }
        public static int ParkingSpace { get; }
        public static int Fine { get; }
        public static Dictionary<CarType, int> Prices { get; }

        static Settings()
        {
            Timeout = 3000;
            ParkingSpace = 12;
            Fine = 2;

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