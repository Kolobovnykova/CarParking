using System;
using CarParking.Entities;
using CarParking.Exceptions;
using CarParking.Helpers;

namespace CarParking
{
    public class Menu
    {
        private const string menu = "What would you like to do?\n '1' - add a car to parking" +
                                    "\n '2' - remove a car from parking" +
                                    "\n '3' - add funds to car's balance" +
                                    "\n '4' - show list of parked cars" +
                                    "\n '5' - show available spaces" +
                                    "\n '6' - show parking balance" +
                                    "\n '7' - show parking income for the past 1 minute" +
                                    "\n '8' - show transactions.log" +
                                    "\n 'Esc' - press escape to exit application \n";

        private const string addCarMenu = "You can add cars with types:\n '1' - passenger" +
                                          "\n '2' - truck" +
                                          "\n '3' - bus" +
                                          "\n '4' - motorcycle\n";

        private readonly Parking parking;

        public Menu()
        {
            parking = Parking.Instance;
        }

        public void SelectMenu()
        {
            ConsoleKeyInfo enteredKey;
            do
            {
                Console.Clear();
                Console.WriteLine(menu);
                enteredKey = Console.ReadKey();

                switch (enteredKey.Key)
                {
                    case ConsoleKey.D1:
                        AddCar();
                        break;
                    case ConsoleKey.D2:
                        RemoveCar();
                        break;
                    case ConsoleKey.D3:
                        ReplenishBalanceOfCar();
                        break;
                    case ConsoleKey.D4:
                        ShowListOfParkedCars();
                        break;
                    case ConsoleKey.D5:
                        ShowNumberOfFreeSpaces();
                        break;
                    case ConsoleKey.D6:
                        ShowParkingBalance();
                        break;
                    case ConsoleKey.D7:
                        ShowParkingIncomeForPastMinute();
                        break;
                    case ConsoleKey.D8:
                        ShowLogFile();
                        break;
                    case ConsoleKey.D9:
                        break;
                }

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            } while (enteredKey.Key != ConsoleKey.Escape);
            
            parking.StopTimers();
        }

        private void AddCar()
        {
            Console.Clear();
            Console.WriteLine(addCarMenu);
            ConsoleKeyInfo enteredKeyCar = Console.ReadKey();
            CarType carType;

            switch (enteredKeyCar.Key)
            {
                case ConsoleKey.D1:
                    carType = CarType.Passenger;
                    break;
                case ConsoleKey.D2:
                    carType = CarType.Truck;
                    break;
                case ConsoleKey.D3:
                    carType = CarType.Passenger;
                    break;
                case ConsoleKey.D4:
                    carType = CarType.Motorcycle;
                    break;
                default:
                    Console.WriteLine("\nSuch type of car doesn't exist, back to menu.");
                    return;
            }

            Console.WriteLine("\nAdd balance:");
            try
            {
                double amount = double.Parse(Console.ReadLine());
                parking.AddCar(carType, amount);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
            catch (NotEnoughSpaceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NegativeBalanceException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RemoveCar()
        {
            Console.Clear();
            Console.WriteLine("Removing an existing a car.\nEnter the id:");
            try
            {
                var carId = int.Parse(Console.ReadLine());
                parking.RemoveCar(carId);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
            catch (InvalidIdException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NegativeBalanceException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ReplenishBalanceOfCar()
        {
            Console.Clear();
            Console.WriteLine("Replenishing balance of an existing car.\nEnter Id of the car:");
            try
            {
                var carId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter amount to replenish.");
                var amount = double.Parse(Console.ReadLine());

                parking.ReplenishCarBalance(carId, amount);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
            catch (InvalidIdException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NegativeBalanceException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ShowListOfParkedCars()
        {
            Console.Clear();
            try
            {
                parking.ShowParkedCars();
            }
            catch (EmptyParkingException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ShowNumberOfFreeSpaces()
        {
            Console.Clear();
            Console.WriteLine($"Number of free spaces: {parking.GetFreeSpacesNumber()}");
            Console.WriteLine($"Number of taken spaces: {parking.GetTakenSpacesNumber()}");
        }

        private void ShowParkingBalance()
        {
            Console.Clear();
            Console.WriteLine($"Parking balance is: {parking.GetParkingBalance()}");
        }
        
        private void ShowParkingIncomeForPastMinute()
        {
            var income = parking.GetParkingIncomeForPastMinute();
            Console.Clear();
            Console.WriteLine($"Parking income for the past minute is: {income}");
        }
        
        private void ShowLogFile()
        {
            var logFile = FileHelper.ReadLogFile();
            Console.Clear();
            Console.WriteLine("Transactions.log content:");
            Console.WriteLine(logFile);
        }
    }
}