using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace Fithub1
{
    public partial class Summary : PhoneApplicationPage
    {
        int type = 0;
        /*
         * Running = 1
         * biking = 2
         * sit-up = 3
         * push-up = 4
         */
        List<sitUpData> sitUpData;
        List<runningData> runningData;
        List<pushUpData> pushUpData;
        List<bikingData> bikingData;

        sitUpData lastSitup;
        pushUpData lastPushup;
        runningData lastRunning;
        bikingData lastBiking;

        public Summary()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string type = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("exercise", out type))
            {
                this.type = int.Parse(type);
                if(this.type == 1) /* Running */ {
                    
                    // grid GPS -> visible
                    GPS.Visibility = System.Windows.Visibility.Visible;

                    // ganti judul
                    gps_execise.Text = "RUNNING";

                    // ganti background
                    ImageBrush img = new ImageBrush 
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/bg_start.jpg",UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.UniformToFill
                    };
                    Header.Background = img;

                    // ganti color scheme
                    fithub_Copy.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_execise.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_calories.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_distance.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_pace.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_speed.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_time.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));

                    // get List of running data from isolatedStorage
                    runningData = (List<runningData>)IsolatedStorageSettings.ApplicationSettings["running_data"];

                    // get last running 
                    lastRunning = runningData[runningData.Count-1];

                    // and display it in the xaml
                    gps_time.Text = lastBiking.duration;
                    gps_calories.Text = string.Format("{0:f1}", lastRunning.calories);
                    gps_distance.Text = string.Format("{0:f2}", lastRunning.distance);
                    gps_pace.Text = TimeSpan.FromMilliseconds(lastRunning.avgpace).ToString(@"mm\:ss");
                    gps_speed.Text = string.Format("{0:f2}", lastRunning.avgspeed);
                }
                else if (this.type == 2) /* Biking */
                {
                    // grid GPS -> visible
                    GPS.Visibility = System.Windows.Visibility.Visible;

                    // GANTI judul
                    gps_execise.Text = "BIKING";

                    // ganti background
                    ImageBrush img = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/bg_start.jpg", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.UniformToFill
                    };
                    Header.Background = img;

                    // ganti color scheme
                    fithub_Copy.Foreground = new SolidColorBrush(Color.FromArgb(100, 243, 38, 27)); //beluma ada warna
                    gps_execise.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_calories.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_distance.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_pace.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_speed.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));
                    gps_time.Foreground = new SolidColorBrush(Color.FromArgb(100, 126, 66, 161));

                    // get list of biking data from isolated storage
                    bikingData = (List<bikingData>)IsolatedStorageSettings.ApplicationSettings["biking_data"];

                    // get last biking 
                    lastBiking = bikingData[bikingData.Count-1];

                    // DISPLAY IT
                    gps_time.Text = lastBiking.duration;
                    gps_calories.Text = string.Format("{0:f1}",lastBiking.calories);
                    gps_distance.Text = string.Format("{0:f2}",lastBiking.distance);
                    gps_pace.Text = TimeSpan.FromMilliseconds(lastBiking.avgpace).ToString(@"mm\:ss");
                    gps_speed.Text = string.Format("{0:f2}", lastBiking.avgspeed);

                }
                else if (this.type == 3) /* Sit-up */
                {
                    // grid non-GPS -> visible
                    Non_GPS.Visibility = System.Windows.Visibility.Visible;

                    // ganti judul exercise
                    exercise_tb.Text = "SIT-UP";

                    // ganti background
                    ImageBrush img = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/bg_situp.jpg", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.UniformToFill
                    };
                    Header.Background = img;

                    // ganti color scheme
                    fithub_Copy.Foreground = new SolidColorBrush(Color.FromArgb(100, 14, 211, 231));
                    exercise_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 14, 211, 231));
                    count_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 14, 211, 231));
                    time_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 14, 211, 231));
                    calories_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 14, 211, 231));

                    // get list of situp data from isolated storage
                    sitUpData = (List<sitUpData>)IsolatedStorageSettings.ApplicationSettings["situp_data"];

                    // get last situp
                    lastSitup = sitUpData[sitUpData.Count-1];

                    // display last situp to xaml
                    count_tb.Text = lastSitup.count + "";
                    time_tb.Text = lastSitup.duration;
                    calories_tb.Text = lastSitup.calories + "";

                } else /* push-up */ {
                    // grid non-GPS -> visible
                    Non_GPS.Visibility = System.Windows.Visibility.Visible;

                    // ganti judul exercise
                    exercise_tb.Text = "PUSH-UP";

                    // ganti background
                    ImageBrush img = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/Images/bg_pushups.jpg", UriKind.RelativeOrAbsolute)),
                        Stretch = Stretch.UniformToFill
                    };
                    Header.Background = img;

                    // ganti color scheme
                    fithub_Copy.Foreground = new SolidColorBrush(Color.FromArgb(100, 243, 38, 27));
                    exercise_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 243, 38, 27));
                    count_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 243, 38, 27));
                    time_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 243, 38, 27));
                    calories_tb.Foreground = new SolidColorBrush(Color.FromArgb(100, 243, 38, 27));

                    // get list of push up data from isolated storage
                    pushUpData = (List<pushUpData>)IsolatedStorageSettings.ApplicationSettings["pushup_data"];

                    // get last push up data 
                    lastPushup = pushUpData[pushUpData.Count-1];

                    // display last situp to xaml
                    count_tb.Text = lastPushup.count + "";
                    time_tb.Text = lastPushup.duration;
                    calories_tb.Text = lastPushup.calories + "";
                }
            }


        }

        // share facebook
        private void share_fb_Click(object sender, RoutedEventArgs e)
        {

        }

        // share twitter

        
    }
}