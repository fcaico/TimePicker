// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using UIKit;
using System;
using System.CodeDom.Compiler;

namespace TimePickerSample
{
	[Register ("TimePickerViewController")]
	partial class TimePickerViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Fcaico.iOS.Controls.TimePicker.TimePickerView TimePicker { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (TimePicker != null) {
				TimePicker.Dispose ();
				TimePicker = null;
			}
		}
	}
}
