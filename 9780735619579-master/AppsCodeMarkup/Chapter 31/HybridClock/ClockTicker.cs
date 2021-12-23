//--------------------------------------------
// ClockTicker.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Petzold.HybridClock
{
    public class ClockTicker : INotifyPropertyChanged
    {
        string strFormat = "F";

        // Event required for interface.
        public event PropertyChangedEventHandler PropertyChanged;

        // Public property.
        public string DateTime
        {
            get { return System.DateTime.Now.ToString(strFormat); }
        }

        public string Format
        {
            set { strFormat = value; }
            get { return strFormat; }
        }

        // Constructor.
        public ClockTicker()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += TimerOnTick;
            timer.Interval = TimeSpan.FromSeconds(0.10);
            timer.Start();
        }

        // Timer event handler triggers PropertyChanged event.
        void TimerOnTick(object sender, EventArgs args)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, 
                    new PropertyChangedEventArgs("DateTime"));
        }
    }
}
