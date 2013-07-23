using System.Security.Cryptography;
using System.Text;


namespace RTDataServices.Utils
{
    public class CPasswordHandlerMd5
    {
        const string ENCRYPT_STRING = "&%#@?,:*";

        static public string Encrypt(string strText)
        {
            return Encrypt(strText, ENCRYPT_STRING);
        }

        static private string Encrypt(string strText, string strEncrypt)
        {
            byte[] data = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(strText + strEncrypt));

            StringBuilder hashedString = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                hashedString.Append(data[i].ToString("x2"));
            }
            return hashedString.ToString();
        }
    }
}
