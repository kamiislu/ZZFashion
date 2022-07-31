using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class Staff
    {
        [Display (Name ="Staff ID")]
        public string StaffID { get; set; }

        [Display (Name ="StoreID")]
        public string StoreID { get; set; }

        [Display(Name ="Staff Name")]
        public string SName { get; set; }

        [Display(Name ="Staff Gender")]
        public char SGender { get; set; }

        [Display(Name ="Staff Appointment")]
        public string SAppt { get; set; }
        
        [Display(Name = "Staff Telephone Number")]
        public string STelNo { get; set; }

        [Display(Name = "Staff Email Addresss")]
        public string SEmailAddr { get; set; }

        [Display(Name = "Staff Password")]
        public string SPassword { get; set; }
    }
}
