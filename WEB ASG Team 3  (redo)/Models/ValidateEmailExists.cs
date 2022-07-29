using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WEB2022Apr_P02_T3.DAL;

namespace WEB2022Apr_P02_T3.Models
{
    public class ValidateEmailExists : ValidationAttribute
    {
        private CustomerDAL customerContext = new CustomerDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)

        {
            // Get the email value to validate
            string memail = Convert.ToString(value);
            // Casting the validation context to the "Staff" model class
            Customer customer = (Customer)validationContext.ObjectInstance;

            // Get the Staff Id from the staff instance
            string memberId = customer.MemberId;
            if (customerContext.IsEmailExist(memail, memberId))
                // validation failed
                return new ValidationResult
                ("Email address already exists!");
            else
                // validation passed 
                return ValidationResult.Success;
        }
    }
}
