using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TrapperKeeper.Services
{
    internal class PassWordKeeper
    {
        internal void KeepThis(string password, string passwordFor)
        {
            var keptPassword = KeepMyPassword(password);
            var serialized = JsonSerializer.Serialize(keptPassword);
            File.WriteAllText(@"E:\trappedPasswords.json", serialized);
            var objectFromFile = File.ReadAllText(@"E:\trappedPasswords.json");
            KeptPassword currentPasswords = JsonSerializer.Deserialize<KeptPassword>(objectFromFile);
            Console.WriteLine(WhatsMyPassword(currentPasswords));
        }

        internal KeptPassword GetMyPasswords()
        {
            throw new NotImplementedException();
        }

        private static KeptPassword KeepMyPassword([NotNull] string password)
        {
            using var aesManaged = new AesManaged { Key = GetKey(), Mode = CipherMode.CBC };
            using ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(password);
            }

            return new KeptPassword(aesManaged.IV, Convert.ToBase64String(memoryStream.ToArray()));
        }

        private static string WhatsMyPassword([NotNull] KeptPassword keptPassword)
        {
            using var aesManaged = new AesManaged { Key = GetKey(), IV = keptPassword.IV, Mode = CipherMode.CBC };
            using ICryptoTransform encryptor = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV);
            using var memoryStream = new MemoryStream(Convert.FromBase64String(keptPassword.EncryptedValue));
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
                unHashedKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("TRAPPER_KEEPER_PASSWORD") ?? throw new ArgumentNullException());
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidOperationException("You must set a \"TRAPPER_KEEPER_PASSWORD\" in your environment variables.", e);
            }

            return sha256Managed.ComputeHash(unHashedKey);
        }
    }

    [Serializable]
    public class KeptPassword
    {
        public KeptPassword() { }
        public string EncryptedValue { get; set; }

        public byte[] IV { get; set; }
        internal KeptPassword([NotNull] byte[] iv, [NotNull] string encryptedValue)
        {
            IV = iv;
            EncryptedValue = encryptedValue;
        }

        public override string ToString()
        {
            return EncryptedValue;
        }
    }
}