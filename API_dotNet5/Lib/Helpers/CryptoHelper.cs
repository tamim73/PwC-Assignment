using System;
using System.Security.Cryptography;

namespace Lib.Helpers
{
    public static class CryptoHelper
    {
        private const int HashSize = 20, SaltSize = 16, HashIterations = 10000;

        public static string HashPassword(string password)
        {
            byte[] hash;
            byte[] salt = new byte[SaltSize];
            byte[] hashedPasswordBytes = new byte[SaltSize + HashSize];

            new RNGCryptoServiceProvider().GetBytes(salt);
            hash = new Rfc2898DeriveBytes(password, salt, HashIterations).GetBytes(HashSize);

            Array.Copy(salt, 0, hashedPasswordBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashedPasswordBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashedPasswordBytes);
        }

        public static bool VerifyHashedPassword(string password, string hashedPassword)
        {
            byte[] hash = new byte[HashSize];
            byte[] salt = new byte[SaltSize];
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            Array.Copy(hashedPasswordBytes, 0, salt, 0, SaltSize);
            Array.Copy(hashedPasswordBytes, SaltSize, hash, 0, HashSize);

            byte[] bytesToTest = new Rfc2898DeriveBytes(password, salt, HashIterations).GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (bytesToTest[i] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

}
