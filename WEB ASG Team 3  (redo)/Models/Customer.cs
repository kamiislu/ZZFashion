using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class Customer
    {
        [Display(Name = "Member ID")]
        public string MemberId { get; set; }
        [Display(Name = "Name")]
        public string MName { get; set; }
        [Display(Name = "Gender")]
        public char MGender { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? MBirthDate { get; set; }
        [Required]
        [Display(Name = "Residential Address")]
        [StringLength(255, ErrorMessage = "Phone number cannot exceed 20 characters!")]
        public string MAddress { get; set; }
        public string MCountry { get; set; }
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters!")]
        public string MTelNo { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress] // Validation Annotation for email address format
        public string MEmailAddr { get; set; }
        [Required]
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "Password cannot exceed 20 characters!")]
        public string MPassword { get; set; }

    }
}
