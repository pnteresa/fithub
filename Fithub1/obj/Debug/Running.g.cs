﻿#pragma checksum "E:\Microsoft Innovation Center\Competition\Vocomfest 2014 Project\Fithub1\Fithub1\Running.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0FF58C00C82B02213BB5DF2D627BD027"
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
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
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
    
    
    public partial class Running : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ProgressBar customIndeterminateProgressBar;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock fithub;
        
        internal System.Windows.Controls.TextBlock fithub_Copy;
        
        internal System.Windows.Controls.Primitives.ToggleButton pushup_b;
        
        internal System.Windows.Controls.TextBlock avgspeedrun_tb;
        
        internal System.Windows.Controls.TextBlock avgpace_tb;
        
        internal Microsoft.Phone.Maps.Controls.Map MainMap;
        
        internal Microsoft.Phone.Maps.Toolkit.Pushpin Start;
        
        internal Microsoft.Phone.Maps.Toolkit.Pushpin Finish;
        
        internal System.Windows.Controls.TextBlock timeelapserun_tb;
        
        internal System.Windows.Controls.TextBlock avgpace_tb1;
        
        internal System.Windows.Controls.TextBlock avgpace_tb_Copy1;
        
        internal System.Windows.Controls.TextBlock distancerun_tb;
        
        internal System.Windows.Controls.TextBlock avgpace_tb_Copy3;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Fithub1;component/Running.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.customIndeterminateProgressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("customIndeterminateProgressBar")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.fithub = ((System.Windows.Controls.TextBlock)(this.FindName("fithub")));
            this.fithub_Copy = ((System.Windows.Controls.TextBlock)(this.FindName("fithub_Copy")));
            this.pushup_b = ((System.Windows.Controls.Primitives.ToggleButton)(this.FindName("pushup_b")));
            this.avgspeedrun_tb = ((System.Windows.Controls.TextBlock)(this.FindName("avgspeedrun_tb")));
            this.avgpace_tb = ((System.Windows.Controls.TextBlock)(this.FindName("avgpace_tb")));
            this.MainMap = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("MainMap")));
            this.Start = ((Microsoft.Phone.Maps.Toolkit.Pushpin)(this.FindName("Start")));
            this.Finish = ((Microsoft.Phone.Maps.Toolkit.Pushpin)(this.FindName("Finish")));
            this.timeelapserun_tb = ((System.Windows.Controls.TextBlock)(this.FindName("timeelapserun_tb")));
            this.avgpace_tb1 = ((System.Windows.Controls.TextBlock)(this.FindName("avgpace_tb1")));
            this.avgpace_tb_Copy1 = ((System.Windows.Controls.TextBlock)(this.FindName("avgpace_tb_Copy1")));
            this.distancerun_tb = ((System.Windows.Controls.TextBlock)(this.FindName("distancerun_tb")));
            this.avgpace_tb_Copy3 = ((System.Windows.Controls.TextBlock)(this.FindName("avgpace_tb_Copy3")));
        }
    }
}

