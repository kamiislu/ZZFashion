using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class SalesTransaction
    {
        [Display(Name = "TransactionID")]
        public int transactionID { get; set; }
        [Display(Name = "Store ID")]
        public string storeID { get; set; }

        [Display(Name = " MemberID")]
        public string memberID { get; set; }

        [Display(Name = "SubTotal")]
        public decimal subtotal { get; set; }

        [Display(Name = "Tax")]
        public decimal Tax { get; set; }

        [Display(Name = "Discount Percent")]
        public double discountPercent { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal discountAmount { get; set; }

        [Display (Name = "Total")]
        public decimal total { get; set; }

        [Display(Name = "Date Created")]
        public DateTime dateCreated { get; set; }
    }
}
