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

namespace Fithub1
{
    public partial class Page1 : PhoneApplicationPage
    {
        Accelerometer accelerometer;
        bool atas = false;
        int count = 0;
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (accelerometer == null)
            {
                // Instantiate the Accelerometer.
                accelerometer = new Accelerometer();
                accelerometer.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged);
            }
            accelerometer.Start();
        }
        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            // Call UpdateUI on the UI thread and pass the AccelerometerReading.
            Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));
        }
        private void UpdateUI(AccelerometerReading accelerometerReading)
        {
           // statusTextBlock.Text = "getting data";

            Vector3 acceleration = accelerometerReading.Acceleration;

            // Show the numeric values.
            xTextBlock.Text = "X: " + acceleration.X.ToString("0.00");
            yTextBlock.Text = "Y: " + acceleration.Y.ToString("0.00");
            zTextBlock.Text = "Z: " + acceleration.Z.ToString("0.00");
            if ((double)acceleration.Z > 0.5) {
                if (!atas)
                {
                    count++;
                    ctb.Text = ""+count;
                }
                atas = true;
                
                
            }
            if ((double)acceleration.Z < 0 && atas == true)
            {
                atas = false;
            }

          /*  // Show the values graphically.
            xLine.X2 = xLine.X1 + acceleration.X * 200;
            yLine.Y2 = yLine.Y1 - acceleration.Y * 200;
            zLine.X2 = zLine.X1 - acceleration.Z * 100;
            zLine.Y2 = zLine.Y1 + acceleration.Z * 100;*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (accelerometer != null)
            {
                // Stop the accelerometer.
                accelerometer.Stop();
                // statusTextBlock.Text = "accelerometer stopped.";
            }
        }

    }
}