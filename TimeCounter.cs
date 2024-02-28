﻿using Microsoft.Maui.Dispatching;
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
        private bool isCounting;

        public event PropertyChangedEventHandler PropertyChanged;

        private string elapsedTime = "00:00";
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
        internal void PauseTimer()
        {
            stopwatch.Stop();
        }
        internal void RestartTimer()
        {
            stopwatch.Restart();
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
