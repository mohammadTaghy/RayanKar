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
        private readonly string _phoneNumber;

        public PhoneNumber(string number)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            PhoneNumbers.PhoneNumber phoneNumber = phoneUtil.Parse(number, null);
            PhoneNumberType phoneNumberType = phoneUtil.GetNumberType(phoneNumber);
            this._phoneNumber = phoneNumberType == PhoneNumberType.MOBILE|| phoneNumberType==PhoneNumberType.FIXED_LINE || phoneNumberType == PhoneNumberType.FIXED_LINE_OR_MOBILE ?
                number :
                throw new ArgumentException("PhoneNumber is incorrect");
        }
        public bool Equals(PhoneNumber? other)
        {
            if (other is null)
                return false;

            return _phoneNumber == other._phoneNumber;
        }
        public override string ToString()
        {
            return _phoneNumber;
        }
    }
}
