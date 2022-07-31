using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class CreditCard
    {
        [Display(Name ="End of year")]
        public DateTime EndOfYear { get; set; }

        [Display(Name = "Numebr of Cardholders - Principal")]
        public int CardsMain { get; set; }

        [Display(Name ="Number of Cardholders - Supplementary")]
        public int CardsSupp { get; set; }

        [Display(Name = "Total Card Billings")]
        public decimal TotalCardBillings { get; set; }

        [Display(Name ="Rollover Balance")]
        public decimal RolloverBalance { get; set; }

        [Display(Name = "Bad Debts Written Off")]
        public int BadDebtsWrittenOff { get; set; }

        [Display(Name = "Charge-off Rates")]
        public decimal ChargeOffRates { get; set; }
    }
}
