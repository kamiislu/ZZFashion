using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022Apr_P02_T3.Models
{
    public class CustomerViewModel
    {
        public List<Customer> customerList { get; set; }
        public List<SalesTransaction> salesTransactionList { get; set; }
        public decimal total { get; set; }
        public CustomerViewModel()
        {
            customerList = new List<Customer>();
            salesTransactionList = new List<SalesTransaction>();
        }
    }
}
