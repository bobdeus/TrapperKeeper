namespace TrapperKeeper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            this.button1 = new System.Windows.Forms.Button();
            this.passwordDataView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize) (this.passwordDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // passwordDataView
            // 
            this.passwordDataView.AllowUserToAddRows = false;
            this.passwordDataView.AllowUserToDeleteRows = false;
            this.passwordDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.passwordDataView.Location = new System.Drawing.Point(444, 12);
            this.passwordDataView.Name = "passwordDataView";
            this.passwordDataView.RowTemplate.Height = 24;
            this.passwordDataView.Size = new System.Drawing.Size(487, 357);
            this.passwordDataView.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 538);
            this.Controls.Add(this.passwordDataView);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "TrapperKeeper";
            ((System.ComponentModel.ISupportInitialize) (this.passwordDataView)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView passwordDataView;

        private System.Windows.Forms.Button button1;

        #endregion
    }
}