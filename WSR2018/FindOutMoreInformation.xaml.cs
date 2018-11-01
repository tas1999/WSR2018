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
    /// Логика взаимодействия для FindOutMoreInformation.xaml
    /// </summary>
    public partial class FindOutMoreInformation : Window
    {
        public FindOutMoreInformation()
        {
            InitializeComponent();
        }

        private void FondInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new ListOfCharities());
        }

        private void HowLongBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new HowLongIsAMarafonWindow());
        }

        private void MarafonInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new AboutMarathonSkillsWindow());
        }
    }
}
