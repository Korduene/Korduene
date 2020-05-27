using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF.CustomControls
{
    /// <summary>
    /// Interaction logic for TextBox.xaml
    /// </summary>
    public partial class DateTimePicker : System.Windows.Controls.TextBox
    {
        public string DateTimeFormat { set; get; } = "yyyy-MM-dd HH:mm";
        public event EventHandler<SelectionChangedEventArgs> DateChanged;

        private DateTime p_selecteddate;

        public DateTime SelectedDate
        {
            set
            {
                p_selecteddate = value;
                //p_calendar.SelectedDate = value;
                this.Text = value.ToString(DateTimeFormat);
                p_time = value.ToString("HH:mm");
            }
            get
            {
                string[] t = p_time.Split(':');

                int h = 0;
                int m = 0;

                int.TryParse(t[0], out h);
                int.TryParse(t[1], out m);

                DateTime dt = p_selecteddate;

                return new DateTime(dt.Year, dt.Month, dt.Day, h, m, 0);
            }
        }
        private System.Windows.Controls.Primitives.Popup p_popup;
        private System.Windows.Controls.Calendar p_calendar;

        private string p_time = "00:00";

        public DateTimePicker()
        {
            InitializeComponent();
            p_calendar = new Calendar();

            p_popup = new System.Windows.Controls.Primitives.Popup() { Height = p_calendar.Height, Width = p_calendar.Width, StaysOpen = false, AllowsTransparency = true, Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse };
            p_popup.Child = p_calendar;
            p_calendar.Background = new SolidColorBrush(Colors.DarkGray);
            p_calendar.Foreground = null; //new SolidColorBrush(Colors.White);
                                          //p_calendar.BorderBrush = null;
                                          //p_calendar.BorderThickness = new Thickness(0);

            p_calendar.SelectedDatesChanged += P_calendar_SelectedDatesChanged;
            this.TextChanged += DateTimePicker_TextChanged;
            this.Loaded += DateTimePicker_Loaded;
            p_selecteddate = DateTime.Now;
            this.Text = DateTime.Now.ToString(DateTimeFormat);
        }

        private void DateTimePicker_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dt = new DateTime();
            DateTime.TryParse(this.Text, out dt);
            if (dt.Year > 1)
            {
                p_selecteddate = dt;
                p_time = p_selecteddate.ToString("HH:mm");
            }
        }

        private void DateTimePicker_Loaded(object sender, RoutedEventArgs e)
        {
            this.Text = p_selecteddate.ToString(DateTimeFormat);
        }

        private void P_calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            p_popup.IsOpen = false;

            string[] t = p_time.Split(':');

            int h = 0;
            int m = 0;

            int.TryParse(t[0], out h);
            int.TryParse(t[1], out m);

            DateTime dt = p_calendar.SelectedDate.Value;

            p_selecteddate = new DateTime(dt.Year, dt.Month, dt.Day, h, m, 0);
            this.Text = p_selecteddate.ToString(DateTimeFormat);
        }

        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime.TryParse(this.Text, out dt);
            p_time = dt.ToString("HH:mm");
            p_calendar.SelectedDate = dt;
            p_popup.IsOpen = true;
        }
    }
}
