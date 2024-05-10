
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedProject.Validation
{
    public sealed class BankAccountNumber 
    {
        private BankAccountNumber()
        {
        }

        public static bool Validate(string number)
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
        private static bool CheckNumberIsValid(string number)
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
        private static string SumBankAccountNumber(string bank, int asciiShift)
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
        private static bool CheckSum(string checkSumString)
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

    }
}
