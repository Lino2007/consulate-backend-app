using NSI.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NSI.Common.Cryptography
{
    // <summary>
    /// Helper methods for encryption and descryption
    /// </summary>
    public class CryptographyHelper
    {
        private string Key { get; set; }

        private string Salt { get; set; }

        public CryptographyHelper(string key, string salt)
        {
            Key = key;
            Salt = salt;
        }

        public CryptographyHelper()
        {
            Key = ConfigHelper.GetValue("NSI.AESEncryptionKey");
            Salt = ConfigHelper.GetValue("NSI.AESEncryptionVector");
        }

        protected byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments. 
            if (string.IsNullOrWhiteSpace(plainText))
            {
                throw new ArgumentNullException("plainText");
            }

            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (var rijAlg = GetCryptoProvider(key, iv))
            {
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor();

                // Create the streams used for encryption. 
                using MemoryStream msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            // Return the encrypted bytes from the memory stream. 
            return encrypted;
        }

        protected string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (var rijAlg = GetCryptoProvider(key, iv))
            {
                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor();

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public string Encrypt(string input)
        {
            return Convert.ToBase64String(Encrypt(input, null, null));
        }

        public string Decrypt(string input)
        {
            if (!IsBase64String(input))
            {
                throw new ArgumentException("The input parameter is not base64 encoded.");
            }

            return Decrypt(Convert.FromBase64String(input), null, null);
        }

        #region Private Methods

        private static bool IsBase64String(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var param = input;
            param = param.Trim();
            return (param.Length % 4 == 0) &&
                   Regex.IsMatch(param, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        private SymmetricAlgorithm GetCryptoProvider(byte[] key = null, byte[] iv = null)
        {
            SymmetricAlgorithm provider = new RijndaelManaged();

            if (key == null || iv == null)
            {
                var saltBytes = Encoding.ASCII.GetBytes(Salt);
                var newKey = new Rfc2898DeriveBytes(Key, saltBytes);
                provider.Key = newKey.GetBytes(provider.KeySize / 8);
                provider.IV = newKey.GetBytes(provider.BlockSize / 8);
            }
            else
            {
                provider.Key = key;
                provider.IV = iv;
            }

            return provider;
        }

        #endregion
    }
}
