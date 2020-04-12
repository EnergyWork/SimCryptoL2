using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace CryptoDes
{
    enum FileType
    {
        none,
        txt,
        img
    }
    public partial class MainForm : Form
    {
        string file;
        FileType fileType;
        Image imageIn, imageOut;
        ICipher cryptoProvider;
        uint fiveSecond;

        private CipherMode CurrentMode()
        {
            /*
             CBC CFB CTS ECB OFB
             */
            CipherMode mode;
            switch(cbConnectBlockAlg.SelectedIndex)
            {
                case 0:  mode = CipherMode.CBC; break;
                case 1:  mode = CipherMode.CFB; break;
                case 2:  mode = CipherMode.CTS; break;
                case 3:  mode = CipherMode.ECB; break;
                case 4:  mode = CipherMode.OFB; break;
                default: mode = CipherMode.CBC; break;
            }
            return mode;
        }
        private PaddingMode CurrentPaddingMode()
        {
            /*
             None Zeros ANSIX923 ISO10126 PKCS7
             */
            PaddingMode paddingMode;
            switch(cbConnectBlockType.SelectedIndex)
            {
                case 0:  paddingMode = PaddingMode.Zeros;    break;
                case 1:  paddingMode = PaddingMode.ANSIX923; break;
                case 2:  paddingMode = PaddingMode.ISO10126; break;
                case 3:  paddingMode = PaddingMode.PKCS7;    break;
                default: paddingMode = PaddingMode.Zeros;     break;
            }
            return paddingMode;
        }
        private void StartTimer()
        {
            fiveSecond = 0;
            timerLogClear.Enabled = true;
        }
        private bool Check()
        {
            if (file != string.Empty)
            {
                if (tbKey.Text != string.Empty)
                {
                    if (tbIV.Text != string.Empty)
                    {
                        return true;
                    }
                    else
                    {
                        labelLog.Text = "Вектор инициализации не инициализирован!";
                        StartTimer();
                    }
                }
                else
                {
                    labelLog.Text = "Ключ не инициализирован!";
                    StartTimer();
                }
            }
            else
            {
                labelLog.Text = "Файл не выбран!";
                StartTimer();
            }
            return false;
        }
        private void CreateCryptoProviderRandom()
        {
            switch(cbCipherAlg.SelectedIndex)
            {
                case 0: 
                    cryptoProvider = new DEScipher(
                        CurrentMode(), 
                        CurrentPaddingMode()
                        ); 
                    break;
                case 1: 
                    cryptoProvider = new AEScipher(
                        CurrentMode(), 
                        CurrentPaddingMode()
                        ); 
                    break;
            }
        }
        private void CreateCryptoProvider()
        {
            switch (cbCipherAlg.SelectedIndex)
            {
                case 0:
                    cryptoProvider = new DEScipher(
                        CurrentMode(),
                        CurrentPaddingMode(),
                        Encoding.UTF8.GetBytes(tbKey.Text),
                        Encoding.UTF8.GetBytes(tbIV.Text)
                        );
                    break;
                case 1:
                    cryptoProvider = new AEScipher(
                        CurrentMode(),
                        CurrentPaddingMode(),
                        Encoding.UTF8.GetBytes(tbKey.Text),
                        Encoding.UTF8.GetBytes(tbIV.Text)
                        );
                    break;
            }
        }
        private Bitmap BytesToImage(byte[] bytes, Size size)
        {
            Bitmap bmp = new Bitmap(
                size.Width,
                size.Height,
                PixelFormat.Format32bppRgb
                );
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly,
                bmp.PixelFormat
                );
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
            //Bitmap result = new Bitmap(size.Width, size.Height);

            //int index = 0;
            //for (int x = 0; x < size.Width; x++) 
            //    for (int y = 0; y < size.Height; y++, index += 3)
            //        result.SetPixel(x, y, Color.FromArgb(bytes[index + 0], bytes[index + 1], bytes[index + 2]));

            //return result;
        }
        private byte[] ImageToBytes(Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                bmp.PixelFormat
                );
            IntPtr ptr = bmpData.Scan0;
            int bytes = bmpData.Stride * bmpData.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            bmp.UnlockBits(bmpData);
            return rgbValues;
            //byte[] bytes = new byte[3 * bmp.Width * bmp.Height];
            //int index = 0;
            //for (int x = 0; x < bmp.Width; x++) 
            //    for (int y = 0; y < bmp.Height; y++, index += 3)
            //    {
            //        Color curPixel = bmp.GetPixel(x, y);
            //        bytes[index + 0] = curPixel.R;
            //        bytes[index + 1] = curPixel.G;
            //        bytes[index + 2] = curPixel.B;
            //    }

            //return bytes;
        }
        private void UpdateKeyIV()
        {
            tbKey.Text = Encoding.ASCII.GetString(
               cryptoProvider.Key,
               0,
               cryptoProvider.Key.Length
               );
            tbIV.Text = Encoding.ASCII.GetString(
                cryptoProvider.IV,
                0,
                cryptoProvider.IV.Length
                );
        }
        private string NewFileName(string file, int type)
        {
            //C:\path\filename.txt => C:\path\filenameENCRYPTED.txt
            string tmp = file;
            string file1 = tmp.Substring(0, tmp.LastIndexOf('.'));
            string format = tmp.Substring(file1.Length);
            switch(type)
            {
                case 0: return file1 + "ENC" + format;
                case 1: return file1 + "DEC" + format;
            }
            return file;
        }
        public MainForm()
        {
            InitializeComponent();
            file = string.Empty;
            fileType = FileType.none;
            fiveSecond = 0;
            cbCipherAlg.SelectedIndex = 0;
            cbConnectBlockAlg.SelectedIndex = 0;
            cbConnectBlockType.SelectedIndex = 0;
            bOpenFile.Select();
            //CreateCryptoProviderRandom();
            //UpdateKeyIV();
        }

        private void bKey_Click(object sender, EventArgs e)
        {
            CreateCryptoProviderRandom();
            UpdateKeyIV();
        }

        private void bIV_Click(object sender, EventArgs e)
        {
            CreateCryptoProvider();
            UpdateKeyIV();
            labelLog.Text = "Инициализировано";
            StartTimer();
        }

        private void cbConnectBlockAlg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CreateCryptoProviderRandom();
            //UpdateKeyIV();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void bEncrypted_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                CreateCryptoProvider();
                if (fileType == FileType.txt)
                {
                    //string text = File.ReadAllText(file, Encoding.ASCII);
                    //byte[] bytes = Encoding.UTF8.GetBytes(text);
                    //byte[] encText = cryptoProvider.Encrypt(bytes);
                    //string encT = Encoding.ASCII.GetString(encText);
                    //File.WriteAllText(NewFileName(file, 0), encT);
                    cryptoProvider.Encrypt(file, NewFileName(file, 0));
                }
                else
                {
                    byte[] bytes = ImageToBytes(imageIn as Bitmap);
                    byte[] encryptionBytes = cryptoProvider.Encrypt(bytes);
                    imageOut = BytesToImage(encryptionBytes, imageIn.Size);
                    imageOut.Save(NewFileName(file, 0));
                }
                labelLog.ForeColor = Color.Green;
                labelLog.Text = "Зашифровано!";
                StartTimer();
            }
        }
        private void bDecrypted_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                if (fileType == FileType.txt)
                {
                    //string text = File.ReadAllText(file, Encoding.ASCII);
                    //byte[] bytes = Encoding.UTF8.GetBytes(text);
                    //byte[] decText = cryptoProvider.Decrypt(bytes);
                    //string decT = Encoding.ASCII.GetString(decText);
                    //File.WriteAllText(NewFileName(file, 1), decT);
                    cryptoProvider.Decrypt(file, NewFileName(file, 1));
                }
                else
                {
                    byte[] bytes = ImageToBytes(imageIn as Bitmap);
                    byte[] decryptionBytes = cryptoProvider.Decrypt(bytes);
                    imageOut = BytesToImage(decryptionBytes, imageIn.Size);
                    imageOut.Save(NewFileName(file, 1));
                }
                labelLog.ForeColor = Color.Green;
                labelLog.Text = "Дешифровано!";
                StartTimer();
            }
        }
        private void timerLogClear_Tick(object sender, EventArgs e)
        {
            if (fiveSecond < 5)
                fiveSecond++;
            else
            {
                labelLog.Text = "";
                labelLog.ForeColor = Color.Black;
                timerLogClear.Enabled = false;
            }
        }

        private void cbCipherAlg_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateCryptoProviderRandom();
            UpdateKeyIV();
        }

        private void bSaveKeyIV_Click(object sender, EventArgs e)
        {

        }

        private void bOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files (*.txt)|*.txt| Bitmap files (*.bmp)|*.bmp| Image files (*.jpg, *.jpeg)|*.jpg| PNG file (*.png)|*png";
            openFileDialog1.Title = "Выберите файл для шифрования";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            file = openFileDialog1.FileName;
            labelFile.Text = "Файл: " + file;
            if (openFileDialog1.FilterIndex == 1)
            {
                fileType = FileType.txt;
            }           
            else
            {
                fileType = FileType.img;
                imageIn = new Bitmap(file); 
            }
        }
    }
}
