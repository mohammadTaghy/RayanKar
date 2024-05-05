using Domain.EntitiesBaseClass;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Customer : AggregateRoot
    {
        public DateTime DateOfBirth { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string Email { get; set; }
        public BankAccountNumber BankAccountNumber { get; set; }
        protected Customer(string firstname, PhoneNumber phoneNumber, string email, BankAccountNumber bankAccountNumber, string lastName, DateTime dateOfBirth)
        {
            Firstname = firstname;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
       
    }
}
