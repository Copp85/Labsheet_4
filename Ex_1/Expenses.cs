using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_1
{
   public class Expenses
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime IncurredDate { get; set; }

        public static decimal TotalExpenses { get; set; }

        public Expenses(string name, decimal cost, DateTime date)
        {
            Name = name;
            Cost = cost;
            IncurredDate = date;

            TotalExpenses += cost;
        }

        public override string ToString()
        {
            return $"{Name} {Cost} on {IncurredDate.ToShortDateString()}";
        }
    }
}
