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
    /// Логика взаимодействия для SponsorARunnerWindow.xaml
    /// </summary>
    public partial class SponsorARunnerWindow : Window
    {
        public SponsorARunnerWindow()
        {
            InitializeComponent();
            RunnerName.ItemsSource = ServerController.RunnerList();
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            double money = double.Parse(MoneyTBox.Text);
            money += 10;
            MoneyTBox.Text = money.ToString();
            MoneyTBlock.Text = "$" + money.ToString();
        }

        private void RemoveMoney_Click(object sender, RoutedEventArgs e)
        {
            double money = double.Parse(MoneyTBox.Text);
            if(money <= 10)
            {
                money = 0;
            }
            else money -= 10;

            MoneyTBox.Text = money.ToString();
            MoneyTBlock.Text = "$" + money.ToString();
            
            
        }
        /// <summary>
        /// Обработчик события измененя текста в поле "MoneyTBox"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void MoneyTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MoneyTBox.Text) ) MoneyTBox.Text = "0";
            MoneyTBlock.Text = "$" + MoneyTBox.Text;
        }
        private void MoneyTBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void GotAllFocus(object sender, RoutedEventArgs e)
        {
            TextBox t = ((TextBox)sender);
            if (t.DataContext == null) {

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
        /// Обработчик события нажатия кнопки "Заплатить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void PayBtn_Click(object sender, RoutedEventArgs e)
        {
            bool falid = true;
            StringBuilder message = new StringBuilder();
            TextBox[] textBoxs = new[]
            {
                NameTBox,
                CardOwnerTBox,
                CardNumberTBox,
                MonthTBox,
                YearTBox,
                CVCCode
            };
            for(int i =0; i < textBoxs.Length; i++)
            {
                message.Append(Utilities.CardValidate(textBoxs[i].Text, (CardType)i));
                if(textBoxs[i].DataContext == null)
                {
                    falid = false;
                    message.Append("Все поля обязательны для заполнения\n");
                }
            }
            if (!string.IsNullOrWhiteSpace(message.ToString())) falid = false;
            if (YearTBox.DataContext != null && MonthTBox.DataContext != null && falid)
            {
                if (int.Parse(YearTBox.Text) < DateTime.Now.Year)
                {
                    falid = false;
                    message.Append("Срок действия должен быть действительный\n");
                }
                else if (int.Parse(YearTBox.Text) == DateTime.Now.Year)
                {
                    if (int.Parse(MonthTBox.Text) < DateTime.Now.Month)
                    {
                        falid = false;
                        message.Append("Срок действия должен быть действительный\n");
                    }
                }
            }
            if (!falid) MessageBox.Show(message.ToString());
            else
            {
                SponsorshipConfirmation sponsorshipConfirmation = new SponsorshipConfirmation(MoneyTBlock.Text);
                WindowsControler.GoTo(sponsorshipConfirmation);
            }


        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.Back();
        }

        private void InfoFond_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = true;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = false;
        }

        private void RunnerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var runnerInfo = ((RunnerAndCountry)RunnerName.SelectedItem);
            FondName.Text = FondNamePopup.Text = runnerInfo.CharityName;
            try
            {
                FondLogo.Source = new BitmapImage(
                                  new Uri(@"G:\Разбор WorldScills\Data\ресурсы_для_заданий\WSR2016_TP09_общие_ресурсы\marathon-skills-2016-charity-data\"
                                                            + runnerInfo.CharityLogo));
            }
            catch
            {

            }
            FondDescription.Text = runnerInfo.CharityDescription;
        }
    }
}
