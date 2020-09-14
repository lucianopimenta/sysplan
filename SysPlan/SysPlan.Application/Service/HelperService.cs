using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SysPlan.Application.Service
{
    public static class HelperService
    {
        public static string GeraSenha()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, 5)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }

        public static string ComputeHashMd5(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var algorithm = new MD5CryptoServiceProvider();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public static string ComputeHashSha256(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var algorithm = new SHA256CryptoServiceProvider();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
