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

            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("Password For", typeof(string)));
            dt.Columns.Add(new DataColumn("Password Encrypted", typeof(string)));

            for (int i = 0; i < 5; i++)
            {
                dr = dt.NewRow();

                dr[0] = i;
                dr[1] = "Item " + i.ToString();

                dt.Rows.Add(dr);
            }

            DataView dv = new DataView(dt);
            currentPasswords.ItemsSource = dv;
        }

        private void savePassword_Click(object sender, RoutedEventArgs e)
        {
            _passWordKeeper.KeepThis(password.Text, passwordFor.Text);
        }
    }
}
