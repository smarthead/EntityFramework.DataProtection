using EF.DataProtection.Abstractions.Abstractions;
using EF.DataProtection.Services.Aes256;
using Xunit;

namespace EF.DataProtection.Core.Tests.Services
{
    public class Aes256DataEncryptorTests
    {
        private readonly IDataEncryptor _aes256Encryptor;

        public Aes256DataEncryptorTests()
        {
            var options = new Aes256DataEncryptorOptions
            {
                Password = "Password",
                Salt = "Salt"
            };
            
            _aes256Encryptor = new Aes256DataEncryptor(options.GetBytes());
        }
        
        [Fact]
        public void Should_ReturnCiphertext_ForGivenString()
        {
            var password = "my_strong_password";

            var encrypted = _aes256Encryptor.Encrypt(password);

            Assert.NotNull(encrypted);
            Assert.NotEqual(password, encrypted);
        }

        [Fact]
        public void Should_ReturnDecryptedText_ForGivenCipher()
        {
            var password = "1234567890";

            var encrypted = _aes256Encryptor.Encrypt(password);;
            var decrypted = _aes256Encryptor.Decrypt(encrypted);;

            Assert.NotNull(decrypted);
            Assert.Equal(password, decrypted);
        }
    }
}