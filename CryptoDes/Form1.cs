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
        string saveFileName;

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
            if (!string.IsNullOrEmpty(file))
            {
                if (!string.IsNullOrEmpty(tbKey.Text))
                {
                    if (!string.IsNullOrEmpty(tbIV.Text))
                    {
                        return true;
                    }
                    else
                    {
                        labelLog.ForeColor = Color.Red;
                        labelLog.Text = "Вектор инициализации не инициализирован!";
                        StartTimer();
                    }
                }
                else
                {
                    labelLog.ForeColor = Color.Red;
                    labelLog.Text = "Ключ не инициализирован!";
                    StartTimer();
                }
            }
            else if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                return true;
            }
            else
            {
                labelLog.ForeColor = Color.Red;
                labelLog.Text = "Нет данных для шифрованя/дешифрования!";
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
                        Convert.FromBase64String(tbKey.Text), //Encoding.Unicode.GetBytes(tbKey.Text),
                        Convert.FromBase64String(tbIV.Text)
                        );
                    break;
                case 1:
                    cryptoProvider = new AEScipher(
                        CurrentMode(),
                        CurrentPaddingMode(),
                        Convert.FromBase64String(tbKey.Text),
                        Convert.FromBase64String(tbIV.Text)
                        );
                    break;
            }
        }
        private Bitmap BytesToImage(byte[] bytes, Size size)
        {
            Bitmap bmp = new Bitmap(
                size.Width,
                size.Height,
                PixelFormat.Format32bppArgb
                );
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly,
                bmp.PixelFormat
                );
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
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
        }
        private void UpdateKeyIV()
        {
            tbKey.Text = Convert.ToBase64String(cryptoProvider.Key);//Encoding.Unicode.GetString(cryptoProvider.Key, 0, cryptoProvider.Key.Length);
            tbIV.Text = Convert.ToBase64String(cryptoProvider.IV);//Encoding.Unicode.GetString(cryptoProvider.IV, 0, cryptoProvider.IV.Length);

            //tbKey.Text = (cryptoProvider.Key).ToString("x2");
        }
        private string NewFileName(string file, int type)
        {
            //C:\path\filename.txt => C:\path\filenameENC.txt
            string tmp, file1, format;
            if (!string.IsNullOrEmpty(file))
            {
                tmp = file;
                file1 = tmp.Substring(0, tmp.LastIndexOf('.'));
                format = tmp.Substring(file1.Length);
            }
            else
            {
                file1 = ".\\File";
                format = fileType == FileType.txt ? ".txt" : ".png";
            }
            switch(type)
            {
                case 0: return file1 + "Enc" + format;
                case 1: return file1 + "Dec" + format;
            }
            return file;
        }
        public MainForm() // 470x349
        {
            InitializeComponent();
            file = string.Empty;
            saveFileName = string.Empty;
            fileType = FileType.txt;
            fiveSecond = 0;
            cbCipherAlg.SelectedIndex = 0;
            cbConnectBlockAlg.SelectedIndex = 0;
            cbConnectBlockType.SelectedIndex = 0;
            tbKey.ScrollBars = ScrollBars.Horizontal;
            tbIV.ScrollBars = ScrollBars.Horizontal;
            bOpenFile.Select();
            labelLog.Location = new Point(labelLog.Location.X, 509);
            pbImage.SizeMode = PictureBoxSizeMode.Zoom;
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
            labelLog.Text = "Инициализировано!";
            StartTimer();
        }
        private void cbConnectBlockAlg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void bEncrypted_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                CreateCryptoProvider();
                 //= NewFileName(file, 0);
                if (fileType == FileType.txt)
                {
                    string text = richTextBox1.Text;
                    byte[] bytes = Encoding.Unicode.GetBytes(text); //File.ReadAllBytes(file);
                    byte[] encText = cryptoProvider.Encrypt(bytes);
                    string encT = Convert.ToBase64String(encText); //Encoding.Unicode.GetString(encText);
                    richTextBox1.Text = encT;
                    //File.WriteAllText(saveFileName, encT, Encoding.Unicode);
                    //cryptoProvider.Encrypt(file, NewFileName(file, 0));
                }
                else
                {
                    byte[] bytes = ImageToBytes(imageIn as Bitmap);
                    byte[] encryptionBytes = cryptoProvider.Encrypt(bytes);
                    imageOut = BytesToImage(encryptionBytes, imageIn.Size);
                    pbImage.Image = imageOut;
                    //imageOut.Save(saveFileName);
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
                string saveFileName = NewFileName(file, 1);
                if (fileType == FileType.txt)
                {
                    //string text = File.ReadAllText(file);
                    byte[] bytes = Convert.FromBase64String(richTextBox1.Text); //File.ReadAllBytes(file);
                    byte[] decText = cryptoProvider.Decrypt(bytes);
                    string decT = Encoding.Unicode.GetString(decText);
                    richTextBox1.Text = decT;
                    //File.WriteAllText(saveFileName, decT, Encoding.Unicode);
                    //cryptoProvider.Decrypt(file, NewFileName(file, 1));
                }
                else
                {
                    byte[] bytes = ImageToBytes(imageIn as Bitmap);
                    byte[] decryptionBytes = cryptoProvider.Decrypt(bytes);
                    imageOut = BytesToImage(decryptionBytes, imageIn.Size);
                    pbImage.Image = imageOut;
                    //imageOut.Save(saveFileName);
                }
                labelLog.ForeColor = Color.Green;
                labelLog.Text = "Дешифровано!";
                StartTimer();
            }
        }
        private void timerLogClear_Tick(object sender, EventArgs e)
        {
            if (fiveSecond < 8)
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
            saveFileDialog1.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog1.Title = "Сохранить ключ и вектор инициализации";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string saveKeyFile = saveFileDialog1.FileName;
            File.WriteAllText(saveKeyFile, "Key: " + tbKey.Text + "\nIV: " + tbIV.Text);
            labelLog.Text = "Сохранено в \"" + saveKeyFile + "\"";
            StartTimer();
        }
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void bSaveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter =
            (
                fileType == FileType.txt
                ? "Text file (*.txt)|*.txt"
                : "Image file (*.png, *.jpg, *.bmp)|*.png; *.jpg; *.jpeg; *.bmp;"
            );
            saveFileDialog1.Title = "Выберите файл для сохранения";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string selectFile = saveFileDialog1.FileName;
            if (fileType == FileType.txt)
            {
                File.WriteAllText(selectFile, richTextBox1.Text);
            }
            else
            {
                pbImage.Image.Save(selectFile);
            }
            labelLog.ForeColor = Color.Green;
            labelLog.Text = "Сохранено в \"" + selectFile + "\"";
            StartTimer();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            pbImage.Visible = false;
            richTextBox1.Visible = true;
            richTextBox1.Text = "";
            labelFile.Text = "Файл: не выбран";
            file = string.Empty;
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
                richTextBox1.Text = File.ReadAllText(file);
                richTextBox1.Visible = true;
                pbImage.Visible = false;
            }           
            else
            {
                fileType = FileType.img;
                imageIn = new Bitmap(file);
                richTextBox1.Visible = false;
                pbImage.Visible = true;
                pbImage.Location = richTextBox1.Location;
                pbImage.Size = richTextBox1.Size;
                pbImage.Image = imageIn;
            }
        }
    }
}
