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
            var des2 = new DESCryptoServiceProvider
            {
                Mode = des.Mode,
                Padding = des.Padding,
                Key = des.Key,
                IV = des.IV
            };
            return CryptoTransform(des2.CreateEncryptor(), data);   
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

            var des2 = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des2.CreateEncryptor(des.Key, des.IV), CryptoStreamMode.Write);

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
        
        byte[] ICipher.Decrypt(byte[] data)
        {
            var des2 = new DESCryptoServiceProvider
            {
                Mode = des.Mode,
                Padding = des.Padding,
                Key = des.Key,
                IV = des.IV
            };
            return CryptoTransform(des2.CreateDecryptor(), data);
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

            var des2 = new DESCryptoServiceProvider();
            CryptoStream decStream = new CryptoStream(fout, des2.CreateDecryptor(des.Key, des.IV), CryptoStreamMode.Write);

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
