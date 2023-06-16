using CIP.API.Interfaces;
using CIP.API.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace CIP.API.Helpers
{
    public static class Encryption
    {
        private const int _keySize = 64;
        private const int _iterations = 350000;
        private static HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
        public static void PasswordHasher(this SignUpUser signUpUser)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_keySize);
            byte[] hash = Hasher(signUpUser.Password, salt);

            signUpUser.Salt = BitConverter.ToString(salt);
            signUpUser.Password = Convert.ToHexString(hash);
        }

        public static bool PasswordVerification(this User user, LoginUser loginUser)
        {
            byte[] hash = Hasher(loginUser.Password, Encoding.UTF8.GetBytes(user.Salt));
            return user.Password == Convert.ToHexString(hash);
        }

        private static byte[] Hasher(string password, byte[] salt)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);
        }
    }
}
