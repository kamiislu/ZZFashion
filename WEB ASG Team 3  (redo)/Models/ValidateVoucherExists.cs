using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WEB2022Apr_P02_T3.DAL;

namespace WEB2022Apr_P02_T3.Models
{
    public class ValidateVoucherExists : ValidationAttribute
    {
        private VoucherDAL voucherContext = new VoucherDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)

        {
            // Get the email value to validate
            string vouchersn = Convert.ToString(value);
            // Casting the validation context to the "Staff" model class
            CashVoucher cashvoucher = (CashVoucher)validationContext.ObjectInstance;

            // Get the Staff Id from the staff instance
            int issuingId = cashvoucher.IssuingID;
            if (voucherContext.IsVoucherExist(vouchersn, issuingId))
                // validation failed
                return new ValidationResult
                ("Voucher SN already exists!");
            else
                // validation passed 
                return ValidationResult.Success;
        }
    }
}
