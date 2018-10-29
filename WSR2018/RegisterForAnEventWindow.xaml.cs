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
    /// Логика взаимодействия для RegisterForAnEventWindow.xaml
    /// </summary>
    public partial class RegisterForAnEventWindow : Window
    {
        public RegisterForAnEventWindow()
        {
            InitializeComponent();
            FondCBox.ItemsSource = ServerController.CharityList();
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

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new RunnerMenuWindow());
        }
        private void CountMoney(object sender, RoutedEventArgs e)
        {
            MoneyTBlock.Text = "$" + CountMoneySearcher();
        }
        /// <summary>
        /// Метот расчитывающий требумое количество денег
        /// </summary>
        /// <returns>Количество денег</returns>
        int CountMoneySearcher()
        {
            int money = 0;
            if (VarA.IsChecked.Value)
            {
                money += 0;
            }
            else if (VarB.IsChecked.Value)
            {
                money += 20;
            }
            else if (VarC.IsChecked.Value)
            {
                money += 45;
            }
            if (FullMar.IsChecked.Value)
            {
                money += 145;
            }
            if (FloorMar.IsChecked.Value)
            {
                money += 75;
            }
            if (SmallDistance.IsChecked.Value)
            {
                money += 20;
            }
            return money;
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Регистрация"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void RegistrBtn_Click(object sender, RoutedEventArgs e)
        {
            string valitOption = null;
            if (VarA.IsChecked.Value)
            {
                valitOption = "A";
            }
            else if (VarB.IsChecked.Value)
            {
                valitOption = "B";
            }
            else if (VarC.IsChecked.Value)
            {
                valitOption = "C";
            }
            StringBuilder message = new StringBuilder();
            if(MoneyTBox.DataContext == null || double.Parse(MoneyTBox.Text) <=0)
            {
                message.Append("\"Сумма взноса\" должна быть действительным положительным числом\n");
            }
            if(!FullMar.IsChecked.Value && !FloorMar.IsChecked.Value && !SmallDistance.IsChecked.Value)
            {
                message.Append("По крайней мере 1 вид марафона должен быть выбран\n");
            }
            if (!string.IsNullOrWhiteSpace(message.ToString())) MessageBox.Show(message.ToString());
            else
            {
                if(!int.TryParse(MoneyTBox.Text, out int ST))
                {
                    ST = 0;
                }
                ServerController.RegistrationRunnerOnMarafon(valitOption, CountMoneySearcher(), (int)((DataRowView)FondCBox.SelectedItem).Row["CharityId"], ST);
                WindowsControler.GoTo(new RegistrationConfirmatinWindow());
            }
        }
    }
}
