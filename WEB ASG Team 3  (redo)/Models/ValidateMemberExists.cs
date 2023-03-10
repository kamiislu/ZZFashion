using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WEB2022Apr_P02_T3.DAL;

namespace WEB2022Apr_P02_T3.Models
{
    public class ValidateMemberExists : ValidationAttribute
    {
        private CustomerDAL customerContext = new CustomerDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)

        {
            // Get the email value to validate
            string memId = Convert.ToString(value);
            // Casting the validation context to the "Staff" model class
            Customer customer = (Customer)validationContext.ObjectInstance;

            // Get the Staff Id from the staff instance
            string memberId = customer.MemberId;
            if (customerContext.IsMemberExist(memId, memberId))
                // validation failed
                return new ValidationResult
                ("MemberID already exists!");
            else
                // validation passed 
                return ValidationResult.Success;
        }
    }
}
