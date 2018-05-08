using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking
{
    public static class Settings
    {
        public static int Timeout = 3;

        public static Dictionary<CarType, int> Prices = new Dictionary<CarType, int>
        {
            {CarType.Motorcycle, 1},
            {CarType.Bus, 2},
            {CarType.Passenger, 3},
            {CarType.Truck, 5}
        };

        public static int ParkingSpace = 12;
        public static int Fine = 2;
    }
}