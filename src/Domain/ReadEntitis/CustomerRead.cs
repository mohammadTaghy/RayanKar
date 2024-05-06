using Domain.Entities;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ReadEntitis
{
    public class CustomerRead : Customer
    {
        public CustomerRead(
            string firstname,
           PhoneNumber phoneNumber,
           string email,
           BankAccountNumber bankAccountNumber,
           string lastName,
           DateTime dateOfBirth,
           int id
           ) :
           base(firstname, phoneNumber, email, bankAccountNumber, lastName, dateOfBirth)
        {
            this.Id = id;
        }
    }
}
