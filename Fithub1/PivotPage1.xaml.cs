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
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

namespace Fithub1
{
    
    public partial class PivotPage1 : PhoneApplicationPage
    {
        public IsolatedStorageSettings dataStorage = IsolatedStorageSettings.ApplicationSettings;

        public PivotPage1()
        {
            
            
            InitializeComponent();

            //METHOD UPDATE
            ShellTile myTile = ShellTile.ActiveTiles.First();
                if (myTile != null)
                {
                    // Create a new data to update my tile with
                    FlipTileData newTileData = new FlipTileData
                    {
                        Title = "New Title",
                        //BackgroundImage = new Uri(@”Assets\Tiles\ChangedTileMedium.png”, UriKind.Relative),
                        BackTitle = "New Background Image",
                      //  BackBackgroundImage = new Uri(textBoxBackBackgroundImage.Text, UriKind.Relative),
                        BackContent = "New Back Content"
                    };
                    // Update the application Tile
                    myTile.Update(newTileData);
                }
            //===METHOD UPDATE END

            //string username
            String uName;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.TryGetValue<String>("username", out uName);
            String usernameIS = uName;
            usernameStat_tb.Text = usernameIS.ToUpper();
            //*

            //progressbar dailygoal
            int temp;
            settings.TryGetValue<int>("daily_goal", out temp);
            //y target, x current
            ycal.Text = "" + temp;
            xcal.Text = cariXcal(temp);
            progbar.Value = int.Parse(xcal.Text);
            //*

            //*if day - different
            //IsolatedStorageSettings.ApplicationSettings["today"] = DateTime.Now.Date;
            //if ((DateTime) IsolatedStorageSettings.ApplicationSettings["today"] != DateTime.Now.Date) //kalau udah ada harinya
            //{
                //sementara
                //IsolatedStorageSettings.ApplicationSettings["today"] = DateTime.Now.Date;
                //IsolatedStorageSettings.ApplicationSettings["pushup_today"] = 0;
                //IsolatedStorageSettings.ApplicationSettings["situp_today"] = 0;
               // IsolatedStorageSettings.ApplicationSettings["distance_today"] = 0;
                //IsolatedStorageSettings.ApplicationSettings["calBurned_today"] = 0;
                //IsolatedStorageSettings.ApplicationSettings.Save();
            //}

            //stat today
            //int s1;
            //IsolatedStorageSettings.ApplicationSettings.TryGetValue("calBurned_today", out s1);
            //calorieburnedStat_tb.Text = ""+s1;
            //bmiStat_tb.Text = "" + IsolatedStorageSettings.ApplicationSettings["BMI_data_top"];
            //pushUpStat_tb.Text = "" + IsolatedStorageSettings.ApplicationSettings["pushup_today"];
            //sitUpStat_tb.Text = "" + IsolatedStorageSettings.ApplicationSettings["situp_today"];
            //distanceStat_tb.Text = "" + IsolatedStorageSettings.ApplicationSettings["distance_today"];
            //*
                
            // SIT-UP STATS
            if (IsolatedStorageSettings.ApplicationSettings.Contains("situp_today") && checkStorage("situp_today") != null )
            {
                    sitUpData st = (sitUpData)IsolatedStorageSettings.ApplicationSettings["situp_today"];
                    if (DateTime.Compare(st.date, DateTime.Today) == -1)
                    {
                        st.count = 0;
                        st.calories = 0;
                        st.date = DateTime.Today;
                    }
                    sitUpStat_tb.Text = st.count + "";
            }
            else
            {                
                dataStorage.Add("situp_today", new sitUpData(0, DateTime.Today, 0.0));
                try
                {
                    dataStorage.Save();
                }
                catch(InvalidDataContractException)
                {
                    MessageBox.Show("Isolated storage issue.");
                }
                
                sitUpStat_tb.Text = "0";
                
            }

            // PUSH-UP STATS
            if (IsolatedStorageSettings.ApplicationSettings.Contains("pushup_today") && !(checkStorage("pushup_today").Equals(null)))
            {
                pushUpData pt = (pushUpData) IsolatedStorageSettings.ApplicationSettings["pushup_today"];
                    if (pt.date.CompareTo(DateTime.Today) == -1)
                    {
                        pt.count = 0;
                        pt.calories = 0;
                        pt.date = DateTime.Today;
                    }
                    pushUpStat_tb.Text = pt.count + "";
            }
            else
            {
                //dataStorage["pushup_today"] = new pushUpData(0, DateTime.Today, 0.0);
                dataStorage.Add("pushup_today", new pushUpData(0, DateTime.Today, 0.0));
                try
                {
                    dataStorage.Save();
                }
                catch(InvalidDataContractException)
                {
                    MessageBox.Show("Isolated storage issue");
                }
                
                pushUpStat_tb.Text = "0";
                
            }

            // RUNNING STATS
            if (IsolatedStorageSettings.ApplicationSettings.Contains("running_today") && !(checkStorage("running_today").Equals(null)))
            {

                runningData rt =(runningData) IsolatedStorageSettings.ApplicationSettings["running_today"];
                    if (rt.date.CompareTo(DateTime.Today) == -1)
                    {
                        rt.distance = 0;
                        rt.calories = 0;
                        rt.date = DateTime.Today;
                    }
                    distanceStat_tb.Text = rt.distance + " km";
            }
            else
            {
                //dataStorage["running_today"] = new runningData(DateTime.Today, 0.0, 0.0);
                dataStorage.Add("running_today", new runningData(DateTime.Today, 0.0, 0.0));
                try
                {
                    dataStorage.Save();
                }
                catch (InvalidDataContractException)
                {
                    MessageBox.Show("Isolated storage issue.");
                }
                distanceStat_tb.Text = "0 km";
                //
            }

            // BIKING STATS
            //if (IsolatedStorageSettings.ApplicationSettings.Contains("biking_today"))
            //{
               // runningData bt = (runningData)IsolatedStorageSettings.ApplicationSettings["biking_today"];
               // if (bt.date.CompareTo(DateTime.Today) == -1)
                //{
                   // bt.distance = 0;
                   // bt.calories = 0;
                   // bt.date = DateTime.Today;
               // }
               // distanceStat_tb.Text = bt.distance + "";
            //}
           // else
            //{
              //  IsolatedStorageSettings.ApplicationSettings["biking_today"] = new runningData(DateTime.Today, 0.0, 0.0);
               // distanceStat_tb.Text = "0 km";
            //}

           //dataStorage.Save();
        }

        private object checkStorage(string key)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key) ? IsolatedStorageSettings.ApplicationSettings[key] : null;
        }

        private String cariXcal(int goal)
        {
            int current;
            IsolatedStorageSettings.ApplicationSettings.TryGetValue<int>("today_cal", out current);
            if (current >= goal)
                return "100";
            else
                return ""+current/goal;

        }

        private void Pivot_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Terminate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pv.SelectedIndex = 1;
        }

        private void settings_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.RelativeOrAbsolute));
        }

        private void run_grid_lg_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            runlg_tb.Foreground = new System.Windows.Media.SolidColorBrush(Color.FromArgb(126, 66, 161, 255));
            runlg_tb.Text = "haha";
        }

        private void run_grid_lg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Running.xaml", UriKind.RelativeOrAbsolute));
        }

        private void weight_grid_lg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/BMI.xaml", UriKind.RelativeOrAbsolute));
        }

        private void pushup_grid_lg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PushUp.xaml", UriKind.RelativeOrAbsolute));
        }

        private void situp_grid_lg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/SitUp.xaml", UriKind.RelativeOrAbsolute));
        }

        private void other_grid_lg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
   
        }

        private void pullup_grid_lg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

    }
}