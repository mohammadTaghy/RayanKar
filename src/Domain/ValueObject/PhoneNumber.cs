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
    public sealed class PhoneNumber : IEquatable<PhoneNumber>
    {
        public readonly string Phone;
        private PhoneNumber() : this("")
        {

        }
        public PhoneNumber(string number)
        {
            PhoneNumberType phoneNumberType = GetPhoneNumberType(number);
            this.Phone = phoneNumberType == PhoneNumberType.MOBILE || phoneNumberType == PhoneNumberType.FIXED_LINE || phoneNumberType == PhoneNumberType.FIXED_LINE_OR_MOBILE ?
                number :
                throw new ArgumentException("PhoneNumber is incorrect");
        }

        private PhoneNumberType GetPhoneNumberType(string number)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(number, null);
            PhoneNumberType phoneNumberType = phoneUtil.GetNumberType(phoneNumber);
            return phoneNumberType;
        }

        public bool Equals(PhoneNumber? other)
        {
            if (other is null)
                return false;

            return Phone == other.Phone;
        }
        public override string ToString()
        {
            return Phone;
        }
    }
}
