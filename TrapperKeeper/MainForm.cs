using System;
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
            var passwordForColumn = new DataGridViewColumn {Name = "Password For"};
            var passwordForTextBoxCell = new DataGridViewTextBoxCell {ValueType = typeof(string)};
            passwordForColumn.CellTemplate = passwordForTextBoxCell;

            var passwordColumn = new DataGridViewColumn {Name = "Password"};
            var passwordTextBoxCell = new DataGridViewTextBoxCell {ValueType = typeof(string)};
            passwordColumn.CellTemplate = passwordTextBoxCell;

            var showPasswordColumn = new DataGridViewButtonColumn {Name = "showPassword", Text = "Show"};
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
            var passwords = _passWordKeeper.GetMyPasswords();
            passwordDataView.Rows.Add(passwords[0].PasswordFor, "*******");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO Need to add more passwords to the keeper
            throw new NotImplementedException();
        }
    }
}