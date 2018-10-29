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
            ServerController.GetInventar(InventStatistic);
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            ReportPopup.IsOpen = true;
            ReportTable.Columns.Add(new TableColumn());
            ReportTable.Columns.Add(new TableColumn());
            ReportTable.Columns.Add(new TableColumn());
            ReportTable.Columns.Add(new TableColumn());
            TableRowGroup tableRowGroup = new TableRowGroup();
            for (int y =0; y < 5; y++)
            {
                TableRow tableRow = new TableRow();
                for (int x = 0; x < 5; x++)
                {
                    tableRow.Cells.Add(new TableCell(new Paragraph(new Run(y + x.ToString()))));
                }
                tableRowGroup.Rows.Add(tableRow);
            }
            ReportTable.RowGroups.Add(tableRowGroup);
        }
    }
}
