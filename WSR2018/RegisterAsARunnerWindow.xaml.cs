using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WSR2018
{
    /// <summary>
    /// Логика взаимодействия для RegisterAsARunnerWindow.xaml
    /// </summary>
    public partial class RegisterAsARunnerWindow : Window
    {
        public RegisterAsARunnerWindow()
        {
            InitializeComponent();
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var nt = new Login();
            WindowsControler.GoTo(nt);
        }

        private void INewBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new RegisterAsARunnerFormWindow());
        }
    }
}
