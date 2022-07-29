using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class TotalAmountViewModel
    {
        [Display(Name ="Member ID")]
        public string MemberID { get; set; }

        [Display(Name ="Member Name")]
        public string MName { get; set; }

        [Display(Name = "Total Amount Spent")]
        public decimal? TotalAmount { get; set; }
        
        [Display(Name = "Transaction Date")]
        public DateTime DateCreated { get; set; }
    }
}
