using System;
using System.Windows.Threading;

namespace EventsDemo.FastClock
{
    public class FastClock
    {
        public EventHandler<DateTime> OneMinuteIsOver;
        
        private readonly DispatcherTimer _timer;
        private bool _isRunning;
        public DateTime CurrentTime { get; set; }

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
            _timer.Interval += TimeSpan.FromMilliseconds(1000);
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
