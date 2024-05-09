using Domain.Entities;

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
