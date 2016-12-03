using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sniper.Core.Encyption
{
    public  class EncryptorHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string GetMD5(string sourceString)
        {
            MD5 md5 = MD5.Create();
            byte[] source = md5.ComputeHash(Encoding.UTF8.GetBytes(sourceString));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                sBuilder.Append(source[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string EncodeBase64(string sourceString)
        {
            byte[] bt = Encoding.UTF8.GetBytes(sourceString);
            return Convert.ToBase64String(bt);
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DecodeBase64(string sourceString)
        {
            byte[] bt = Convert.FromBase64String(sourceString);
            return Encoding.UTF8.GetString(bt);
        }


        /// <summary>
        /// 使用加密服务提供程序实现加密生成随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns>16进制格式字符串</returns>
        public static string CreateMachineKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];
            rng.GetBytes(buff);
            System.Text.StringBuilder hexString = new System.Text.StringBuilder(64);
            for (int i = 0; i < buff.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", buff[i]));
            }
            return hexString.ToString();
        }

        /// <summary>
        /// Des加密方法
        /// </summary>
        /// <param name="val">待加密数据</param>
        /// <param name="key">8位字符</param>
        /// <param name="IV">8位字符</param>
        /// <returns></returns>
        public static string DESEncrypt(string val, string key, string iv)
        {
            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);
                sw.Write(val);
                sw.Flush();
                cst.FlushFinalBlock();
                sw.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch// (Exception ex)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Des解密方法
        /// </summary>
        /// <param name="val">待解密数据</param>
        /// <param name="key">8位字符</param>
        /// <param name="iv">8位字符</param>
        /// <returns></returns>
        public static string DESDecrypt(string val, string key, string iv)
        {
            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

                byte[] byEnc;
                try
                {
                    byEnc = Convert.FromBase64String(val);
                }
                catch
                {
                    return null;
                }
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream(byEnc);
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cst);
                return sr.ReadToEnd();
            }
            catch// (System.Exception ex)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 生成一对秘钥。arr[0]私钥，arr[1]公钥
        /// </summary>
        /// <returns></returns>
        public static string[] GenerateRSAKeys()
        {
            string[] sKeys = new String[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);
            return sKeys;
        }

        ///<summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey">公钥</param>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        public static string RSAEncrypt(string publickey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey">私钥</param>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string RSADecrypt(string privatekey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }


        /// <summary>
        /// 生成Salt盐
        /// </summary>
        /// <param name="size">随机数长度，默认32字节</param>
        /// <returns></returns>
        public static string CreateSaltKey(int size =32)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

    }
}
