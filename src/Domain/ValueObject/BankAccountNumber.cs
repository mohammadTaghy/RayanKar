using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.ValueObject
{
    public sealed class BankAccountNumber : IEquatable<BankAccountNumber>
    {
        public readonly string BankAccount;
        private BankAccountNumber() : this("")
        {

        }
        public BankAccountNumber(string number)
        {
            BankAccount = Validation(number) ? 
                number : 
                throw new ArgumentException(DomainMessages.InValidBankAccountNumber);
        }
        public bool Validation(string number)
        {
            number = number.ToUpper(); //IN ORDER TO COPE WITH THE REGEX BELOW
            if (string.IsNullOrEmpty(number))
                return false;
            try
            {
                return CheckNumberIsValid(number);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CheckNumberIsValid(string number)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(number, "^[A-Z0-9]"))
            {
                number = number.Replace(" ", string.Empty);
                string bank =
                number.Substring(4, number.Length - 4) + number.Substring(0, 4);
                int asciiShift = 55;
                string checkSumString = SumBankAccountNumber(bank, asciiShift);
                return CheckSum(checkSumString);
            }
            return false;
        }
        private string SumBankAccountNumber(string bank, int asciiShift)
        {
            StringBuilder sb = new();
            foreach (char c in bank)
            {
                int v;
                if (char.IsLetter(c)) v = c - asciiShift;
                else v = int.Parse(c.ToString());
                sb.Append(v);
            }
            return sb.ToString();
        }
        private bool CheckSum(string checkSumString)
        {
            int checksum = int.Parse(checkSumString.Substring(0, 1));
            for (int i = 1; i < checkSumString.Length; i++)
            {
                int v = int.Parse(checkSumString.Substring(i, 1));
                checksum *= 10;
                checksum += v;
                checksum %= 97;
            }
            return checksum == 1;
        }

        public override string ToString()
        {
            return BankAccount;
        }
        public bool Equals(BankAccountNumber? other)
        {
            if (other is null)
                return false;

            return BankAccount == other.BankAccount;
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as BankAccountNumber);
        }

        public override int GetHashCode()
        {
            return BankAccount.GetHashCode();
        }
    }
}
