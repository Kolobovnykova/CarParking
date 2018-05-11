using System;
using CarParking.Entities;

namespace CarParking.Helpers
{
    public class ParkingEventArgs : EventArgs
    {
        public string Message { get; }

        public Car Car { get; }

        public ParkingEventArgs(string mes, Car car)
        {
            Message = mes;
            Car = car;
        }
    }
}