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
    /// Логика взаимодействия для InteractiveMapWindow.xaml
    /// </summary>
    public partial class InteractiveMapWindow : Window
    {
        public InteractiveMapWindow()
        {
            InitializeComponent();

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            CheckpointInfo.Visibility = Visibility.Hidden;
        }

        private void StartPoint_Click(object sender, RoutedEventArgs e)
        {
            CheckpointInfo.Visibility = Visibility.Visible;
            ServerController.StartpointStatus(((Button)sender).DataContext.ToString(), ServisProvaiderSPanel);
        }

        private void Checkpoint_Click(object sender, RoutedEventArgs e)
        {
            CheckpointInfo.Visibility = Visibility.Visible;
            ServerController.CheckpointStatus(((Button)sender).DataContext.ToString(), ServisProvaiderSPanel);
            
        }
    }
}
