using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.ValueObject
{
    public sealed class PhoneNumber 
    {
        
        public static bool Validate(string number)
        {
            PhoneNumberType phoneNumberType = GetPhoneNumberType(number);
            return phoneNumberType == PhoneNumberType.MOBILE || phoneNumberType == PhoneNumberType.FIXED_LINE || phoneNumberType == PhoneNumberType.FIXED_LINE_OR_MOBILE;
        }

        private static PhoneNumberType GetPhoneNumberType(string number)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(number, null);
            PhoneNumberType phoneNumberType = phoneUtil.GetNumberType(phoneNumber);
            return phoneNumberType;
        }

    }
}
