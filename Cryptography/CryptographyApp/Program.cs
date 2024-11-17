namespace CryptographyApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            byte[] key = Encrypt.EncryptFile("TestData.txt", "Hello World!");
            Decrypt.DecryptFile("TestData.txt", key);
        }
    }
}