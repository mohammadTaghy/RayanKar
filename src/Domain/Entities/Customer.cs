using Domain.EntitiesBaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Customer : AggregateRoot
    {
        protected Customer(string firstname, string phoneNumber, string email, string bankAccountNumber, string lastName)
        {
            Firstname = firstname;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
            LastName = lastName;
        }
        public DateTime DateOfBirth { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
