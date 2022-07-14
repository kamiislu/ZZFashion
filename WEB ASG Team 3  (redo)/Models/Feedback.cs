using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class Feedback
    {
        [Display(Name= "Feedback ID")]
        public int FeedbackID { get; set; }

        [Display(Name = "Member ID")]
        public string MemberID { get; set; }

        [Display(Name = "Date Posted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/mm/yyyy}")]
        public DateTime DatePosted { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name ="Text")]
        public string? Text { get; set; }

        public string? Image { get; set; }

    }
}
