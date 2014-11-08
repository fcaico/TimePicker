using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Drawing;
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

