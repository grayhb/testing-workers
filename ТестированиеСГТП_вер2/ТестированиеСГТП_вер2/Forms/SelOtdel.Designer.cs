namespace ТестированиеСГТП_вер2
{
    partial class SelOtdel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelOtdel));
            this.ListOtdels = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListOtdels)).BeginInit();
            this.SuspendLayout();
            // 
            // ListOtdels
            // 
            this.ListOtdels.AllowUserToAddRows = false;
            this.ListOtdels.AllowUserToDeleteRows = false;
            this.ListOtdels.AllowUserToResizeRows = false;
            this.ListOtdels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListOtdels.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ListOtdels.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ListOtdels.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListOtdels.ColumnHeadersHeight = 20;
            this.ListOtdels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListOtdels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.ListOtdels.Location = new System.Drawing.Point(12, 12);
            this.ListOtdels.MultiSelect = false;
            this.ListOtdels.Name = "ListOtdels";
            this.ListOtdels.ReadOnly = true;
            this.ListOtdels.RowHeadersVisible = false;
            this.ListOtdels.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListOtdels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListOtdels.Size = new System.Drawing.Size(403, 554);
            this.ListOtdels.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Наименование отдела";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SelOtdel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 578);
            this.Controls.Add(this.ListOtdels);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "SelOtdel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор отдела";
            ((System.ComponentModel.ISupportInitialize)(this.ListOtdels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ListOtdels;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}