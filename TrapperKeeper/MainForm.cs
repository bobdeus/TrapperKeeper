using System;
using System.Windows.Forms;
using TrapperKeeper.Extensions;
using TrapperKeeper.Services;

namespace TrapperKeeper
{
    public partial class MainForm : Form
    {
        private readonly PassWordKeeper _passWordKeeper = new PassWordKeeper();
        const int NameColumnIndex = 0;
        const int PasswordColumnIndex = 1;
        public MainForm()
        {
            InitializeComponent();
            PreparePasswordDataView();
            UpdateCurrentPasswordData();
        }

        private void PreparePasswordDataView()
        {
            passwordDataView.RowHeadersVisible = false;
            
            // TODO: This needs to be much more dynamic and less hard coded..
            // Should use reflection to get the column names?
            var passwordForColumn = new DataGridViewColumn {Name = "Password For", Width = 400};
            var passwordForTextBoxCell = new DataGridViewTextBoxCell {ValueType = typeof(string)};
            passwordForColumn.CellTemplate = passwordForTextBoxCell;

            var passwordColumn = new DataGridViewColumn {Name = "Password", Width = 400};
            var passwordTextBoxCell = new DataGridViewTextBoxCell {ValueType = typeof(string)};
            passwordColumn.CellTemplate = passwordTextBoxCell;

            var showPasswordColumn = new DataGridViewButtonColumn {Name = "Show Password", Text = "Show", Width = 110};
            var showPasswordButtonCell = new DataGridViewButtonCell
                {ValueType = typeof(Button), UseColumnTextForButtonValue = true};
            showPasswordColumn.CellTemplate = showPasswordButtonCell;
            
            var copyPasswordColumn = new DataGridViewButtonColumn {Name = "Copy Password", Text = "Copy To Clipboard", Width = 190};
            var copyPasswordButtonCell = new DataGridViewButtonCell
                {ValueType = typeof(Button), UseColumnTextForButtonValue = true};
            copyPasswordColumn.CellTemplate = copyPasswordButtonCell;

            passwordDataView.Columns.Add(passwordForColumn);
            passwordDataView.Columns.Add(passwordColumn);
            passwordDataView.Columns.Add(showPasswordColumn);
            passwordDataView.Columns.Add(copyPasswordColumn);
            
            // Adding this event to the entire DataGridView because I am not sure how to get the click location
            // in the DataGridView to the correct password button.
            passwordDataView.CellClick += showButton_Click;
        }

        private void showButton_Click(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridView = (DataGridView) sender;

            // I am not 100% sure how to make the form work with a button click event. Using an event on the entire
            // DataGridView was the only way I could find to do this since I am adding the button inside the DataGridView.
            // This might be a mistake. I could possibly move the button outside the DataGridView and make it better.
            //
            // Since I have done what I have done, we need to validate the button click to ensure
            // the correct type of button was clicked to honor this event.
            var isNotValidSelection = dataGridView.SelectedCells.Count > 1 || dataGridView.SelectedCells.IsNotValidButton();
            if (e.ColumnIndex < 0 || isNotValidSelection) return;

            var rowIndex = e.RowIndex;
            var passwordName = dataGridView.Rows[rowIndex].Cells[NameColumnIndex].Value.ToString();
            var passwordValue = _passWordKeeper.GetPassword(passwordName);

            
            if (dataGridView.SelectedCells.IsShowButton())
            {
                UpdateCurrentPasswordData();
                dataGridView.Rows[rowIndex].Cells[PasswordColumnIndex].Value = passwordValue;
            }
            else
            {
                UpdateCurrentPasswordData();
                Clipboard.SetText(passwordValue);
            }
        }

        private void UpdateCurrentPasswordData()
        {
            passwordDataView.Rows.Clear();
            passwordDataView.ClearSelection();
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