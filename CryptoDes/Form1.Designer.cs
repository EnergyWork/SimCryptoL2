namespace CryptoDes
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelAlgType = new System.Windows.Forms.Label();
            this.labelConnectBlockAlg = new System.Windows.Forms.Label();
            this.labelConnectBlockType = new System.Windows.Forms.Label();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.tbIV = new System.Windows.Forms.TextBox();
            this.bKey = new System.Windows.Forms.Button();
            this.bIV = new System.Windows.Forms.Button();
            this.bEncrypted = new System.Windows.Forms.Button();
            this.bDecrypted = new System.Windows.Forms.Button();
            this.bOpenFile = new System.Windows.Forms.Button();
            this.cbCipherAlg = new System.Windows.Forms.ComboBox();
            this.cbConnectBlockAlg = new System.Windows.Forms.ComboBox();
            this.cbConnectBlockType = new System.Windows.Forms.ComboBox();
            this.labelFile = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelKey = new System.Windows.Forms.Label();
            this.labelIV = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.timerLogClear = new System.Windows.Forms.Timer(this.components);
            this.bSaveKeyIV = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lInput = new System.Windows.Forms.Label();
            this.bSaveFile = new System.Windows.Forms.Button();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAlgType
            // 
            this.labelAlgType.AutoSize = true;
            this.labelAlgType.Location = new System.Drawing.Point(12, 20);
            this.labelAlgType.Name = "labelAlgType";
            this.labelAlgType.Size = new System.Drawing.Size(123, 13);
            this.labelAlgType.TabIndex = 0;
            this.labelAlgType.Text = "Алгоритм шифрования";
            // 
            // labelConnectBlockAlg
            // 
            this.labelConnectBlockAlg.AutoSize = true;
            this.labelConnectBlockAlg.Location = new System.Drawing.Point(141, 20);
            this.labelConnectBlockAlg.Name = "labelConnectBlockAlg";
            this.labelConnectBlockAlg.Size = new System.Drawing.Size(152, 13);
            this.labelConnectBlockAlg.TabIndex = 3;
            this.labelConnectBlockAlg.Text = "Алгоритм сцепления блоков";
            // 
            // labelConnectBlockType
            // 
            this.labelConnectBlockType.AutoSize = true;
            this.labelConnectBlockType.Location = new System.Drawing.Point(299, 20);
            this.labelConnectBlockType.Name = "labelConnectBlockType";
            this.labelConnectBlockType.Size = new System.Drawing.Size(138, 13);
            this.labelConnectBlockType.TabIndex = 9;
            this.labelConnectBlockType.Text = "Режим сцепления блоков";
            // 
            // tbKey
            // 
            this.tbKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbKey.Location = new System.Drawing.Point(15, 198);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(202, 20);
            this.tbKey.TabIndex = 18;
            // 
            // tbIV
            // 
            this.tbIV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbIV.Location = new System.Drawing.Point(15, 246);
            this.tbIV.Name = "tbIV";
            this.tbIV.Size = new System.Drawing.Size(202, 20);
            this.tbIV.TabIndex = 19;
            // 
            // bKey
            // 
            this.bKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bKey.Location = new System.Drawing.Point(233, 172);
            this.bKey.Name = "bKey";
            this.bKey.Size = new System.Drawing.Size(204, 29);
            this.bKey.TabIndex = 20;
            this.bKey.Text = "Новый ключ";
            this.bKey.UseVisualStyleBackColor = true;
            this.bKey.Click += new System.EventHandler(this.bKey_Click);
            // 
            // bIV
            // 
            this.bIV.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bIV.Location = new System.Drawing.Point(233, 207);
            this.bIV.Name = "bIV";
            this.bIV.Size = new System.Drawing.Size(204, 29);
            this.bIV.TabIndex = 21;
            this.bIV.Text = "Инициализировать";
            this.bIV.UseVisualStyleBackColor = true;
            this.bIV.Click += new System.EventHandler(this.bIV_Click);
            // 
            // bEncrypted
            // 
            this.bEncrypted.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bEncrypted.Location = new System.Drawing.Point(233, 102);
            this.bEncrypted.Name = "bEncrypted";
            this.bEncrypted.Size = new System.Drawing.Size(204, 29);
            this.bEncrypted.TabIndex = 22;
            this.bEncrypted.Text = "Зашифровать";
            this.bEncrypted.UseVisualStyleBackColor = true;
            this.bEncrypted.Click += new System.EventHandler(this.bEncrypted_Click);
            // 
            // bDecrypted
            // 
            this.bDecrypted.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bDecrypted.Location = new System.Drawing.Point(233, 137);
            this.bDecrypted.Name = "bDecrypted";
            this.bDecrypted.Size = new System.Drawing.Size(204, 29);
            this.bDecrypted.TabIndex = 23;
            this.bDecrypted.Text = "Расшифровать";
            this.bDecrypted.UseVisualStyleBackColor = true;
            this.bDecrypted.Click += new System.EventHandler(this.bDecrypted_Click);
            // 
            // bOpenFile
            // 
            this.bOpenFile.FlatAppearance.BorderSize = 2;
            this.bOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.bOpenFile.Location = new System.Drawing.Point(15, 100);
            this.bOpenFile.Name = "bOpenFile";
            this.bOpenFile.Size = new System.Drawing.Size(202, 37);
            this.bOpenFile.TabIndex = 28;
            this.bOpenFile.Text = "Открыть";
            this.bOpenFile.UseVisualStyleBackColor = true;
            this.bOpenFile.Click += new System.EventHandler(this.bOpenFile_Click);
            // 
            // cbCipherAlg
            // 
            this.cbCipherAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCipherAlg.FormattingEnabled = true;
            this.cbCipherAlg.Items.AddRange(new object[] {
            "DES",
            "AES"});
            this.cbCipherAlg.Location = new System.Drawing.Point(15, 45);
            this.cbCipherAlg.Name = "cbCipherAlg";
            this.cbCipherAlg.Size = new System.Drawing.Size(120, 21);
            this.cbCipherAlg.TabIndex = 29;
            this.cbCipherAlg.SelectedIndexChanged += new System.EventHandler(this.cbCipherAlg_SelectedIndexChanged);
            // 
            // cbConnectBlockAlg
            // 
            this.cbConnectBlockAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConnectBlockAlg.FormattingEnabled = true;
            this.cbConnectBlockAlg.Items.AddRange(new object[] {
            "CBC",
            "CFB",
            "CTS",
            "ECB",
            "OFB"});
            this.cbConnectBlockAlg.Location = new System.Drawing.Point(144, 44);
            this.cbConnectBlockAlg.Name = "cbConnectBlockAlg";
            this.cbConnectBlockAlg.Size = new System.Drawing.Size(149, 21);
            this.cbConnectBlockAlg.TabIndex = 30;
            this.cbConnectBlockAlg.SelectedIndexChanged += new System.EventHandler(this.cbConnectBlockAlg_SelectedIndexChanged);
            // 
            // cbConnectBlockType
            // 
            this.cbConnectBlockType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConnectBlockType.FormattingEnabled = true;
            this.cbConnectBlockType.Items.AddRange(new object[] {
            "Zeros",
            "ANSIX923",
            "ISO10126",
            "PKCS7"});
            this.cbConnectBlockType.Location = new System.Drawing.Point(302, 44);
            this.cbConnectBlockType.Name = "cbConnectBlockType";
            this.cbConnectBlockType.Size = new System.Drawing.Size(135, 21);
            this.cbConnectBlockType.TabIndex = 31;
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(12, 78);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(95, 13);
            this.labelFile.TabIndex = 32;
            this.labelFile.Text = "Файл: не выбран";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(12, 182);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(33, 13);
            this.labelKey.TabIndex = 33;
            this.labelKey.Text = "Ключ";
            // 
            // labelIV
            // 
            this.labelIV.AutoSize = true;
            this.labelIV.Location = new System.Drawing.Point(12, 230);
            this.labelIV.Name = "labelIV";
            this.labelIV.Size = new System.Drawing.Size(124, 13);
            this.labelIV.TabIndex = 34;
            this.labelIV.Text = "Вектор инициализации";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(15, 278);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(0, 13);
            this.labelLog.TabIndex = 35;
            // 
            // timerLogClear
            // 
            this.timerLogClear.Interval = 1000;
            this.timerLogClear.Tick += new System.EventHandler(this.timerLogClear_Tick);
            // 
            // bSaveKeyIV
            // 
            this.bSaveKeyIV.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bSaveKeyIV.Location = new System.Drawing.Point(233, 242);
            this.bSaveKeyIV.Name = "bSaveKeyIV";
            this.bSaveKeyIV.Size = new System.Drawing.Size(204, 29);
            this.bSaveKeyIV.TabIndex = 36;
            this.bSaveKeyIV.Text = "Сохранить ключ и вектор иниц.";
            this.bSaveKeyIV.UseVisualStyleBackColor = true;
            this.bSaveKeyIV.Click += new System.EventHandler(this.bSaveKeyIV_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 294);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(422, 208);
            this.richTextBox1.TabIndex = 37;
            this.richTextBox1.Text = "";
            // 
            // lInput
            // 
            this.lInput.AutoSize = true;
            this.lInput.Location = new System.Drawing.Point(12, 277);
            this.lInput.Name = "lInput";
            this.lInput.Size = new System.Drawing.Size(87, 13);
            this.lInput.TabIndex = 38;
            this.lInput.Text = "Поле для ввода";
            // 
            // bSaveFile
            // 
            this.bSaveFile.FlatAppearance.BorderSize = 2;
            this.bSaveFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSaveFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.bSaveFile.Location = new System.Drawing.Point(15, 139);
            this.bSaveFile.Name = "bSaveFile";
            this.bSaveFile.Size = new System.Drawing.Size(202, 37);
            this.bSaveFile.TabIndex = 39;
            this.bSaveFile.Text = "Сохранить";
            this.bSaveFile.UseVisualStyleBackColor = true;
            this.bSaveFile.Click += new System.EventHandler(this.bSaveFile_Click);
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage.Location = new System.Drawing.Point(342, 467);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(100, 50);
            this.pbImage.TabIndex = 40;
            this.pbImage.TabStop = false;
            this.pbImage.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.button1.Location = new System.Drawing.Point(110, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 21);
            this.button1.TabIndex = 41;
            this.button1.Text = "Очистить поле ввода";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 529);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.bSaveFile);
            this.Controls.Add(this.lInput);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.bSaveKeyIV);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.labelIV);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.cbConnectBlockType);
            this.Controls.Add(this.cbConnectBlockAlg);
            this.Controls.Add(this.cbCipherAlg);
            this.Controls.Add(this.bOpenFile);
            this.Controls.Add(this.bDecrypted);
            this.Controls.Add(this.bEncrypted);
            this.Controls.Add(this.bIV);
            this.Controls.Add(this.bKey);
            this.Controls.Add(this.tbIV);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.labelConnectBlockType);
            this.Controls.Add(this.labelConnectBlockAlg);
            this.Controls.Add(this.labelAlgType);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "MainForm";
            this.Text = "DES AES шифрование";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAlgType;
        private System.Windows.Forms.Label labelConnectBlockAlg;
        private System.Windows.Forms.Label labelConnectBlockType;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.TextBox tbIV;
        private System.Windows.Forms.Button bKey;
        private System.Windows.Forms.Button bIV;
        private System.Windows.Forms.Button bEncrypted;
        private System.Windows.Forms.Button bDecrypted;
        private System.Windows.Forms.Button bOpenFile;
        private System.Windows.Forms.ComboBox cbCipherAlg;
        private System.Windows.Forms.ComboBox cbConnectBlockAlg;
        private System.Windows.Forms.ComboBox cbConnectBlockType;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelIV;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Timer timerLogClear;
        private System.Windows.Forms.Button bSaveKeyIV;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lInput;
        private System.Windows.Forms.Button bSaveFile;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button button1;
    }
}

