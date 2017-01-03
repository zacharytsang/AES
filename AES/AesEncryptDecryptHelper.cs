using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AES
{
    // Token: 0x02000004 RID: 4
    internal class AesEncryptDecryptHelper
    {
        // Token: 0x06000006 RID: 6 RVA: 0x00002250 File Offset: 0x00000450
        public static string AesDecrypt(string toDecrypt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(AesEncryptDecryptHelper.aeskey);
            byte[] array = Convert.FromBase64String(toDecrypt);
            ICryptoTransform cryptoTransform = new RijndaelManaged
            {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            }.CreateDecryptor();
            byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
            return Encoding.UTF8.GetString(bytes2);
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000021E0 File Offset: 0x000003E0
        public static string AesEncrypt(string toEncrypt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(AesEncryptDecryptHelper.aeskey);
            byte[] bytes2 = Encoding.UTF8.GetBytes(toEncrypt);
            ICryptoTransform cryptoTransform = new RijndaelManaged
            {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            }.CreateEncryptor();
            byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
            return Convert.ToBase64String(array, 0, array.Length);
        }

        // Token: 0x04000001 RID: 1
        public static string aeskey = "31ae590ae1eca387f4fa6801a0281fdb";
    }
}
