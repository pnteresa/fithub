using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Fithub1.Resources;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Toolkit;

namespace Fithub1
{
    public partial class Running : PhoneApplicationPage
    {
        List<runningData> runningData;
        runningData todayRunning;
        double distanceNow = 0.0;
        double calories = 0.0;
        GeoCoordinate previousPoint;
        double avgspeed;
        double avgpace;

        // create watcher and polyline object
        private GeoCoordinateWatcher _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
        private MapPolyline _line;

        // timer properties
        private DispatcherTimer _timer = new DispatcherTimer();
        private long _startTime;
        TimeSpan totalDuration;

        // button switch
        bool buttonState = false;

        public Running()
        {
            InitializeComponent();

            // create a line which illustrates the run
            _line = new MapPolyline();
            _line.StrokeColor = Colors.Purple;
            _line.StrokeThickness = 8;
            MainMap.MapElements.Add(_line);

            // execute when position of GeoPositionWatcher changed
            _watcher.PositionChanged += Watcher_PositionChanged;

            // execute when timer tick performed
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            
        }

        // when user start or stop exercise
        private void pushup_b_Click(object sender, RoutedEventArgs e)
        {
            if (!buttonState)
            {
                try
                {
                    // remove current polyline
                    //MainMap.MapElements.Remove(_line);

                    Pushpin p = FindName("Finish") as Pushpin;
                    p.Visibility = System.Windows.Visibility.Collapsed;

                    // GET CURRENT PHONE LOCATION 
                    GetLocationNow("Start");

                    // 
                    customIndeterminateProgressBar.Visibility = System.Windows.Visibility.Visible;
                    customIndeterminateProgressBar.IsIndeterminate = true;
                    _watcher.Start();
                    customIndeterminateProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                    customIndeterminateProgressBar.IsIndeterminate = false;

                    // start the timer 
                    _timer.Start();
                    _startTime = System.Environment.TickCount;


                    buttonState = true;


                }
                catch (Exception)
                {
                    MessageBox.Show("Location data is disabled. Please enabled in phone settings.", "Location settings disabled", MessageBoxButton.OK);
                }
            }
            else
            {
                buttonState = false;
                // stop watcher and timer
                _watcher.Stop();
                _timer.Stop();

                // place pushpin in last location
                GetLocationNow("Finish");

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
                if (DateTime.Today.CompareTo(todayRunning.date) == -1)
                {
                    todayRunning.date = DateTime.Today;
                    todayRunning.distance = distanceNow;
                    todayRunning.calories = calories;
                }
                else
                {
                    todayRunning.distance += distanceNow;
                    todayRunning.calories += calories;
                }
                IsolatedStorageSettings.ApplicationSettings["running_today"] = todayRunning;

                /*
                 * Menyimpan  statistik running
                 * runningData
                 */
                runningData.Add(new runningData(DateTime.Now, distanceNow, totalDuration.ToString(@"hh\:mm\:ss"), avgspeed, avgpace, calories));
                IsolatedStorageSettings.ApplicationSettings["running_data"] = runningData;

                // don't forget to save
                IsolatedStorageSettings.ApplicationSettings.Save();

                // setup
                avgpace_tb.Text = "00:00";
                avgspeedrun_tb.Text = "0 kph";
                distancerun_tb.Text = "0 km";

                // navigate to summary page
                NavigationService.Navigate(new Uri("/Summary.xaml?exercise=1", UriKind.RelativeOrAbsolute));
            }
        }

        // override when phone navigated to this page
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // ambil running_data 
            if (IsolatedStorageSettings.ApplicationSettings.Contains("running_data"))
            {
                runningData = (List<runningData>)IsolatedStorageSettings.ApplicationSettings["running_data"];
            }
            else
            {
                runningData = new List<runningData>();
            }

            // ambil stats today
            if (IsolatedStorageSettings.ApplicationSettings.Contains("running_today"))
            {
                todayRunning = (runningData)IsolatedStorageSettings.ApplicationSettings["running_today"];
            }
            else
            {
                todayRunning = new runningData(DateTime.Today, 0.0, 0.0);
            }
            
            
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                //User already gave us his agreement for using his position
                if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] == true)

                    return;
                //If he didn't we ask for it
                else
                {
                    MessageBoxResult result =
                                MessageBox.Show("Can I use your position?",
                                "Location",
                                MessageBoxButton.OKCancel);

                    if (result == MessageBoxResult.OK)
                    {
                        IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                    }
                    else
                    {
                        IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                    }

                    IsolatedStorageSettings.ApplicationSettings.Save();
                }
            }

                //Ask for user agreement in using his position
            else
            {
                MessageBoxResult result =
                            MessageBox.Show("Can I use your position?",
                            "Location",
                            MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                }
                else
                {
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                }

                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        // called when timer tick
        private void Timer_Tick(object sender, EventArgs e)
        {
            totalDuration = TimeSpan.FromMilliseconds(System.Environment.TickCount - _startTime);
            timeelapserun_tb.Text = totalDuration.ToString(@"hh\:mm\:ss");
        }

        // convert the value of Geocoordinate to GeoCoordinate 
        public GeoCoordinate ConvertGeoCoOrdinate(Geocoordinate geoCooridinate)
        {
            return new GeoCoordinate(
                geoCooridinate.Latitude,
                geoCooridinate.Longitude,
                geoCooridinate.Altitude ?? double.NaN,
                geoCooridinate.Accuracy,
                geoCooridinate.AltitudeAccuracy ?? double.NaN,
                geoCooridinate.Speed ?? double.NaN,
                geoCooridinate.Heading ?? double.NaN);
        }

        // helper method: get current location
        private async void GetLocationNow(String condition)
        {
            try
            {
                Geolocator locationFinder = new Geolocator
                {
                    DesiredAccuracyInMeters = 50,
                    DesiredAccuracy = PositionAccuracy.Default
                };

                customIndeterminateProgressBar.Visibility = System.Windows.Visibility.Visible;
                customIndeterminateProgressBar.IsIndeterminate = true;
                Geoposition currentLocation = await locationFinder.GetGeopositionAsync(
                maximumAge: TimeSpan.FromSeconds(120),
                timeout: TimeSpan.FromSeconds(10));

                //create pushpin
                PlacePushpin(currentLocation, condition);
                customIndeterminateProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                customIndeterminateProgressBar.IsIndeterminate = false;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location data is disabled. Please enable in your phone settings.", "Location settings disabled", MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Location data is disabled. Please enable in your phone settings.", "Location settings disabled", MessageBoxButton.OK);
            }
        }

        // create push pin in start location and finish location
        private void PlacePushpin(Geoposition g, String condition)
        {
            Geocoordinate geocoor = g.Coordinate;
            GeoCoordinate convert = ConvertGeoCoOrdinate(geocoor);

            Pushpin pushpin = (Pushpin)this.FindName(condition);
            if (condition.Equals("Start"))
            {
                pushpin.Background = new SolidColorBrush(Color.FromArgb(100, 195, 31, 31));
            }
            else
            {
                pushpin.Background = new SolidColorBrush(Color.FromArgb(100, 20, 28, 201));
            }

            pushpin.GeoCoordinate = convert;
            pushpin.Visibility = System.Windows.Visibility.Visible;
        }

        

        //ID_CAP_LOCATION
        private double _kilometres;
        private long _previousPositionChangeTick;

        private void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {

            customIndeterminateProgressBar.Visibility = System.Windows.Visibility.Visible;
            customIndeterminateProgressBar.IsIndeterminate = true;

            var coord = new GeoCoordinate(e.Position.Location.Latitude, e.Position.Location.Longitude);

            customIndeterminateProgressBar.Visibility = System.Windows.Visibility.Collapsed;
            customIndeterminateProgressBar.IsIndeterminate = false;

            if (_line.Path.Count > 0)
            {
                // calculate the properties
                previousPoint = _line.Path.Last();
                distanceNow = coord.GetDistanceTo(previousPoint);
                avgspeed = e.Position.Location.Speed;
                avgpace = (1000.0 / distanceNow) * (System.Environment.TickCount - _previousPositionChangeTick);
                _kilometres += distanceNow / 1000.0;

                // set the textBox text
                avgpace_tb.Text = TimeSpan.FromMilliseconds(avgpace).ToString(@"mm\:ss");
                distancerun_tb.Text = string.Format("{0:f2} km", _kilometres);
                //caloriesBox.Text = string.Format("{0:f1}", _kilometres * 65.0);
                avgspeedrun_tb.Text = string.Format("{0:f2} kph", avgspeed);

                MainMap.SetView(coord, 17);

            }
            else
            {
                // set view on map
                MainMap.Center = coord;
                MainMap.SetView(coord, 17);
            }

            // add position to path on _line
            _line.Path.Add(coord);
            _previousPositionChangeTick = System.Environment.TickCount;


        }

       
    }
}