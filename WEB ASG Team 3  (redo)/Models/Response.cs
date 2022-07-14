using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class Response
    {
        [Display (Name = "Response ID")]
        public int ResponseID { get; set; }

        [Display(Name ="FeedbackID")]
        public int FeedbackID { get; set; }
        
        [Display(Name = "Member ID")]
        public string? MemberID { get; set; }

        [Display (Name = "Staff ID")]
        public string? StaffID { get; set; }

        [Display (Name = "Date & Time Posted")]
        public DateTime DatePosted { get; set; }
        
        [Display (Name = "Text")]
        public string Text { get; set; }
    }
}
