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
       
        public CustomerWrite():this(string.Empty, string.Empty, string.Empty, DateTime.Now, string.Empty, string.Empty)
        {
            
        }
        public CustomerWrite(
           string firstname,
           string email,
           string lastName,
           DateTime dateOfBirth,
           string phoneNumber,
           string bankAccountNumber
           ) :
           base(firstname,  email, lastName, dateOfBirth, phoneNumber, bankAccountNumber)
        {
            
        }
    }
}
