using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConnectionKey
{
    public class MyEncrypt
    {
        /// <summary>
        /// key 長度可為:128bit or 192 bit or 256 bit
        /// </summary>
        public static string key = "MEGASSO_MEGASSO_MEGASSO_MEGASSO_";
        /// <summary>
        /// IV( Initialization Vector) 長度固定為 128 bit
        /// </summary>
        public static string iv = "MEGASSO_MEGASSO_";

        /// <summary>
        /// Encrypt 將文字加密回傳暗碼
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public String Encrypt(String data)
        {
            if (!String.IsNullOrWhiteSpace(data))
            {
                byte[] encrypted;
                // Create an Rijndael object
                using (Rijndael rijAlg = Rijndael.Create())
                {
                    rijAlg.Key = Encoding.UTF8.GetBytes(key);
                    rijAlg.IV = Encoding.UTF8.GetBytes(iv);

                    //在衍生類別中覆寫時，使用指定的 System.Security.Cryptography.SymmetricAlgorithm.Key 屬性和初始化向量
                    //(System.Security.Cryptography.SymmetricAlgorithm.IV) 建立對稱加密子物件。
                    // create a encrypt to perform the stream transform
                    ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {

                                //Write all data to the stream.
                                swEncrypt.Write(data.Trim());
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(encrypted);
            }

            return "";
        }

        /// <summary>
        /// 將暗碼轉成明碼
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public String Decrypt(String data)
        {
            if (!String.IsNullOrWhiteSpace(data))
            {
                String plaintext = "";

                using (Rijndael rijAlg = Rijndael.Create())
                {
                    byte[] cipherText = Convert.FromBase64String(data);

                    rijAlg.Key = Encoding.UTF8.GetBytes(key);
                    rijAlg.IV = Encoding.UTF8.GetBytes(iv);

                    ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
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
            return "";
        }
    }
    public class Parameters
    {
        public static readonly string ConnectionString = "data source=.;initial catalog=DotrA;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
    }
}
