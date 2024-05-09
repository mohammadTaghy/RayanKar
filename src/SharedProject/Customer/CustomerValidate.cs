
using SharedProject.Validation;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedProject.Customer
{
    public class CustomerValidate
    {
        public static bool CommonValidate(CustomerDto customerWrite, out string errorMessage)
        {
            StringBuilder errors = new StringBuilder();
            if (!PhoneNumber.Validate(customerWrite.PhoneNumber))
            {
                WriteMessage(errors,CommonMessage.InValidPhonNumber);
            }
            if (!BankAccountNumber.Validate(customerWrite.BankAccountNumber))
            {
                WriteMessage(errors, CommonMessage.InValidBankAccountNumber);
            }
            EmailValidate(customerWrite, errors);
            if (string.IsNullOrEmpty(customerWrite.Firstname))
            {
                WriteMessage(errors, string.Format(CommonMessage.CannotBeNullOrLessThan, nameof(customerWrite.Firstname), 4));
            }
            if (string.IsNullOrEmpty(customerWrite.LastName))
            {
                WriteMessage(errors, string.Format(CommonMessage.CannotBeNullOrLessThan, nameof(customerWrite.LastName), 4));
            }
            errorMessage = errors.ToString();
            return string.IsNullOrEmpty(errorMessage);
        }

        private static void WriteMessage(StringBuilder errors, string message)
        {
            errors.AppendLine(message);
        }

        private static void EmailValidate(CustomerDto customerWrite, StringBuilder errors)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(customerWrite.Email);
            if (!match.Success)
                errors.Append(CommonMessage.InValidEmail);
        }
    }
}
