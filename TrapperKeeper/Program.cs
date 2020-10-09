using System;
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
            Console.WriteLine(KeepMyPassword(args[0]));
        }

        private static string KeepMyPassword(string password)
        {
            using var aesManaged = new AesManaged {KeySize = 256};
            using var sha256Managed = new SHA256Managed();
            using var md5Managed = new MD5CryptoServiceProvider();
            var unHashedKey = Encoding.UTF8.GetBytes("key");
            var unHashedIv = Encoding.UTF8.GetBytes("iv");
            var key = sha256Managed.ComputeHash(unHashedKey);
            var iv = md5Managed.ComputeHash(unHashedIv);
            using ICryptoTransform encryptor = aesManaged.CreateEncryptor(key, iv);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using var streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(password);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}