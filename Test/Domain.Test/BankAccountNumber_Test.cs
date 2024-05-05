using Domain.ValueObject;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Test
{
    public class BankAccountNumber_Test
    {
        [Fact]
        public void BankAccountNumberr_ValidationError_Test()
        {
            string number = "IR00000000000001";

            var result = Assert.Throws<ArgumentException>(() => new BankAccountNumber(number));

            Assert.Equal(DomainMessages.InValidBankAccountNumber, result.Message);
        }
        [Fact]
        public void BankAccountNumberr_ValidationSuccess_Test()
        {
            string number = "IR830120010000001387998021";

            BankAccountNumber bankAccountNumber =  new BankAccountNumber(number);

            Assert.Equal(number, bankAccountNumber.ToString());
        }
        [Theory]
        [InlineData("IR830120010000001387998021", "IR830120010000001387998021", true)]
        [InlineData("IR830120010000001387998021", "IR062960000000100324200001", false)]
        public void BankAccountNumberr_Equal_Test(string number1, string number2, bool expected)
        {
            BankAccountNumber bankAccountNumber1 = new BankAccountNumber(number1);
            BankAccountNumber bankAccountNumber2 = new BankAccountNumber(number2);

            Assert.Equal(expected, bankAccountNumber1.Equals(bankAccountNumber2));

        }
    }
}
