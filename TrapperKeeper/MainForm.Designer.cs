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
            this.addPasswordButton = new System.Windows.Forms.Button();
            this.passwordDataView = new System.Windows.Forms.DataGridView();
            this.passwordForTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.passwordDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // addPasswordButton
            // 
            this.addPasswordButton.Location = new System.Drawing.Point(139, 157);
            this.addPasswordButton.Name = "addPasswordButton";
            this.addPasswordButton.Size = new System.Drawing.Size(118, 34);
            this.addPasswordButton.TabIndex = 0;
            this.addPasswordButton.Text = "Add Password";
            this.addPasswordButton.UseVisualStyleBackColor = true;
            this.addPasswordButton.Click += new System.EventHandler(this.addPasswordButton_Click);
            // 
            // passwordDataView
            // 
            this.passwordDataView.AllowUserToAddRows = false;
            this.passwordDataView.AllowUserToDeleteRows = false;
            this.passwordDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.passwordDataView.Location = new System.Drawing.Point(366, 12);
            this.passwordDataView.Name = "passwordDataView";
            this.passwordDataView.RowTemplate.Height = 24;
            this.passwordDataView.Size = new System.Drawing.Size(1100, 600);
            this.passwordDataView.TabIndex = 2;
            // 
            // passwordForTextBox
            // 
            this.passwordForTextBox.Location = new System.Drawing.Point(44, 44);
            this.passwordForTextBox.Name = "passwordForTextBox";
            this.passwordForTextBox.Size = new System.Drawing.Size(293, 22);
            this.passwordForTextBox.TabIndex = 3;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(44, 94);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(293, 22);
            this.passwordTextBox.TabIndex = 4;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(44, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Password For";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(45, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1479, 622);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordForTextBox);
            this.Controls.Add(this.passwordDataView);
            this.Controls.Add(this.addPasswordButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrapperKeeper";
            ((System.ComponentModel.ISupportInitialize) (this.passwordDataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox passwordForTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;

        private System.Windows.Forms.DataGridView passwordDataView;

        private System.Windows.Forms.Button addPasswordButton;

        #endregion
    }
}