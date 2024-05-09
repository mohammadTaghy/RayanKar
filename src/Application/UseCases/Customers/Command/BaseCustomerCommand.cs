using SharedProject.Customer;
using SharedProject.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Command
{
    public abstract class BaseCustomerCommand
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public CustomerDto CustomerDto { get; set; }    
       
        protected BaseCustomerCommand(
            string firstname, 
            string lastName, 
            DateTime dateOfBirth, 
            string phoneNumber, 
            string email, 
            string bankAccountNumber)
        {
            Firstname = firstname;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
            CustomerDto = new CustomerDto(0, firstname, lastName, dateOfBirth, email, phoneNumber, bankAccountNumber);
        }
        public void FillCustomerDto()
        {
            CustomerDto = new CustomerDto(0, Firstname, LastName, DateOfBirth, Email, PhoneNumber, BankAccountNumber);

        }
    }
}
