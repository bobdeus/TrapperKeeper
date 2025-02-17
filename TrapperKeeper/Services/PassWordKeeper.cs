﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TrapperKeeper.Services
{
    internal class PassWordKeeper
    {
        private List<KeptPassword> _passwords;

        internal PassWordKeeper()
        {
            _passwords = new List<KeptPassword>();
        }
        internal void KeepThis(string password, string passwordFor)
        {
            var keptPassword = KeepMyPassword(password, passwordFor);
            _passwords.Add(keptPassword);
            var serialized = JsonSerializer.Serialize(_passwords);
            File.WriteAllText(@"E:\trappedPasswords.json", serialized);
            var objectFromFile = File.ReadAllText(@"E:\trappedPasswords.json");
            _passwords = JsonSerializer.Deserialize<List<KeptPassword>>(objectFromFile);
        }

        internal List<KeptPassword> GetMyPasswords()
        {
            var objectFromFile = File.ReadAllText(@"E:\trappedPasswords.json");
            if (string.IsNullOrEmpty(objectFromFile)) return new List<KeptPassword>();
            _passwords = JsonSerializer.Deserialize<List<KeptPassword>>(objectFromFile);
            return _passwords;
        }

        private static KeptPassword KeepMyPassword([NotNull] string password, [NotNull] string passwordFor)
        {
            using var aesManaged = new AesManaged { Key = GetKey(), Mode = CipherMode.CBC };
            using ICryptoTransform encryptor = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(password);
            }

            return new KeptPassword(aesManaged.IV, Convert.ToBase64String(memoryStream.ToArray()), passwordFor);
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

        public string GetPassword(string passwordForName)
        {
            // TODO: This needs to be handled via lookup using Linq
            var passwords = _passwords.Where(name => name.PasswordFor.Equals(passwordForName)).ToList();
            return WhatsMyPassword(passwords[0]);
        }
    }

    [Serializable]
    public class KeptPassword
    {
        public KeptPassword() { }
        public string EncryptedValue { get; set; }
        public string PasswordFor { get; set; }

        public byte[] IV { get; set; }
        internal KeptPassword([NotNull] byte[] iv, [NotNull] string encryptedValue, [NotNull] string passwordFor)
        {
            IV = iv;
            EncryptedValue = encryptedValue;
            PasswordFor = passwordFor;
        }

        public override string ToString()
        {
            return EncryptedValue;
        }
    }
}