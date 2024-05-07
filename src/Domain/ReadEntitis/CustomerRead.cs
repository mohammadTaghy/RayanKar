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
           string email,
           string lastName,
           DateTime dateOfBirth,
           int id,
           string phoneNumber,
           string bankAccountNumber
           ) :
           base(firstname, email, lastName, dateOfBirth, phoneNumber, bankAccountNumber)
        {
            this.Id = id;
        }
    }
}
