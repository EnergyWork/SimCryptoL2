using System.IO;
using System.Security.Cryptography;

namespace CryptoDes
{
    class AEScipher : ICipher
    {
        Aes aes;
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
        public AEScipher(CipherMode mode, PaddingMode paddingMode, byte[] key, byte[] IV)
        {
            aes = new AesCryptoServiceProvider
            {
                Mode = mode,
                Padding = paddingMode,
                Key = key,
                IV = IV
            };
        }
        public AEScipher(CipherMode mode, PaddingMode paddingMode)
        {
            aes = new AesCryptoServiceProvider
            {
                Mode = mode,
                Padding = paddingMode
            };
        }
        byte[] ICipher.Key
        {
            get
            {
                byte[] key = new byte[aes.Key.Length];
                for (uint i = 0; i < key.Length; i++)
                    key[i] = aes.Key[i];
                return key;
            }
        }
        byte[] ICipher.IV
        {
            get
            {
                byte[] IV = new byte[aes.IV.Length];
                for (uint i = 0; i < IV.Length; i++)
                    IV[i] = aes.IV[i];
                return IV;
            }
        }
        byte[] ICipher.Decrypt(byte[] data)
        {
            return CryptoTransform(aes.CreateEncryptor(), data);
        }
        byte[] ICipher.Encrypt(byte[] data)
        {
            return CryptoTransform(aes.CreateDecryptor(), data);
        }
    }
}
