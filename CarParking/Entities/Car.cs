namespace CarParking.Entities
{
    public class Car
    {
        public int Id { get; }
        public double Balance { get; private set; }
        public CarType CarType { get; }

        public Car(int id, double balance, CarType carType)
        {
            Id = id;
            Balance = balance;
            CarType = carType;
        }

        public void ReplenishBalance(double amount)
        {
            Balance += amount;
        }
        
        public void WithdrawBalance(double amount)
        {
            Balance -= amount;
        }

        public override string ToString()
        {
            return $"Id: {Id}, type: {CarType}, balance: {Balance}";
        }
    }
}