using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class IssueVoucher
    {
        [Display (Name = "Issuing ID")]
        public int IssuingID { get; set; }

        [Display (Name = "MemberID")]
        public string MemberID { get; set; }

        [Display (Name = "Amount")]
        public decimal Amount { get; set; }

        [Display (Name = "Month issued for")]
        public int MonthIssuedFor { get; set; }

        [Display (Name = "Year issued for")]
        public int YearIssuedFor { get; set; }

        [Display (Name = "Date and Time issued")]
        public DateTime DateTimeIssued { get; set; }

        [Display (Name = "Voucher Serial Number")]
        public string VoucherSN { get; set; }

        [Display (Name = "Status")]
        [StringLength(1)]
        public string status { get; set; }
            
        [Display(Name ="Date and Time redeemed")]
        public DateTime DateTimeRedeemed { get; set; }
    }
}
