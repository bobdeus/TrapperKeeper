using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrapperKeeper.Services;

namespace TrapperKeeper
{
    public partial class MainWindow : Window
    {
        private readonly PassWordKeeper _passWordKeeper;
        public MainWindow()
        {
            InitializeComponent();
            _passWordKeeper = new PassWordKeeper();

            var dataTable = new DataTable();

            var siteColumn = new DataColumn("Password For", typeof(string));
            
            
            DataGridColumn dataGridColumn = new DataGridTextColumn();
            DataGridColumn dataGridColumn2 = new DataGridTemplateColumn();
            dataGridColumn.Header = "Password For";
            CurrentPasswords.Columns.Add(dataGridColumn);
            
            
            
            DataGridTemplateColumn buttonColumn = new DataGridTemplateColumn();
            buttonColumn.Header = "My Button";
            DataTemplate buttonTemplate = new DataTemplate();
            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonTemplate.VisualTree = buttonFactory;
            //add handler or you can add binding to command if you want to handle click
            buttonFactory.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler((sender, args) => { }));
            buttonFactory.SetBinding(Button.ContentProperty, new Binding("#"));
            buttonColumn.CellTemplate = buttonTemplate;
            CurrentPasswords.Columns.Add(buttonColumn);

            var row = new {sdf = "asdf", asdfae = new Button() };
            CurrentPasswords.Items.Add(row);
            
            
            
            
            dataTable.Columns.Add(siteColumn);
            dataTable.Columns.Add(new DataColumn("Password Encrypted", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Actions", typeof(Button)));

            foreach (var keptPassword in _passWordKeeper.GetMyPasswords())
            {
                var dataRow = dataTable.NewRow();

                dataRow[0] = keptPassword.PasswordFor;
                dataRow[1] = keptPassword.EncryptedValue;
                dataRow[2] = new Button();

                dataTable.Rows.Add(dataRow);
            }

            var dataView = new DataView(dataTable);
            // CurrentPasswords.ItemsSource = dataView;
        }

        private void savePassword_Click(object sender, RoutedEventArgs e)
        {
            _passWordKeeper.KeepThis(Password.Text, PasswordFor.Text);
            
            // CurrentPasswords.ItemsSource = _passWordKeeper.GetMyPasswords();
        }
    }
}
