using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Windows.Storage;
using System.IO.IsolatedStorage;
using System.Windows.Threading;

namespace Fithub1
{
    public partial class PushUp : PhoneApplicationPage
    {
        public IsolatedStorageSettings dataStorage = IsolatedStorageSettings.ApplicationSettings;
        public bool started = false;
        public int countNow;
        public TimeSpan totalDuration;
        public List<pushUpData> pushupStats;
        public double calories = 0.0;

        public int best;
        public pushUpData today;

        // timer defiinition
        public DispatcherTimer timer = new DispatcherTimer();
        public long startTime;

        public PushUp()
        {
            InitializeComponent();
          //  MessageBox.Show("yeiye");

            // timer definition
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // ambil statsData 
            if (IsolatedStorageSettings.ApplicationSettings.Contains("pushup_data"))
            {
                pushupStats = (List<pushUpData>)IsolatedStorageSettings.ApplicationSettings["pushup_data"];
            }
            else
            {
                pushupStats = new List<pushUpData>();

                if (pushupStats.Count <= 0)
                {
                    how_to_pushup.Visibility = System.Windows.Visibility.Visible;
                }
            }

            // cek apakah terdapat key pushup_best -> sudah pernah dipakai?
            if (IsolatedStorageSettings.ApplicationSettings.Contains("pushup_best"))
            {
                string tmp = (IsolatedStorageSettings.ApplicationSettings["pushup_best"] as string);
                best = int.Parse(tmp);
                pushuprecord_tb.Text= "" + best;
            }
            else
            {
                best = 0;
                pushuprecord_tb.Text = "" + best;
            }

            // cek hari ini
            if (IsolatedStorageSettings.ApplicationSettings.Contains("pushup_today"))
            {
                today = (pushUpData)IsolatedStorageSettings.ApplicationSettings["pushup_today"];
            }
            else
            {
                today = new pushUpData(0, DateTime.Today, calories);
            }
            //MessageBox.Show("yey");

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            totalDuration = TimeSpan.FromMilliseconds(System.Environment.TickCount - startTime);
            pushuptime_tb.Text = totalDuration.ToString(@"hh\:mm\:ss");
        }

        private void pushup_b_Click(object sender, RoutedEventArgs e) //kalau button diclick
        {
            started = !started; //toggle boolean "started"

            if (started)
            {
                pushup_b.Content = "STOP";
                countNow = 0;

                // start the timer 
                timer.Start();

                // declare the startTime
                startTime = System.Environment.TickCount;
            }
            else
            {
                // stop the timer 
                timer.Stop();
                

                /*
                 * Tentukan besar kalori yang dibakar oleh user
                 */

                //if() {

                //} else if() {
                //
                //} else if() {

                // } else if() {

                //}

                /*
                 * Menyimpan statistik sit-up
                 * Today
                 */
                if (DateTime.Today.CompareTo(today.date) == -1)
                {
                    today.date = DateTime.Today;
                    today.count = countNow;
                    today.calories = calories;
                }
                else
                {
                    today.count += countNow;
                    today.calories += calories;
                }
                dataStorage["pushup_today"] = today;

                /*
                * Menyimpan statistik sit-up 
                * Best
                */
                if (countNow > best)
                {
                    best = countNow;
                    pushuprecord_tb.Text = "" + countNow;
                }
                dataStorage["pushup_best"] = best;

                /*
                * Menyimpan  statistik sit-up
                * data stats
                */
                pushupStats.Add(new pushUpData(countNow, DateTime.Now, totalDuration.ToString(@"hh\:mm\:ss"), calories));
                dataStorage["pushup_data"] = pushupStats;

                // save to isolated storage
                dataStorage.Save();
                
                // setup
                pushup_b.Content = "START";
                countNow = 0;
                started = false;
                pushupcount_tb.Text = "0";

                // navigate to summary page
                NavigationService.Navigate(new Uri("/Summary.xaml?exercise=4", UriKind.RelativeOrAbsolute));
            }
        }

        // define the tap area, when start and tapArea tapped, countNow++
        private void pushup_area_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (started) //kalau started, baru bisa diklik
            {
                ++countNow;
                pushupcount_tb.Text = "" + countNow;
            }
                
        }

        // override the back button hardware navigation
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PivotPage1.xaml", UriKind.Relative));
        }

        // application bar button
        private void howto_click(object sender, EventArgs e)
        {
            how_to_pushup.Visibility = System.Windows.Visibility.Visible;
        }

        private void howto_close(object sender, RoutedEventArgs e)
        {
            how_to_pushup.Visibility = System.Windows.Visibility.Collapsed;
        }

    }
}