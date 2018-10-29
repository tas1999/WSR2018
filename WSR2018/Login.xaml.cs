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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void GotAllFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = ((TextBox)sender);
            if (t.DataContext == null)
            {
                t.DataContext = t.Text;
                t.Text = "";
            }
        }
        private void LostAllFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = ((TextBox)sender);
            if (string.IsNullOrWhiteSpace(t.Text) && t.DataContext == null)
                t.Text = t.DataContext.ToString();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Login"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTBox.Text) || string.IsNullOrWhiteSpace(PasswordTBox.Text) || PasswordTBox.DataContext == null || PasswordTBox.DataContext == null)
            {
                MessageBox.Show("Все поля обязательны для заполнения");
            }
            else
            {
                string rol = ServerController.Connect(EmailTBox.Text, PasswordTBox.Text);
                if (rol == null)
                {
                    MessageBox.Show("Пользоатель не найден");
                }
                else if(rol == "R")
                {
                    WindowsControler.GoTo(new RunnerMenuWindow());
                }
                else if (rol == "A")
                {
                    WindowsControler.GoTo(new AdministratorMenuWindow());
                }
                else if (rol == "C")
                {
                    WindowsControler.GoTo(new CoordinatorMenuWindow());
                }
            }
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Cancel"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.Back();
        }
    }
}
