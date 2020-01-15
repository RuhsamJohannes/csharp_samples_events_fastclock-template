using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace EventsDemo.FastClockWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            FastClock.FastClock clock = FastClock.FastClock.Instance;
            clock.CurrentTime = DateTime.Now;

            DatePickerDate.SelectedDate = DateTime.Today;
            TextBlockDate.Text = DateTime.Now.ToShortDateString();
            TextBlockTime.Text = clock.CurrentTime.ToShortTimeString();
            TextBoxTime.Text = DateTime.Now.ToShortTimeString();

            clock.OneMinuteIsOver += FastClockOneMinuteIsOver;
        }

        private void ButtonSetTime_Click(object sender, RoutedEventArgs e)
        {
            SetFastClockStartDateAndTime();
        }

        private void SetFastClockStartDateAndTime()
        {
            TextBlockDate.Text = DatePickerDate.Text;
            bool isOK = Regex.IsMatch(TextBoxTime.Text, @"[0-2][0-9]\:[0-5][0-9]");


            if (isOK && TextBoxTime.Text[0] - '0' == 2 && TextBoxTime.Text[1] - '0' < 4)
            {
                TextBlockTime.Text = TextBoxTime.Text;
                FastClock.FastClock clock = FastClock.FastClock.Instance;
                clock.CurrentTime = Convert.ToDateTime(TextBoxTime.Text);
            }
            else if (isOK && TextBoxTime.Text[0] - '0' != 2)
            {
                TextBlockTime.Text = TextBoxTime.Text;
                FastClock.FastClock clock = FastClock.FastClock.Instance;
                clock.CurrentTime = Convert.ToDateTime(TextBoxTime.Text);
            }
            else
            {
                TextBlockTime.Text = "insert correct time";
            }
        }

        private void FastClockOneMinuteIsOver(object sender, DateTime fastClockTime)
        {
            if (TextBlockTime.Text == "00:00")
            {
                TextBlockDate.Text = fastClockTime.ToShortDateString();
            }
            TextBlockTime.Text = fastClockTime.ToShortTimeString();
        }

        private void CheckBoxClockRuns_Click(object sender, RoutedEventArgs e)
        {
            FastClock.FastClock clock = FastClock.FastClock.Instance;
            clock.Factor = SliderFactor.Value;
            clock.IsRunning = CheckBoxClockRuns.IsChecked == true;
        }

        private void ButtonCreateView_Click(object sender, RoutedEventArgs e)
        {
            DigitalClock digitalClock = new DigitalClock();
            digitalClock.Owner = this;
            digitalClock.Show();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FastClock.FastClock.Instance.Factor = SliderFactor.Value;
        }
    }
}
