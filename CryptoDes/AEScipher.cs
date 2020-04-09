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
                    //cryptoStream.FlushFinalBlock();
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

        void ICipher.Encrypt(string fileIn, string fileOut)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[200];  //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            var aes2 = new AesCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, aes2.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 200);
                encStream.Write(bin, 0, len);
                rdlen += len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        void ICipher.Decrypt(string fileIn, string fileOut)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[200];  //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            var aes2 = new AesCryptoServiceProvider();
            CryptoStream decStream = new CryptoStream(fout, aes2.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 200);
                decStream.Write(bin, 0, len);
                rdlen += len;
            }

            decStream.Close();
            fout.Close();
            fin.Close();
        }
    }
}
