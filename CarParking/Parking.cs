using System.Collections.Generic;

namespace CarParking
{
    public class Parking
    {
        private static Parking instance;

        public List<Car> Cars { get; private set; }
        private List<Transaction> Transactions { get; set; }
        public int Balance { get; private set; }

        public int Timeout { get; }
        public Dictionary<CarType, int> Prices { get; }
        public int ParkingSpace { get; }
        public int Fine { get; }

        private Parking()
        {
            Timeout = Settings.Timeout;
            Prices = Settings.Prices;
            ParkingSpace = Settings.ParkingSpace;
            Fine = Settings.Fine;
            Cars = new List<Car>();
            Transactions = new List<Transaction>();
        }

        public static Parking GetInstance()
        {
            if (instance == null)
            {
                instance = new Parking();
            }

            return instance;
        }

        public void AddCar(Car car)
        {
            if (Cars.Count != ParkingSpace && !Cars.Contains(car))
            {
                Cars.Add(car);
            }
        }

        public void RemoveCar(Car car)
        {
            if (Cars.Contains(car) && car.Balance > 0)
            {
                Cars.Remove(car);
            }
        }

        public int GetFreeSpacesNumber()
        {
            return ParkingSpace - Cars.Count;
        }

        public int GetTakenSpacesNumber()
        {
            return Cars.Count;
        }
    }
}