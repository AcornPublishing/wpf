//---------------------------------------------
// ClockTicker1.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using System;
using System.Windows;
using System.Windows.Threading;

namespace Petzold.DigitalClock
{
    public class ClockTicker1 : DependencyObject
    {
        // Define DependencyProperty.
        public static DependencyProperty DateTimeProperty = 
                DependencyProperty.Register("DateTime", typeof(DateTime), 
                                            typeof(ClockTicker1));
        
        // Expose DependencyProperty as CLR property.
        public DateTime DateTime
        {
            set { SetValue(DateTimeProperty, value); }
            get { return (DateTime) GetValue(DateTimeProperty); }
        }

        // Constructor sets timer.
        public ClockTicker1()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerOnTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        // Timer event handler sets DateTime property.
        void TimerOnTick(object sender, EventArgs args)
        {
            DateTime = DateTime.Now;
        }
    }
}
