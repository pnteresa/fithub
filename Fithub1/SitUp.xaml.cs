using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using Windows.Storage;
using System.IO.IsolatedStorage;
using System.Windows.Threading;
using System.Runtime.Serialization;

namespace Fithub1
{
    [DataContract]
    public partial class SitUp : PhoneApplicationPage
    {
        IsolatedStorageSettings dataStorage = IsolatedStorageSettings.ApplicationSettings;

        Accelerometer accelerometer;
        bool atas = false;
        int count = 0;
        bool start = false;
        double calories = 0.0;
        
        List<sitUpData> statsData;
        int best;
        sitUpData today;


        // timer properties
        DispatcherTimer timer = new DispatcherTimer();
        long startTime;
        TimeSpan totalDuration;

        public SitUp()
        {
          
            InitializeComponent();

            // timer definition
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;


        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

                // cek apakah terdapat key situp_best -> sudah pernah dipakai?
                if (IsolatedStorageSettings.ApplicationSettings.Contains("situp_best"))
                {
                    best = (int)IsolatedStorageSettings.ApplicationSettings["situp_best"]; ;
                    situprecord_tb.Text = "" + best;
                }
                else
                {
                    best = 0;
                    situprecord_tb.Text = ""+best;
                }
                
                // cek hari ini
                if (IsolatedStorageSettings.ApplicationSettings.Contains("situp_today"))
                {
                    today = (sitUpData)IsolatedStorageSettings.ApplicationSettings["situp_today"];
                }
                else
                {
                    today = new sitUpData(0, DateTime.Today, 0.0);
                }

                // cek apakah user pertama kali mencoba fitur sit-up
                if (IsolatedStorageSettings.ApplicationSettings.Contains("situp_data"))
                {
                    statsData = (List<sitUpData>)IsolatedStorageSettings.ApplicationSettings["situp_data"];
                }
                else
                {
                    statsData = new List<sitUpData>();

                    if (statsData.Count <= 0)
                    {
                        how_to_situp.Visibility = System.Windows.Visibility.Visible;
                    }
                }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            totalDuration = TimeSpan.FromMilliseconds(System.Environment.TickCount - startTime);
            situptime_tb.Text = totalDuration.ToString(@"hh\:mm\:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!start)
            {
                timer.Start();
                start = true;
                situp_b.Content = "STOP";

                try
                {
                    if (accelerometer == null)
                    {
                        // Instantiate the Accelerometer.
                        accelerometer = new Accelerometer();
                        accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                        accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
                    }
                }
                    // catch the exception if accelerometer fail when starts
                catch(AccelerometerFailedException) {
                    MessageBox.Show("Your phone don't have accelerometer OR your phone accelerometer is broken", "Accelerometer Error", MessageBoxButton.OK);
                }
                
                
                // start accelerometer
                accelerometer.Start();

                // start time
                startTime = System.Environment.TickCount;
                
            }
            else
            {
                timer.Stop();

                // stop accelerometer
                stop();

                atas = false;


                situpcount_tb.Text = "0";
                

                /*
                 * Menyimpan statistik sit-up 
                 * Best
                 */
                if (count > best)
                {
                    IsolatedStorageSettings.ApplicationSettings["situp_best"] = count;
                    situprecord_tb.Text = ""+ count;
                }
                

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
                    today.count = count;
                    today.calories = calories;
                }
                else
                {
                    today.count += count;
                    today.calories += calories;
                }
                dataStorage["situp_today"] = today;


                /*
                 * Menyimpan  statistik sit-up
                 * data stats
                 */
                statsData.Add(new sitUpData(count, DateTime.Now, totalDuration.ToString(@"hh\:mm\:ss"), calories));
                dataStorage["situp_data"] = statsData;


                // save ke isolatedStorage
                // wajib dilakukan
                dataStorage.Save();

                // set-up 
                count = 0;
                situp_b.Content = "START";
                start = false;
                situpcount_tb.Text = "0";

                // navigate ke summary page
                NavigationService.Navigate(new Uri("/Summary.xaml?exercise=3", UriKind.RelativeOrAbsolute));

            }
        }

        // execute when acclerometer value has changed
        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            // Call UpdateUI on the UI thread and pass the AccelerometerReading.
            Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));
        }

        // update the textbox when value has changed
        private void UpdateUI(AccelerometerReading accelerometerReading)
        {
            Vector3 acceleration = accelerometerReading.Acceleration;
   
            if ((double)acceleration.Z > 0.5)
            {
                if (!atas)
                {
                    count++;
                    situpcount_tb.Text = "" + count;
                }
                atas = true;


            }

            if ((double)acceleration.Z < 0 && atas == true)
            {
                atas = false;
            }

        }

        // stop the accelerometer
        private void stop()
        {

            if (accelerometer != null)
            {
                // Stop the accelerometer.
                accelerometer.Stop();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            how_to_situp.Visibility = System.Windows.Visibility.Collapsed;

        }

        // override the back button hardware 
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PivotPage1.xaml", UriKind.Relative));
        }

        // application bar button
        private void howto_click(object sender, EventArgs e)
        {
            how_to_situp.Visibility = System.Windows.Visibility.Visible;
        }
    }
}