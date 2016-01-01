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

using ArrayList = System.Collections.Generic.List<object>;

namespace Fithub1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private readonly object _sync = new object();
        public MainPage()
        {
            InitializeComponent();
        }

        // cek apakah pertama kali visit atau tidak
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
           
            if (settings.Contains("username"))
            {
                NavigationService.Navigate(new Uri("/PivotPage1.xaml", UriKind.Relative));
            }
        }
            

            
        
        public bool formChecked() 
        {
            bool formisFull = false;

            formisFull = !(username_tb.Text.Trim().Length == 0) && 
                !(age_tb.Text.Trim().Length == 0) && 
                !(weight_tb.Text.Trim().Length == 0) &&
                !(height_tb.Text.Trim().Length == 0) &&
                (((bool)male_rb.IsChecked) || ((bool)female_rb.IsChecked));

            //Console.WriteLine("hehe");

            //MessageBox.Show("" + (username_tb.Text.Trim().Length == 0));
            return formisFull;
        }

        private void ok_click(object sender, EventArgs e)
        {
            if (formChecked()) //kalau isiannya full
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                int nage = (int) Double.Parse(age_tb.Text);
                double nheight = Double.Parse(height_tb.Text);
                double nweight = Double.Parse(weight_tb.Text);
                bool ismale = false;
                if ((bool)female_rb.IsChecked) ismale = true;

               
                IsolatedStorageSettings.ApplicationSettings["username"] = username_tb.Text;
                IsolatedStorageSettings.ApplicationSettings["age"] = nage;
                IsolatedStorageSettings.ApplicationSettings["height"] = nheight;
                IsolatedStorageSettings.ApplicationSettings["weight"] = nweight;
                IsolatedStorageSettings.ApplicationSettings["genderisMale"] = ismale;

                List<Double> tem = new List<Double>();
                double bmi = nweight*1.0 / (nheight * nheight) * 10000;
                bmi = Math.Round(bmi, 1);
                tem.Add(bmi);

                IsolatedStorageSettings.ApplicationSettings["BMI_data_top"] = bmi;
                IsolatedStorageSettings.ApplicationSettings["BMI_data"] = tem;

                IsolatedStorageSettings.ApplicationSettings["BMI_cat"] = new List<string>();
                List<string> dataC = (List<string>)IsolatedStorageSettings.ApplicationSettings["BMI_cat"];
                IsolatedStorageSettings.ApplicationSettings["today"] = DateTime.Now.Date;
                
                dataC.Add(countCat(bmi));
                IsolatedStorageSettings.ApplicationSettings.Save();
                              
                                
                NavigationService.Navigate(new Uri("/MainPage2.xaml", UriKind.Relative));
            }
            else 
            {
                MessageBox.Show("Please complete the form", "Form is not complete", MessageBoxButton.OK);
            }
        }
        public string countCat(double bmi)
        {
            if (bmi < 18.5)
            {
                return "Underweight";
            }
            else if (bmi < 24.9)
            {
                return "Normal";
            }
            else if (bmi < 29.9)
            {
                return "Overweight";
            }
            else if (bmi < 40)
            {
                return "Obese";
            }
            else
            {
                return "OverlyObese";
            }
        }
         

    }
}