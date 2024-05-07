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
