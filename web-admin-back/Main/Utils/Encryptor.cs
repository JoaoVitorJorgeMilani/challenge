using System.Security.Cryptography;
using MongoDB.Bson;

namespace Main.Utils
{
    public class Encryptor : IEncryptor
    {

        private Aes myAes;

        public Encryptor()
        {
            this.myAes = Aes.Create();
        }

        private void validateEnvironment()
        {
            if (myAes.Key == null || myAes.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (myAes.IV == null || myAes.IV.Length <= 0)
                throw new ArgumentNullException("IV");
        }

        public string Encrypt(ObjectId? id)
        {
            if (id == null)
                return string.Empty;

            return Encrypt(id.ToString());
        }

        public string Encrypt(string? str)
        {
            if (str == null || str.Length <= 0)
                throw new ArgumentNullException("plainText");

            validateEnvironment();

            byte[] encrypted;

            ICryptoTransform encryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(str);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted.ToArray());
        }


        public ObjectId DecryptObjectId(string encryptStr)
        {
            string str = Decrypt(encryptStr);

            if (string.IsNullOrEmpty(str))
                return ObjectId.Empty;

            return ObjectId.TryParse(str, out var id) ? id : ObjectId.Empty;
        }

        public string Decrypt(string encryptedStr)
        {
            byte[] cipherBytes = Convert.FromBase64String(encryptedStr);

            if (cipherBytes == null || cipherBytes.Length <= 0)
            {
                throw new ArgumentNullException("encryptedStr");
            }

            validateEnvironment();

            string plaintext;

            ICryptoTransform decryptor = myAes.CreateDecryptor(myAes.Key, myAes.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        try
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                        catch (Exception)
                        {
                            plaintext = string.Empty;
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}