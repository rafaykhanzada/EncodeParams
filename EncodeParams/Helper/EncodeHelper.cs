using System.Security.Cryptography;
using System.Text;

namespace EncodeParams.Helper
{
    public class EncodeHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("TECHDawlance2022");
        private static readonly byte[] Iv = Encoding.UTF8.GetBytes("Fin@Dawlance2022");

        public static string Encrypt(string plainText)
        {
            byte[] encryptedBytes;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = Iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Convert the plaintext string to a byte array
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // Create a memory stream to receive the encrypted bytes
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    // Create a crypto stream that uses the memory stream and the encryptor to perform the encryption
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        // Write the encrypted data to the memory stream
                        csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                    }

                    // Convert the encrypted memory stream to a byte array
                    encryptedBytes = msEncrypt.ToArray();
                }
            }

            // Convert the encrypted byte array to a base64 string and return it
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cipherText)
        {
            byte[] iv = Encoding.UTF8.GetBytes(ConfigHelper.config["PublicIV"]);
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ConfigHelper.config["SecretKey"]);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
