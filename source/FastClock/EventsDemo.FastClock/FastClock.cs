using System;
using System.Windows.Threading;

namespace EventsDemo.FastClock
{
    public class FastClock
    {
        public EventHandler<DateTime> OneMinuteIsOver;
        public DateTime CurrentTime { get; set; }

        private readonly DispatcherTimer _timer;
        private bool _isRunning;
        private double _factor = 60;
        public double Factor 
        {
            get => _factor;

            set
            {
                if (value < 1)
                {
                    _factor = 1;
                }
                else
                {
                    _factor = value / 3600 * 60000;
                }
                _timer.Interval = TimeSpan.FromMilliseconds(_factor);
            }
        
        }

        private static FastClock _instance;
        public static FastClock Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FastClock();
                }

                return _instance;
            }
        }

        public bool IsRunning
        {
            get => _isRunning;

            set
            {
                if (!_isRunning && value)
                {
                    _timer.Interval = TimeSpan.FromMilliseconds(_factor);
                    _timer.Start();
                }
                else if (_isRunning && !value)
                {
                    _timer.Stop();
                }

                _isRunning = value;
            }
        }

        public FastClock()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += OnTimerTick;
            _timer.Interval += TimeSpan.FromMilliseconds(_factor);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            CurrentTime = CurrentTime.AddMinutes(1);
            OnOneMinuteIsOver(CurrentTime);
        }

        protected virtual void OnOneMinuteIsOver(DateTime time)
        {
            OneMinuteIsOver?.Invoke(this, time);
        }
    }
}
