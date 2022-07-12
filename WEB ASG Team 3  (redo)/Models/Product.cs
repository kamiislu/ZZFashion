using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB2022Apr_P02_T3.Models
{
    public class Product
    {
        //product ID
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        //product title
        [Display(Name = "Product Title")]
        public string ProductTitle { get; set; }

        //product image
        [Display(Name = "Product Image")]
        public string ? ProductImage { get; set; }

        //price
        [Display(Name = "Price ($)")]
        [DisplayFormat(DataFormatString = "{0:0.00}",
            ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        //effective date
        [Display(Name = "Effective Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime EffectiveDate { get; set; }

        //obsolete
        public string Obsolete { get; set; }
    }
}
