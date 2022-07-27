using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class Customer
    {
        [Required]
        [Display(Name = "Member ID")]
        [StringLength(9, ErrorMessage = "MemberID cannot exceed 9 characters!")]
        public string MemberId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters!")]
        public string MName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public char MGender { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? MBirthDate { get; set; }

        [Display(Name = "Residential Address")]
        [StringLength(250, ErrorMessage = "Phone number cannot exceed 20 characters!")]
        public string MAddress { get; set; }

        [Required]
        [Display(Name = "Country")]
        [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters!")]
        public string MCountry { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters!")]
        [ValidatePhoneNumExists]
        public string MTelNo { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress] // Validation Annotation for email address format
                       // Custom Validation Attribute for checking email address exists
        [ValidateEmailExists]
        public string MEmailAddr { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "Password cannot exceed 20 characters!")]
        public string MPassword { get; set; }

    }
}