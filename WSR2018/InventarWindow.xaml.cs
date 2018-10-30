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
    /// Логика взаимодействия для InventarWindow.xaml
    /// </summary>
    public partial class InventarWindow : Window
    {
        public InventarWindow()
        {
            InitializeComponent();
            RunnerRegCountTBlock.Text = ServerController.GetInventar(InventStatistic, ReportTable).ToString();
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportPopup.IsOpen = true;
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ReportPopup.IsOpen = false;
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            ReportPopup.IsOpen = false;
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(((IDocumentPaginatorSource)PrintDocum.Document).DocumentPaginator, "Печать отчёта");
            }
            ReportPopup.IsOpen = true;
                
        }

        private void InventarImportBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new InventarImportWindow());
        }
    }
}
