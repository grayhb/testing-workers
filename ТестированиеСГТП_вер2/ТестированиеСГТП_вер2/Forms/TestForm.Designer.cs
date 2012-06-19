namespace ТестированиеСГТП_вер2
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.CntAllQLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CntFinishQLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CntLeftQLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.FIO = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.закончитьТестToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ListButtons = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoxQ = new System.Windows.Forms.GroupBox();
            this.TextQ = new System.Windows.Forms.Label();
            this.AnswBox = new System.Windows.Forms.GroupBox();
            this.ListAnsw = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListButtons)).BeginInit();
            this.BoxQ.SuspendLayout();
            this.AnswBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListAnsw)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CntAllQLabel,
            this.CntFinishQLabel,
            this.CntLeftQLabel,
            this.toolStripStatusLabel1,
            this.FIO});
            this.statusStrip1.Location = new System.Drawing.Point(0, 688);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(974, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // CntAllQLabel
            // 
            this.CntAllQLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.CntAllQLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.CntAllQLabel.Name = "CntAllQLabel";
            this.CntAllQLabel.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.CntAllQLabel.Size = new System.Drawing.Size(117, 17);
            this.CntAllQLabel.Text = "Всего вопросов: 0";
            // 
            // CntFinishQLabel
            // 
            this.CntFinishQLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.CntFinishQLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.CntFinishQLabel.Name = "CntFinishQLabel";
            this.CntFinishQLabel.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.CntFinishQLabel.Size = new System.Drawing.Size(133, 17);
            this.CntFinishQLabel.Text = "Получено ответов: 0";
            // 
            // CntLeftQLabel
            // 
            this.CntLeftQLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.CntLeftQLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.CntLeftQLabel.Name = "CntLeftQLabel";
            this.CntLeftQLabel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.CntLeftQLabel.Size = new System.Drawing.Size(132, 17);
            this.CntLeftQLabel.Text = "Осталось вопросов: 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(552, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // FIO
            // 
            this.FIO.Name = "FIO";
            this.FIO.Size = new System.Drawing.Size(25, 17);
            this.FIO.Text = "FIO";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.закончитьТестToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(974, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // закончитьТестToolStripMenuItem
            // 
            this.закончитьТестToolStripMenuItem.Name = "закончитьТестToolStripMenuItem";
            this.закончитьТестToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.закончитьТестToolStripMenuItem.Text = "Закончить тест";
            this.закончитьТестToolStripMenuItem.Click += new System.EventHandler(this.закончитьТестToolStripMenuItem_Click);
            // 
            // ListButtons
            // 
            this.ListButtons.AllowUserToAddRows = false;
            this.ListButtons.AllowUserToDeleteRows = false;
            this.ListButtons.AllowUserToResizeRows = false;
            this.ListButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ListButtons.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ListButtons.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListButtons.ColumnHeadersHeight = 20;
            this.ListButtons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListButtons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.ListButtons.Location = new System.Drawing.Point(12, 32);
            this.ListButtons.MultiSelect = false;
            this.ListButtons.Name = "ListButtons";
            this.ListButtons.ReadOnly = true;
            this.ListButtons.RowHeadersVisible = false;
            this.ListButtons.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListButtons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListButtons.Size = new System.Drawing.Size(107, 645);
            this.ListButtons.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Вопросы";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BoxQ
            // 
            this.BoxQ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BoxQ.Controls.Add(this.TextQ);
            this.BoxQ.Location = new System.Drawing.Point(128, 27);
            this.BoxQ.Name = "BoxQ";
            this.BoxQ.Size = new System.Drawing.Size(840, 149);
            this.BoxQ.TabIndex = 5;
            this.BoxQ.TabStop = false;
            this.BoxQ.Text = "Содержание вопроса:";
            // 
            // TextQ
            // 
            this.TextQ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextQ.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextQ.ForeColor = System.Drawing.Color.Maroon;
            this.TextQ.Location = new System.Drawing.Point(10, 20);
            this.TextQ.Name = "TextQ";
            this.TextQ.Size = new System.Drawing.Size(818, 118);
            this.TextQ.TabIndex = 0;
            // 
            // AnswBox
            // 
            this.AnswBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswBox.Controls.Add(this.ListAnsw);
            this.AnswBox.Location = new System.Drawing.Point(128, 182);
            this.AnswBox.Name = "AnswBox";
            this.AnswBox.Size = new System.Drawing.Size(840, 446);
            this.AnswBox.TabIndex = 6;
            this.AnswBox.TabStop = false;
            this.AnswBox.Text = "Варианты ответов:";
            // 
            // ListAnsw
            // 
            this.ListAnsw.AllowUserToAddRows = false;
            this.ListAnsw.AllowUserToDeleteRows = false;
            this.ListAnsw.AllowUserToResizeRows = false;
            this.ListAnsw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListAnsw.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ListAnsw.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ListAnsw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListAnsw.ColumnHeadersHeight = 20;
            this.ListAnsw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListAnsw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column2,
            this.dataGridViewCheckBoxColumn1});
            this.ListAnsw.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ListAnsw.Location = new System.Drawing.Point(13, 20);
            this.ListAnsw.MultiSelect = false;
            this.ListAnsw.Name = "ListAnsw";
            this.ListAnsw.RowHeadersVisible = false;
            this.ListAnsw.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.ListAnsw.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.ListAnsw.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.ListAnsw.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListAnsw.RowTemplate.Height = 25;
            this.ListAnsw.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListAnsw.Size = new System.Drawing.Size(815, 414);
            this.ListAnsw.TabIndex = 3;
            // 
            // Column3
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column3.HeaderText = "";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 35;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "Содержание ответа";
            this.Column2.MinimumWidth = 150;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FalseValue = "0";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Правильный ответ";
            this.dataGridViewCheckBoxColumn1.MinimumWidth = 50;
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCheckBoxColumn1.TrueValue = "1";
            this.dataGridViewCheckBoxColumn1.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(128, 630);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(840, 49);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(644, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Следующий вопрос";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 710);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AnswBox);
            this.Controls.Add(this.BoxQ);
            this.Controls.Add(this.ListButtons);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Test_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListButtons)).EndInit();
            this.BoxQ.ResumeLayout(false);
            this.AnswBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListAnsw)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem закончитьТестToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView ListButtons;
        private System.Windows.Forms.ToolStripStatusLabel CntAllQLabel;
        private System.Windows.Forms.ToolStripStatusLabel CntFinishQLabel;
        private System.Windows.Forms.ToolStripStatusLabel CntLeftQLabel;
        private System.Windows.Forms.GroupBox BoxQ;
        private System.Windows.Forms.Label TextQ;
        private System.Windows.Forms.GroupBox AnswBox;
        private System.Windows.Forms.DataGridView ListAnsw;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel FIO;
    }
}