using System;
using System.Text;
using System.Security.Cryptography;


namespace rsacrypt
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Enter the string: ");
                string InPutString = Console.ReadLine();

                var RSACrypto = new RSACryptoServiceProvider();
                var ByteConverter = new UnicodeEncoding();

                byte[] dataToEncrypt = ByteConverter.GetBytes(InPutString);
                byte[] encryptedData;
                byte[] decryptedData;

                encryptedData = RSAEncrypt(dataToEncrypt, RSACrypto.ExportParameters(false), false);

                decryptedData = RSADecrypt(encryptedData, RSACrypto.ExportParameters(true), false);

                Console.WriteLine("Decrypted text: {0}", ByteConverter.GetString(decryptedData));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }

    }
}
