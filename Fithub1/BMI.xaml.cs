using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

namespace Fithub1
{
    public partial class BMI : PhoneApplicationPage
    {
        public BMI()
        {
            InitializeComponent();

            List<Double> bmiTemp = new List<Double>();
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("BMI_data", out bmiTemp);
            int jml = bmiTemp.Count() - 1;
            try
            {
                bmiNum.Text = "" + bmiTemp[jml];
            }
            catch {
                bmiCat.Text = "";
            }
            updateList();
            
        }
        private void ahaha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ahaha.SelectedIndex == 1)
            {
                updateList();
            }
        } 
        private void updateList()
        {
            List<Double> bmiTemp = new List<Double>();
            List<DateTime> bT = new List<DateTime>();
            List<string> cT = new List<string>();

            IsolatedStorageSettings.ApplicationSettings.TryGetValue("BMI_data", out bmiTemp);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("BMI_datetime", out bT);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("BMI_cat", out cT);
            try
            {
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            catch(InvalidDataContractException)
            {
                MessageBox.Show("Isolated storage issue");
            }
            

            int jml = bmiTemp.Count();
            //Console.WriteLine(bT[jml - 1]);
            try
            {
                bmi_a_1.Text = "" + bmiTemp[jml - 1];
                bmi_b_1.Text = "" + bT[jml - 1];
                bmi_c_1.Text = "" + cT[jml - 1];
                bmi_a_2.Text = "" + bmiTemp[jml - 2];
                bmi_b_2.Text = "" + bT[jml - 2];
                bmi_c_2.Text = "" + cT[jml - 2];
                bmi_2.Visibility = System.Windows.Visibility.Visible;
                bmi_a_3.Text = "" + bmiTemp[jml - 3];
                bmi_b_3.Text = "" + bT[jml - 3];
                bmi_c_3.Text = "" + cT[jml - 3];
                bmi_3.Visibility = System.Windows.Visibility.Visible;
                bmi_a_4.Text = "" + bmiTemp[jml - 4];
                bmi_b_4.Text = "" + bT[jml - 4];
                bmi_c_4.Text = "" + cT[jml - 4];
                bmi_4.Visibility = System.Windows.Visibility.Visible;
                bmi_a_5.Text = "" + bmiTemp[jml - 5];
                bmi_b_5.Text = "" + bT[jml - 5];
                bmi_c_5.Text = "" + cT[jml - 5];
                bmi_5.Visibility = System.Windows.Visibility.Visible;
            }
            catch { }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dialog_grid.Visibility = System.Windows.Visibility.Visible;
        }

        private void submitNewWeight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Double> bmiTemp = new List<Double>();
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("BMI_data", out bmiTemp);
                double h;
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("height", out h);
                double nw = Double.Parse(newWeight_tb.Text);
                double bmi = nw*1.0 / (h * h) * 10000;
                bmi = Math.Round(bmi, 1);
                bmiTemp.Add(bmi);
                IsolatedStorageSettings.ApplicationSettings["BMI_data_top"] = bmi;
                int jml = bmiTemp.Count() - 1;
                bmiNum.Text = "" + bmiTemp[jml];

                bmiCat.Text = countCat(bmi).ToUpper();

                dialog_grid.Visibility = System.Windows.Visibility.Collapsed;

                DateTime now = DateTime.Now;
                List<DateTime> data = (List<DateTime>) IsolatedStorageSettings.ApplicationSettings["BMI_datetime"];
                data.Add(now);
                List<String> dT = (List<String>)IsolatedStorageSettings.ApplicationSettings["BMI_cat"];
                dT.Add(countCat(bmi));
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            catch
            {
                MessageBox.Show("Please enter your weight");
            }
        }
        public string countCat(double bmi)
        {
            if (bmi < 18.5)
            {
                return "UNDERWEIGHT";
            }
            else if (bmi < 24.9)
            {
                return "NORMAL  ";
            }
            else if (bmi < 29.9)
            {
                return "overweight";
            }
            else if (bmi < 40)
            {
                return "OBESE";
            }
            else
            {
                return "OVEROBESE";
            }
        }
    }
}