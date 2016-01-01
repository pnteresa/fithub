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
using System.Windows.Media;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO.IsolatedStorage;

namespace Fithub1
{
    public partial class MainPage2 : PhoneApplicationPage
    {
        public MainPage2()
        {
            InitializeComponent();
            
            IsolatedStorageSettings.ApplicationSettings["BMI_datetime"] = new List<DateTime>();
            DateTime now = DateTime.Now;
            List<DateTime> data = (List<DateTime>)IsolatedStorageSettings.ApplicationSettings["BMI_datetime"];
            data.Add(now);
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void dailySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dailyGoal_tb.Text = ""+Math.Round(dailySlider.Value,0)+" kcal";
        }

        private void ok_click(object sender, EventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings["daily_goal"] = (int) Math.Round(dailySlider.Value);
            NavigationService.Navigate(new Uri("/PivotPage1.xaml", UriKind.Relative));
        }
    }
}