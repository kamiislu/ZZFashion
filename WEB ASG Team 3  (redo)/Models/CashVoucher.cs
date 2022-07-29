using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class CashVoucher
    {
        [Required]
        [Display(Name = "Issuing ID")]
        public int IssuingID { get; set; }


        [Required]
        [Display(Name = "Member ID")]
        [StringLength(9, ErrorMessage = "MemberID cannot exceed 9 characters!")]
        public string MemberId { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}",
            ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; } = 20;

        [Display(Name = "Month Issued For")]
        public int MonthIssuedFor { get; set; }

        [Display(Name = "Year Issued For")]
        public int YearIssuedFor { get; set;  }

        [Required]
        [Display(Name ="Date Time Issued")]
        public DateTime DateTimeIssued { get; set; }

        [Required]
        [Display(Name = "Voucher SN")]
        [StringLength(30, ErrorMessage = "Vocuher SN cannot exceed 30 characters!")]
        [ValidateVoucherExists]
        public string VoucherSN { get; set; }

        [Required]
        [Display(Name ="Status")]
        public char Status { get; set; }

        [Display(Name = "DateTime Redeemed")]
        public DateTime? DateTimeRedeemed { get; set; }
    }
}
