using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedProject.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public CustomerDto(
            int id,
           string firstname,
           string lastName,
           DateTime dateOfBirth,
           string email,

          string phoneNumber,
          string bankAccountNumber)
        {
            Id = id;
            Firstname = firstname;
            Email = email;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            this.PhoneNumber = phoneNumber;
            this.BankAccountNumber = bankAccountNumber;
        }

        public static CustomerDto EmptyInstance()
        {
            return new CustomerDto(0, "", "", DateTime.Now, "", "", "");
        }
    }

}
