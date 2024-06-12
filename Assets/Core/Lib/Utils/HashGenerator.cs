using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Lib
{
    public static class HashGenerator
    {
        public static int Generate() => GenerateHash(Guid.NewGuid().ToString());

        private static int GenerateHash(string value)
        {
            var md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
            return BitConverter.ToInt32(hashed, 0);
        }
    }
}