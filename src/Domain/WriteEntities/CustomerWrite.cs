using Domain.Entities;
using Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WriteEntities
{
    public class CustomerWrite : Customer
    {
        public CustomerWrite(
           string firstname,
           PhoneNumber phoneNumber,
           string email,
           BankAccountNumber bankAccountNumber,
           string lastName,
           DateTime dateOfBirth
           ) :
           base(firstname, phoneNumber, email, bankAccountNumber, lastName, dateOfBirth)
        {
        }
    }
}
