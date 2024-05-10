using SharedProject.Validation;

namespace SharedProject.Test
{
    public class BankAccountNumber_Test
    {
        [Theory]
        [InlineData("IR00000000000001",false)]
        [InlineData("IR830120010000001387998021", true)]
        public void BankAccountNumberr_Validation_Test(string bankAcountNumber, bool expected)
        {

            bool result =  BankAccountNumber.Validate(bankAcountNumber);

            Assert.Equal(expected, result);
        }
    }
}
