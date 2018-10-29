using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для EditRunnerProfileWindow.xaml
    /// </summary>
    public partial class EditRunnerProfileWindow : Window
    {
        public EditRunnerProfileWindow()
        {
            InitializeComponent();
            CoutryCBox.ItemsSource = ServerController.CountriesList();
            GenderCBox.ItemsSource = ServerController.GenderList();
            EmailTBox.Text = ServerController.Email;
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.Back();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Сохранить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            message.Append(Utilities.RegistrateValidate(NameTBox.Text, RegUserType.Name) + (NameTBox.DataContext == null ? "Все поля обязательны для заполнения\n" : ""));
            message.Append(Utilities.RegistrateValidate(SurnameTBox.Text, RegUserType.Surname) + (SurnameTBox.DataContext == null ? "Все поля обязательны для заполнения\n" : ""));
            message.Append(Utilities.RegistrateValidate(PhotoPathTBox.Text, RegUserType.Photo) + (PhotoPathTBox.DataContext == null ? "Все поля обязательны для заполнения\n" : ""));
            if (!(string.IsNullOrWhiteSpace(PasswordTBox.Text) && string.IsNullOrWhiteSpace(ReturnPasswordTBox.Text)) && PasswordTBox.DataContext != null && ReturnPasswordTBox.DataContext != null)
            {
                string passEror = Utilities.RegistrateValidate(PasswordTBox.Text, RegUserType.Password);
                if (string.IsNullOrWhiteSpace(passEror))
                {
                    if (PasswordTBox.Text != ReturnPasswordTBox.Text)
                    {
                        message.Append("Значение \"повторите пароль\" должно соответствовать с значением \"Пароль\"\n");
                    }
                }
                else
                {
                    message.Append(passEror);
                }
            }
            if (new DateTime((DateTime.Now - DateOfBirthTBox.DisplayDate).Ticks).Year < 10)
            {
                message.Append("\"Дата рождения\" должна быть правильной датой и вам должно быть не менее 10 лет\n");
            }
            if (!string.IsNullOrWhiteSpace(message.ToString())) MessageBox.Show(message.ToString());
            else
            {
                ServerController.EditRunner(EmailTBox.Text, PasswordTBox.Text, NameTBox.Text, SurnameTBox.Text, ((DataRowView)GenderCBox.SelectedItem).Row["Gender"].ToString(), DateOfBirthTBox.SelectedDate.Value, ((DataRowView)CoutryCBox.SelectedItem).Row["CountryCode"].ToString(), PhotoPathTBox.Text);
                WindowsControler.Back();
            }
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
            if (string.IsNullOrWhiteSpace(t.Text) && t.DataContext == null) t.Text = t.DataContext.ToString();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки добавить фото
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void SelectPathPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog photoPath = new OpenFileDialog();
            photoPath.ShowDialog();
            if (string.IsNullOrWhiteSpace(Utilities.RegistrateValidate(photoPath.FileName, RegUserType.Photo)))
            {
                PhotoImage.Source = new BitmapImage(new Uri(photoPath.FileName));
                PhotoPathTBox.Text = photoPath.FileName;
                PhotoPathTBox.DataContext = photoPath.FileName;
            }
        }

    }
}
