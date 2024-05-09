using Domain.Entities;

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
