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

            bool result = PhoneNumber.Validate(number);

            Assert.False(result);
        }
        [Fact]
        public void PhoneNumber_ValidationSuccess_Test()
        {
            string _mobileNumber = "+989384563280";
            string _phoneNumber = "+982133367289";

            bool mobileNumberValid = PhoneNumber.Validate(_mobileNumber);
            bool phoneNumberValid = PhoneNumber.Validate(_phoneNumber);

            Assert.True(mobileNumberValid);
            Assert.True(phoneNumberValid);
        }
        
    }
}
