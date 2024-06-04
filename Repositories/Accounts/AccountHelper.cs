using System;
using System.Linq;
using System.Text;

namespace BankCoreApi.Repositories.Accounts
{
    public class AccountHelper
    {
        private readonly AccountRepository _accountRepository;

        public AccountHelper (AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public static string GenerateAccountNumber(int length = 10)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateIban(string countryCode = "XX")
        {
            string accountNumber = GenerateAccountNumber();
            string bankCode = "123456"; // Example bank code, adjust as necessary
            string countryCodeDigits = ConvertCountryCodeToDigits(countryCode);

            // Calculate check digits (this is a simplified version and might not be fully compliant)
            string bban = bankCode + accountNumber;
            string checkDigits = CalculateCheckDigits(countryCodeDigits + "00" + bban);

            return $"{countryCode}{checkDigits}{bban}";
        }

        private static string ConvertCountryCodeToDigits(string countryCode)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in countryCode)
            {
                sb.Append((int)c - 55); // Convert A-Z to 10-35
            }
            return sb.ToString();
        }

        private static string CalculateCheckDigits(string input)
        {
            // Move the first four characters to the end
            string rearranged = input.Substring(4) + input.Substring(0, 4);
            string numericRearranged = string.Join("", rearranged.Select(c => char.IsLetter(c) ? (c - 55).ToString() : c.ToString()));

            // Calculate the mod 97
            int remainder = 0;
            foreach (char c in numericRearranged)
            {
                remainder = (remainder * 10 + (c - '0')) % 97;
            }

            int checkDigits = 98 - remainder;
            return checkDigits.ToString("D2"); 
        }
    }
}
