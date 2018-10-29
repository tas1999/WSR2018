using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WSR2018
{
    class WindowsControler
    {
        static public MainWindow MainWindow = new MainWindow();
        static List<UIElement> OpenWindows = new List<UIElement>();
        static int step = 0;
        static public void Start(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            if(step >= OpenWindows.Count )
                OpenWindows.Add(MainWindow.MainContent);
        }
        static public void GoTo(Window finish)
        {
            MainWindow.main.Children.Remove(OpenWindows[step]);
            UIElement it = finish.Content as UIElement;
            step++;
            if (step >= OpenWindows.Count)
                OpenWindows.Add(it);
            else
                OpenWindows[step] = it;
            finish.Content = null;
            MainWindow.main.Children.Add(it);
        }
        static public void Back()
        {
            MainWindow.main.Children.Remove(OpenWindows[step]);
            step--;
            MainWindow.main.Children.Add(OpenWindows[step]);
        }
        /// <summary>
        /// Возвращение к первому окну
        /// </summary>
        static public void ReturnInStart()
        {
            MainWindow.main.Children.Remove(OpenWindows[step]);
            step = 0;
            MainWindow.main.Children.Add(OpenWindows[step]);
        }
    }
}
