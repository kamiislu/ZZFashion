using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022Apr_P02_T3.Models
{
    public class MonthlyTransactionVM
    {
        public List<Customer> customerList { get; set; }
        public List<SalesTransaction> chosenTransactionList { get; set; }
        public MonthlyTransactionVM()
        {
            customerList = new List<Customer>();
            chosenTransactionList = new List<SalesTransaction>();
        }
    }
}
