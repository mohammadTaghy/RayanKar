using Domain.ValueObject;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Test
{
    public class PhoneNumber_Test
    {
        [Fact]
        public void PhoneNumber_ValidationError_Test()
        {
            string number = "+9893845632801";

            var result = Assert.Throws<ArgumentException>(() => new PhoneNumber(number));

            Assert.Equal(DomainMessages.InValidPhonNumber, result.Message);
        }
        [Fact]
        public void PhoneNumber_ValidationSuccess_Test()
        {
            string _mobileNumber = "+989384563280";
            string _phoneNumber = "+989384563280";

            PhoneNumber mobileNumber = new PhoneNumber(_mobileNumber);
            PhoneNumber phoneNumber = new PhoneNumber(_phoneNumber);

            Assert.Equal(_phoneNumber, phoneNumber.ToString());
            Assert.Equal(_mobileNumber, mobileNumber.ToString());
        }
        
        [Theory]
        [InlineData("+989384563280", "+989384563280", true)]
        [InlineData("+982133367289", "+982133367288", false)]
        public void PhoneNumber_Equal_Test(string number1, string number2, bool expected)
        {

            PhoneNumber phoneNumber1 = new PhoneNumber(number1);
            PhoneNumber phoneNumber2 = new PhoneNumber(number2);

            Assert.Equal(expected, phoneNumber1.Equals(phoneNumber2));

        }
        [Fact]
        public void PhoneNumber_ToString_Test()
        {
            string number = "+982133367289";

            PhoneNumber phoneNumber1 = new PhoneNumber(number);

            Assert.Equal(number, phoneNumber1.ToString());
        }
    }
}
