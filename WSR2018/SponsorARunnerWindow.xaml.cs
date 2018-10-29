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
            money -= 10;
            MoneyTBox.Text = money.ToString();
            MoneyTBlock.Text = "$" + money.ToString();
        }

        private void MoneyTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MoneyTBox.Text)) MoneyTBox.Text = "0";
           MoneyTBlock.Text = "$" + MoneyTBox.Text;
        }

        
        private void MoneyTBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
