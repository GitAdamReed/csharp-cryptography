using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;

namespace CryptographyApp
{
    public static class Encrypt
    {
        // https://learn.microsoft.com/en-us/dotnet/standard/security/encrypting-data
        // https://learn.microsoft.com/en-us/dotnet/api/system.io.memorystream?view=net-8.0

        public static byte[] EncryptFile(string file, string text)
        {
            string currDir = FileSystem.CurrentDirectory;
            string textFile = Path.Combine(currDir, file);
            using (FileStream fileStream = new(textFile, FileMode.OpenOrCreate))
            {

                using (Aes aes = Aes.Create())
                {
                    //byte[] key =
                    //{
                    //    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                    //    0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                    //};
                    aes.Key = GenerateSymmetricKey();
                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);

                    using (CryptoStream cryptoStream = new(
                        fileStream,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        // By default, the StreamWriter uses UTF-8 encoding.
                        // To change the text encoding, pass the desired encoding as the second parameter.
                        // For example, new StreamWriter(cryptoStream, Encoding.Unicode).
                        using (StreamWriter encryptWriter = new(cryptoStream))
                        {
                            encryptWriter.WriteLine(text);
                        }
                        Console.WriteLine($"File {file} encrypted.");
                        return aes.Key;
                    }
                }
            }
        }

        public static byte[] GenerateSymmetricKey()
        {
            Random random = new();
            byte[] key = new byte[16];
            random.NextBytes(key);
            key.ToList().ForEach(b => Console.WriteLine(b));
            return key;
        }
    }
}
