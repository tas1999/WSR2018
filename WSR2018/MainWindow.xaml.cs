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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WSR2018
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var timer =  new MarafonTimer(TimerTBox);
            timer.Start();

        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Я хочу стать спонсором бегуна"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void SponsorBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.Start(this);
            var nt = new SponsorARunnerWindow();
            WindowsControler.GoTo(nt);

        }
        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.Start(this);
            var nt = new FindOutMoreInformation();
            WindowsControler.GoTo(nt);
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.Start(this);
            var nt = new Login();
            WindowsControler.GoTo(nt);
        }
        /// <summary>
        /// Обработчик события нажатия кнопки "Я хочу стать бегуном"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void RunnerBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.Start(this);
            var nt = new RegisterAsARunnerWindow();
            WindowsControler.GoTo(nt);
        }
    }
    

}
