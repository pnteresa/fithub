﻿#pragma checksum "C:\Users\Teresa\documents\visual studio 2012\Projects\Fithub1\Fithub1\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F8E0744F726C397795D481113D7066A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Fithub1 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock fithub;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox username_tb;
        
        internal System.Windows.Controls.TextBox age_tb;
        
        internal System.Windows.Controls.RadioButton male_rb;
        
        internal System.Windows.Controls.RadioButton female_rb;
        
        internal System.Windows.Controls.TextBox weight_tb;
        
        internal System.Windows.Controls.TextBox height_tb;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Fithub1;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.fithub = ((System.Windows.Controls.TextBlock)(this.FindName("fithub")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.username_tb = ((System.Windows.Controls.TextBox)(this.FindName("username_tb")));
            this.age_tb = ((System.Windows.Controls.TextBox)(this.FindName("age_tb")));
            this.male_rb = ((System.Windows.Controls.RadioButton)(this.FindName("male_rb")));
            this.female_rb = ((System.Windows.Controls.RadioButton)(this.FindName("female_rb")));
            this.weight_tb = ((System.Windows.Controls.TextBox)(this.FindName("weight_tb")));
            this.height_tb = ((System.Windows.Controls.TextBox)(this.FindName("height_tb")));
        }
    }
}

