using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WSR2018
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Метод вызывающий переход к начальному окну
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BackMain_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.ReturnInStart();
        }
        /// <summary>
        /// Метод вызывающий переход к меню бегуна
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BackToRunner_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new RunnerMenuWindow());
        }
        /// <summary>
        /// Метод вызывающий выход из акаунта и перехода к начальному окну
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            ServerController.Logout();
            WindowsControler.ReturnInStart();
        }
        /// <summary>
        /// Метод вызывающий перехода к окну подробной информации о марафоне
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new FindOutMoreInformation());
        }
        /// <summary>
        /// Метод вызывающий переход к меню бегуна
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BackToAdmin_Click(object sender, RoutedEventArgs e)
        {
            WindowsControler.GoTo(new AdministratorMenuWindow());
        }
    }
}
