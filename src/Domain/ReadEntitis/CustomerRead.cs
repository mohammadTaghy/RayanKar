using Domain.Entities;
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
            string phoneNumber,
            string email,
            string bankAccountNumber,
            string lastName) :
            base(firstname, phoneNumber, email, bankAccountNumber, lastName)
        {
        }
    }
}
