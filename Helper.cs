using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NFAuthenicationKeyCli
{
    class Helper
    {
        public static void AssertCookies(JArray cookies)
        {
            if (cookies.Count == 0)
                throw new InvalidDataException("Not found cookies");

            List<string> loginCookies = new List<string> { "memclid", "nfvdid", "SecureNetflixId", "NetflixId" };
            foreach (string cookieName in loginCookies)
            {
                if (cookies.Children<JObject>().FirstOrDefault(o => o["name"].ToString() == cookieName) == null)
                    throw new InvalidDataException("Not found cookies");
            };
        }

        public static void SaveData(JObject data, string pin, string outputFileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), outputFileName);

            File.WriteAllText(filePath, EncryptDataAES(pin, data.ToString()));
        }

        private static string EncryptDataAES(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                /*
                // Function to ensure 16 bytes length in the key
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] safeKeyBytes = new byte[16];
                int len = keyBytes.Length;
                if (len > safeKeyBytes.Length)
                {
                    len = safeKeyBytes.Length;
                }
                Array.Copy(keyBytes, safeKeyBytes, len);
                */
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(key + key + key + key); // The key must have 16 byte
                aes.IV = iv;  // Set as bytes null

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
    }
}