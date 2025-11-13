using System.Security.Cryptography;
using System.Text;

namespace Assignment_5_6_Day_Roberts_DLL
{
    public static class Crypto
    {
        /*
         *Method to hash strings with optional salting param
         */
        public static string Hash(string raw, string salt  = "")
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(raw + salt));
            var seasoned = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                seasoned.Append(bytes[i].ToString("x2"));
            }

            return seasoned.ToString();
        }

        /*
         * Method to encode and decode strings using Base64
         */
        public static string Encode64(string plaintext)
        {
            var plaintextbytes = System.Text.Encoding.UTF8.GetBytes(plaintext);
            return System.Convert.ToBase64String(plaintextbytes);
        }

        public static string Decode64(string encoded64)
        {
            var encoded64bytes = System.Convert.FromBase64String(encoded64);
            return System.Text.Encoding.UTF8.GetString(encoded64bytes);
        }
    }
}
