namespace CarParking
{
    public class Car
    {
        public int Id { get; }
        public int Balance { get; private set; }
        public CarType CarType { get; }

        public Car(int id, int balance, CarType carType)
        {
            Id = id;
            Balance = balance;
            CarType = carType;
        }

        public void ReplenishBalance(int amount)
        {
            Balance += amount;
        }
    }
}