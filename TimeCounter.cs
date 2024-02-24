using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroMaker
{
    public class TimeCounter : INotifyPropertyChanged
    {
        private Stopwatch stopwatch = new Stopwatch();
        private string elapsedTime = "00:00";
        private bool isCounting;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ElapsedTime
        {
            get { return elapsedTime; }
            set
            {
                elapsedTime = value;
                OnPropertyChanged("ElapsedTime");
            }
        }

        internal void StartDispatcherTimer()
        {
            stopwatch.Start();
            isCounting = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                ElapsedTime = UpdateElapsedTime();
                return isCounting;
            });
        }

        private string UpdateElapsedTime()
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            return $"{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
