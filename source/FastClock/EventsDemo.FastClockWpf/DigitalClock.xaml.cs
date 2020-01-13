using System;

namespace EventsDemo.FastClockWpf
{
    /// <summary>
    /// Interaction logic for DigitalClock.xaml
    /// </summary>
    public partial class DigitalClock
    {
        public DigitalClock()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            FastClock.FastClock clock = FastClock.FastClock.Instance;
            FastClock.FastClock.Instance.OneMinuteIsOver += DigitalClock_OneMinuteIsOver;
            TextBlockClock.Text = clock.CurrentTime.ToShortTimeString();
        }

        private void DigitalClock_OneMinuteIsOver(object sender, DateTime e)
        {
            TextBlockClock.Text = e.ToShortTimeString();
        }
    }
}
