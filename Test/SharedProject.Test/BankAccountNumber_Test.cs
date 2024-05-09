using SharedProject.Validation;

namespace SharedProject.Test
{
    public class BankAccountNumber_Test
    {
        [Fact]
        public void BankAccountNumberr_ValidationError_Test()
        {
            string number = "IR00000000000001";

            bool result =  BankAccountNumber.Validate(number);

            Assert.False(result);
        }
        [Fact]
        public void BankAccountNumberr_ValidationSuccess_Test()
        {
            string number = "IR830120010000001387998021";

            bool valid =  BankAccountNumber.Validate(number);

            Assert.True(valid);
        }
        
    }
}
