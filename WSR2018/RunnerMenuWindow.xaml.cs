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
    /// Логика взаимодействия для RunnerMenuWindow.xaml
    /// </summary>
    public partial class RunnerMenuWindow : Window
    {
        public RunnerMenuWindow()
        {
            InitializeComponent();
        }

        private void Contact_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = true;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            InfoPopup.IsOpen = false;
        }

        private void RegistOnM_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new RegisterForAnEventWindow());
        }

        private void EditProfle_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new EditRunnerProfileWindow());
        }

        private void MyStatistic_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new MyRaceResultsWindow());
        }
    }
}
