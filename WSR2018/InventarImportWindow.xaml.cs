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
    /// Логика взаимодействия для InventarImportWindow.xaml
    /// </summary>
    public partial class InventarImportWindow : Window
    {
        public InventarImportWindow()
        {
            InitializeComponent();
            ServerController.InventarImport(InventsImport);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ServerController.InventarExsport(InventsImport);
        }
    }
}
