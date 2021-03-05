using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrapperKeeper.Services;

namespace TrapperKeeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PassWordKeeper passWordKeeper = new PassWordKeeper();
            var passwords = passWordKeeper.GetMyPasswords();
            textBox1.Multiline = true;
            var passwordOutput = ""; 
            passwords.ForEach(password =>
            {
                passwordOutput += $"pass: {password.PasswordFor} encrypt: {password.EncryptedValue}\n";
            });
            textBox1.Text = passwordOutput;
        }
    }
}