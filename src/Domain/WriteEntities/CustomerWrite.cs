using Domain.Entities;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.WriteEntities
{
    public class CustomerWrite : Customer
    {
        public PhoneNumber PhoneNumber { get; set; }
        public BankAccountNumber BankAccountNumber { get; set; }
        public CustomerWrite():this("","","",DateTime.Now, null, null)
        {
            
        }
        public CustomerWrite(
           string firstname,
           string email,
           string lastName,
           DateTime dateOfBirth,
           PhoneNumber phoneNumber,
           BankAccountNumber bankAccountNumber
           ) :
           base(firstname,  email, lastName, dateOfBirth)
        {
            this.PhoneNumber = phoneNumber;
            this.BankAccountNumber = bankAccountNumber;
        }
    }
}
