using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MigrateDataTCSC.Utils
{
    public class Common
    {
        public static string Decrypt(string password)
        {
            return RijndaelEncryption.Decrypt(password, RijndaelEncryption.PASSPHASE, RijndaelEncryption.SALTVALUE,
                                       RijndaelEncryption.HASHALGORITHM, RijndaelEncryption.PASSINTERATIONS,
                                       RijndaelEncryption.INITVECTOR, RijndaelEncryption.KEYSIZE);
        }

        public static string Encryt(string password)
        {
            return RijndaelEncryption.Encrypt(password, RijndaelEncryption.PASSPHASE, RijndaelEncryption.SALTVALUE,
                                              RijndaelEncryption.HASHALGORITHM, RijndaelEncryption.PASSINTERATIONS,
                                              RijndaelEncryption.INITVECTOR, RijndaelEncryption.KEYSIZE);
        }
    }
}
