using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace OTS.WebLib.encryption
{
	/// <summary>
	/// Encryption Utilities
	/// Innotech
	/// </summary>
	public class MEncryption
	{
        public const string DEFAULT_ENCRYPTION_KEY = "cop!~9]Z";
                
        /// <summary>
        /// encrypt
        /// </summary>
        /// <param name="stringToDecrypt"></param>
        /// <param name="sEncryptionKey"></param>
        /// <returns></returns>
        public string decrypt(string stringToDecrypt, string sEncryptionKey)
        {   
            // SEncryptionKey phai dai 8 ky tu
            byte[] inputByteArray = new byte[stringToDecrypt.Length];
            try
            {   
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = ASCIIEncoding.ASCII.GetBytes(sEncryptionKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sEncryptionKey);

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// decrypt
        /// </summary>
        /// <param name="stringToEncrypt"></param>
        /// <param name="SEncryptionKey"></param>
        /// <returns></returns>
        public string encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {  
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = ASCIIEncoding.ASCII.GetBytes(SEncryptionKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(SEncryptionKey);

                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


		/// <summary>
		/// Calculates the MD5 of a given string.
		/// </summary>
		/// <param name="inputString"></param>
		/// <returns>The (hexadecimal) string representatation of the MD5 hash.</returns>
		public static string StringToMD5Hash(string inputString)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < encryptedBytes.Length; i++)
			{
				sb.AppendFormat("{0:x2}", encryptedBytes[i]);
			}
			return sb.ToString();
		}

	}
}
