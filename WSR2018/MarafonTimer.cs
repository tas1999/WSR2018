using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WSR2018
{
    public class MarafonTimer
    {
        DateTime MarafonTime = new DateTime(2018, 11, 24, 6, 0, 0);
        TextBlock TimerTBox { get; set; }
        public MarafonTimer(TextBlock timerTBox)
        {
            TimerTBox = timerTBox;
        }
        public void Start()
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (o, arg) =>
            {
                var time = MarafonTime - DateTime.Now;
                TimerTBox.Text = $"{time.Days} дней {time.Hours} часов и {time.Minutes} минут {time.Seconds} секунд  до старта марафона";
            };
            timer.Start();
        }

    }
}
