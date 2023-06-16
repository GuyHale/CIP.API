using CIP.API.Interfaces;
using CIP.API.Models.Users;
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
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(signUpUser.Password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);

            signUpUser.Salt = BitConverter.ToString(salt);
            signUpUser.Password = Convert.ToHexString(hash);
        }
    }
}
