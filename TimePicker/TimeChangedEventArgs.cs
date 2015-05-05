using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using CoreGraphics;
using System.ComponentModel;

namespace YuFit.IOS.Controls.TimePicker
{
    public class TimeChangedEventArgs : EventArgs
    {
        public DateTime Time
        {
            get;
            set;
        }

        public TimeChangedEventArgs(DateTime time)
        {
            Time = time;
        }
    }
}

