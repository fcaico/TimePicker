using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace TimePickerSample
{
	partial class TimePickerViewController : UIViewController
	{
		public TimePickerViewController (IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TimePicker.Time = new DateTime (DateTime.Now.Year, DateTime.Now.Month, 25, DateTime.Now.AddHours (1).Hour, 0, 0);
			TimePicker.MinTime = new DateTime (DateTime.Now.Year, DateTime.Now.Month, 25);
			TimePicker.MaxTime = TimePicker.MaxTime.AddDays (1);

		}
	}
}
