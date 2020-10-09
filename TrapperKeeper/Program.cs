using System;
using System.Buffers.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TrapperKeeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var encryptedPass = KeepMyPassword(args[0]);
            Console.WriteLine(encryptedPass);
            Console.WriteLine(WhatsMyPassword(encryptedPass));
        }

        private static string KeepMyPassword(string password)
        {
            using var aesManaged = new AesManaged{Key = GetKey(), Mode = CipherMode.CBC};
            using ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using(var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(password);
            }
            return Convert.ToBase64String(memoryStream.ToArray());
        }
        
        private static string WhatsMyPassword(string password)
        {
            using var aesManaged = new AesManaged{Key = GetKey(), Mode = CipherMode.CBC};
            using ICryptoTransform encryptor = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV);
            using var memoryStream = new MemoryStream(Convert.FromBase64String(password));
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        private static byte[] GetKey()
        {
            using var sha256Managed = new SHA256Managed();
            byte[] unHashedKey;
            try
            {
                unHashedKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TRAPPER_KEEPER_PASSWORD"));
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidOperationException("You must set a \"TRAPPER_KEEPER_PASSWORD\" in your environment variables.");
            }

            return sha256Managed.ComputeHash(unHashedKey);
        }
        private static byte[] GetIv()
        {
            using var md5Managed = new MD5CryptoServiceProvider();
            var unHashedIv = Encoding.UTF8.GetBytes("iv");
            return md5Managed.ComputeHash(unHashedIv);
        }
    }
}