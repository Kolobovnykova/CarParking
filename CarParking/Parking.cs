using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking
{
    public class Parking
    {
        private static Parking instance;

        private Parking()
        {
        }

        public static Parking GetInstance()
        {
            if (instance == null)
            {
                instance = new Parking();
            }

            return instance;
        }

        private List<Car> Cars { get; set; }
        private List<Transaction> Transactions { get; set; }
        public int Balance { get; set; }
    }
}
