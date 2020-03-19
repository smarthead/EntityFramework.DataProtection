using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using EF.DataProtection.Abstractions.Abstractions;
using Microsoft.Extensions.Options;

namespace EF.DataProtection.Services.Aes256
{
    public class Aes256DataEncryptor : IDataEncryptor
    {
        private readonly byte[] _key;

        private const int KeySize = 256;
        private const int BlockSize = 128;
        private const CipherMode Mode = CipherMode.CBC;
        private const PaddingMode PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7;

        public Aes256DataEncryptor(byte[] key)
        {
            _key = key;
        }
        
        public Aes256DataEncryptor(IOptions<Aes256DataEncryptorOptions> options) 
            : this(options.Value.GetBytes())
        {
        }
        
        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;
            
            using (var aesAlg = AesManaged.Create())
            {
                aesAlg.KeySize = KeySize;
                aesAlg.BlockSize = BlockSize;
                aesAlg.Mode = Mode;
                aesAlg.Padding = PaddingMode;

                aesAlg.Key = _key;
                aesAlg.GenerateIV();

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        //Concatenate the byte array of the IV and the encrypted data 
                        //and convert to base-64 string
                        var encrypted = msEncrypt.ToArray();
                        return Convert.ToBase64String(aesAlg.IV
                            .Concat(encrypted)
                            .ToArray());
                    }
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return null;
            
            using (var aesAlg = AesManaged.Create())
            {
                aesAlg.KeySize = KeySize;
                aesAlg.BlockSize = BlockSize;
                aesAlg.Mode = Mode;
                aesAlg.Padding = PaddingMode;

                var encrypted = Convert
                    .FromBase64String(cipherText)
                    .ToArray();

                aesAlg.Key = _key;
                aesAlg.IV = encrypted
                    .Take(aesAlg.BlockSize >> 3)
                    .ToArray();

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                var encryptedValue = encrypted
                    .Skip(aesAlg.BlockSize >> 3)
                    .ToArray();

                using (var msDecrypt = new MemoryStream(encryptedValue))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}