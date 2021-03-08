﻿using System;
using System.Windows.Forms;
using TrapperKeeper.Services;

namespace TrapperKeeper
{
    public partial class MainForm : Form
    {
        private readonly PassWordKeeper _passWordKeeper = new PassWordKeeper();

        public MainForm()
        {
            InitializeComponent();
            PreparePasswordDataView();
            UpdateCurrentPasswordData();
        }

        private void PreparePasswordDataView()
        {
            // TODO: This needs to be much more dynamic and less hard coded..
            // Should use reflection to get the column names?
            var passwordForColumn = new DataGridViewColumn {Name = "Password For", Width = 400};
            var passwordForTextBoxCell = new DataGridViewTextBoxCell {ValueType = typeof(string)};
            passwordForColumn.CellTemplate = passwordForTextBoxCell;

            var passwordColumn = new DataGridViewColumn {Name = "Password", Width = 400};
            var passwordTextBoxCell = new DataGridViewTextBoxCell {ValueType = typeof(string)};
            passwordColumn.CellTemplate = passwordTextBoxCell;

            var showPasswordColumn = new DataGridViewButtonColumn {Name = "showPassword", Text = "Show", Width = 100};
            var showPasswordButtonCell = new DataGridViewButtonCell
                {ValueType = typeof(Button), UseColumnTextForButtonValue = true};
            showPasswordColumn.CellTemplate = showPasswordButtonCell;
            passwordDataView.CellClick += showButton_Click;

            passwordDataView.Columns.Add(passwordForColumn);
            passwordDataView.Columns.Add(passwordColumn);
            passwordDataView.Columns.Add(showPasswordColumn);
        }

        private void showButton_Click(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView) sender;
            var passwordValue = _passWordKeeper.GetPassword(dataGridView.Rows[0].Cells[0].Value.ToString());
            dataGridView.Rows[0].Cells[1].Value = passwordValue;
            MessageBox.Show(passwordValue);
        }

        private void UpdateCurrentPasswordData()
        {
            passwordDataView.Rows.Clear();
            var passwords = _passWordKeeper.GetMyPasswords();
            if (passwords.Count == 0) return;

            foreach (var keptPassword in passwords)
            {
                passwordDataView.Rows.Add(keptPassword.PasswordFor, "*******");
            }
        }

        private void addPasswordButton_Click(object sender, EventArgs e)
        {
            _passWordKeeper.KeepThis(passwordTextBox.Text, passwordForTextBox.Text);
            UpdateCurrentPasswordData();
        }
    }
}