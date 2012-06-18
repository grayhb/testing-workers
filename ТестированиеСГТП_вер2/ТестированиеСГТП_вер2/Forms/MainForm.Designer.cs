namespace ТестированиеСГТП_вер2
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.операцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьТестToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьТестToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьВыбранныйТестToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьВыбранныйТестКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортироватьВыбранныйТестВWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьОтделToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.результатыТестовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.праваДоступаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ComboTests = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.NameOtdelLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.FIOWorker = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.операцииToolStripMenuItem,
            this.экспортToolStripMenuItem,
            this.выбратьОтделToolStripMenuItem,
            this.результатыТестовToolStripMenuItem,
            this.праваДоступаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // операцииToolStripMenuItem
            // 
            this.операцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьТестToolStripMenuItem,
            this.редактироватьТестToolStripMenuItem,
            this.удалитьВыбранныйТестToolStripMenuItem,
            this.toolStripSeparator1,
            this.сохранитьВыбранныйТестКакToolStripMenuItem,
            this.toolStripSeparator2,
            this.выходToolStripMenuItem});
            this.операцииToolStripMenuItem.Name = "операцииToolStripMenuItem";
            this.операцииToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.операцииToolStripMenuItem.Text = "Операции";
            // 
            // добавитьТестToolStripMenuItem
            // 
            this.добавитьТестToolStripMenuItem.Name = "добавитьТестToolStripMenuItem";
            this.добавитьТестToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.добавитьТестToolStripMenuItem.Text = "Добавить новый тест";
            this.добавитьТестToolStripMenuItem.Click += new System.EventHandler(this.добавитьТестToolStripMenuItem_Click);
            // 
            // редактироватьТестToolStripMenuItem
            // 
            this.редактироватьТестToolStripMenuItem.Name = "редактироватьТестToolStripMenuItem";
            this.редактироватьТестToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.редактироватьТестToolStripMenuItem.Text = "Редактировать выбранный тест";
            this.редактироватьТестToolStripMenuItem.Click += new System.EventHandler(this.редактироватьТестToolStripMenuItem_Click);
            // 
            // удалитьВыбранныйТестToolStripMenuItem
            // 
            this.удалитьВыбранныйТестToolStripMenuItem.Name = "удалитьВыбранныйТестToolStripMenuItem";
            this.удалитьВыбранныйТестToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.удалитьВыбранныйТестToolStripMenuItem.Text = "Удалить выбранный тест";
            this.удалитьВыбранныйТестToolStripMenuItem.Click += new System.EventHandler(this.удалитьВыбранныйТестToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(260, 6);
            // 
            // сохранитьВыбранныйТестКакToolStripMenuItem
            // 
            this.сохранитьВыбранныйТестКакToolStripMenuItem.Name = "сохранитьВыбранныйТестКакToolStripMenuItem";
            this.сохранитьВыбранныйТестКакToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.сохранитьВыбранныйТестКакToolStripMenuItem.Text = "Сохранить выбранный тест как ...";
            this.сохранитьВыбранныйТестКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьВыбранныйТестКакToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(260, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // экспортToolStripMenuItem
            // 
            this.экспортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортироватьВыбранныйТестВWordToolStripMenuItem});
            this.экспортToolStripMenuItem.Name = "экспортToolStripMenuItem";
            this.экспортToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.экспортToolStripMenuItem.Text = "Экспорт";
            // 
            // экспортироватьВыбранныйТестВWordToolStripMenuItem
            // 
            this.экспортироватьВыбранныйТестВWordToolStripMenuItem.Name = "экспортироватьВыбранныйТестВWordToolStripMenuItem";
            this.экспортироватьВыбранныйТестВWordToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.экспортироватьВыбранныйТестВWordToolStripMenuItem.Text = "Экспортировать выбранный тест в Word";
            this.экспортироватьВыбранныйТестВWordToolStripMenuItem.Click += new System.EventHandler(this.экспортироватьВыбранныйТестВWordToolStripMenuItem_Click);
            // 
            // выбратьОтделToolStripMenuItem
            // 
            this.выбратьОтделToolStripMenuItem.Name = "выбратьОтделToolStripMenuItem";
            this.выбратьОтделToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.выбратьОтделToolStripMenuItem.Text = "Выбрать отдел";
            this.выбратьОтделToolStripMenuItem.Click += new System.EventHandler(this.выбратьОтделToolStripMenuItem_Click);
            // 
            // результатыТестовToolStripMenuItem
            // 
            this.результатыТестовToolStripMenuItem.Name = "результатыТестовToolStripMenuItem";
            this.результатыТестовToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.результатыТестовToolStripMenuItem.Text = "Результаты тестов";
            this.результатыТестовToolStripMenuItem.Click += new System.EventHandler(this.результатыТестовToolStripMenuItem_Click);
            // 
            // праваДоступаToolStripMenuItem
            // 
            this.праваДоступаToolStripMenuItem.Name = "праваДоступаToolStripMenuItem";
            this.праваДоступаToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.праваДоступаToolStripMenuItem.Text = "Права доступа";
            this.праваДоступаToolStripMenuItem.Click += new System.EventHandler(this.праваДоступаToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ComboTests);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(700, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тест:";
            // 
            // ComboTests
            // 
            this.ComboTests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboTests.FormattingEnabled = true;
            this.ComboTests.Location = new System.Drawing.Point(6, 19);
            this.ComboTests.Name = "ComboTests";
            this.ComboTests.Size = new System.Drawing.Size(688, 21);
            this.ComboTests.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(95, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(530, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Начать тестирование";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NameOtdelLabel,
            this.toolStripStatusLabel1,
            this.FIOWorker});
            this.statusStrip1.Location = new System.Drawing.Point(0, 145);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(724, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // NameOtdelLabel
            // 
            this.NameOtdelLabel.Margin = new System.Windows.Forms.Padding(15, 3, 0, 2);
            this.NameOtdelLabel.Name = "NameOtdelLabel";
            this.NameOtdelLabel.Size = new System.Drawing.Size(95, 17);
            this.NameOtdelLabel.Text = "Название отдела";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(491, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // FIOWorker
            // 
            this.FIOWorker.Margin = new System.Windows.Forms.Padding(0, 3, 15, 2);
            this.FIOWorker.Name = "FIOWorker";
            this.FIOWorker.Size = new System.Drawing.Size(93, 17);
            this.FIOWorker.Text = "ФИО сотрудника";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 167);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Электронная база профессиональных тестов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem операцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьТестToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьТестToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ComboTests;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel NameOtdelLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel FIOWorker;
        private System.Windows.Forms.ToolStripMenuItem удалитьВыбранныйТестToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортироватьВыбранныйТестВWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьОтделToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem результатыТестовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьВыбранныйТестКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem праваДоступаToolStripMenuItem;
    }
}

