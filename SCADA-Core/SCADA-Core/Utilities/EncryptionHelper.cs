using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Utilities
{
    public class EncryptionHelper
    {
        public static string EncryptPassword(string password)
        {
            var salt = GenerateSalt();
            var saltedPassword = Encoding.UTF8.GetBytes(salt + password);
            using (var sha = new SHA256Managed())
            {
                var hash = sha.ComputeHash(saltedPassword);
                return $"{Convert.ToBase64String(hash)}:{salt}";
            }
        }

        public static bool ValidatePassword(string password, string storedEncryptedPassword)
        {
            var parts = storedEncryptedPassword.Split(':');
            var storedHash = parts[0];
            var salt = parts[1];

            var saltedPassword = Encoding.UTF8.GetBytes(salt + password);
            using (var sha = new SHA256Managed())
            {
                var hash = sha.ComputeHash(saltedPassword);
                var computedHash = Convert.ToBase64String(hash);
                return storedHash == computedHash;
            }
        }

        private static string GenerateSalt()
        {
            using (var crypto = new RNGCryptoServiceProvider())
            {
                var salt = new byte[32];
                crypto.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }
    }

}
