using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace CryptoDes
{
    class DEScipher : ICipher
    {
        DES des;
        private byte[] CryptoTransform(ICryptoTransform transform, byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public DEScipher(CipherMode mode, PaddingMode paddingMode, byte[] key, byte[] IV)
        {
            des = new DESCryptoServiceProvider
            {
                Mode = mode,
                Padding = paddingMode,
                Key = key,
                IV = IV
            };
        }
        public DEScipher(CipherMode mode, PaddingMode paddingMode)
        {
            des = new DESCryptoServiceProvider
            {
                Mode = mode,
                Padding = paddingMode
            };
        }
        byte[] ICipher.Key
        {
            get
            {
                byte[] key = new byte[des.Key.Length];
                for (uint i = 0; i < key.Length; i++)
                    key[i] = des.Key[i];
                return key;
            }
        }
        byte[] ICipher.IV
        {
            get
            {
                byte[] IV = new byte[des.IV.Length];
                for (uint i = 0; i < IV.Length; i++)
                    IV[i] = des.IV[i];
                return IV;
            }
        }
        byte[] ICipher.Encrypt(byte[] data)
        {
            return CryptoTransform(des.CreateEncryptor(des.Key, des.IV), data);   
        }
        byte[] ICipher.Decrypt(byte[] data)
        {
            return CryptoTransform(des.CreateDecryptor(des.Key, des.IV), data);
        } 
    }
}
