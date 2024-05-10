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
        public DateTime DateOfBirth { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
        protected Customer(
            string firstname, 
            string email, 
            string lastName, 
            DateTime dateOfBirth,
           string phoneNumber,
           string bankAccountNumber)
        {
            Firstname = firstname;
            Email = email;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            this.PhoneNumber = phoneNumber;
            this.BankAccountNumber = bankAccountNumber;
        }
       
    }
}
