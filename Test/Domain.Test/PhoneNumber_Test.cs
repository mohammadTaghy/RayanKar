using Domain.ValueObject;
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
        [Theory]
        [InlineData("+989384563280", "")]
        [InlineData("+9893845632801", DomainMessages.InValidNumber)]
        [InlineData("+982133367289", "")]
        public void PhoneNumber_Validation_Test(string number, string message)
        {
            try
            {
                PhoneNumber phoneNumber = new PhoneNumber(number);

                // Assert
                Assert.Equal(number, phoneNumber.ToString());

            }
            catch (Exception)
            {
                var result = Assert.Throws<ArgumentException>(() => new PhoneNumber(number));
                Assert.Equal(message, result.Message);
            }
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
