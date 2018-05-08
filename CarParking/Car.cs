namespace CarParking
{
    public class Car
    {
        public Car(int id, int balance, CarType carType)
        {
            Id = id;
            Balance = balance;
            CarType = carType;
        }

        public int Id { get; set; }
        public int Balance { get; set; }
        public CarType CarType { get; set; }
    }
}
