using System;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace Fcaico.iOS.Controls.TimePicker
{
	internal static class TimePickerStyleKit
	{
		public static UIColor TimeColor = UIColor.Black;
		public static UIColor LabelColor = UIColor.DarkGray;
		public static UIColor RuleColor = UIColor.Orange;
		public static UIColor ArrowColor = UIColor.Orange;

		public static  UIFont LabelFont = UIFont.SystemFontOfSize(22);
		public static  UIFont TimeFont = UIFont.SystemFontOfSize(60);
		public static  UIFont AmPmFont = UIFont.SystemFontOfSize(22);

		public static void DrawUpArrow(RectangleF frame, UIColor monthChangeColor)
		{
			// Subframes
			RectangleF group = new RectangleF(frame.GetMinX() + (float)Math.Floor(frame.Width * 0.07012f + 0.4f) + 0.1f, frame.GetMinY() + (float)Math.Floor(frame.Height * 0.19852f + 0.31f) + 0.19f, (float)Math.Floor(frame.Width * 0.94965f + 0.01f) - (float)Math.Floor(frame.Width * 0.07012f + 0.4f) + 0.39f, (float)Math.Floor(frame.Height * 0.69357f + 0.46f) - (float)Math.Floor(frame.Height * 0.19852f + 0.31f) - 0.15f);


			// Group
			{
				// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(group.GetMinX() + 0.05351f * group.Width, group.GetMinY() + 0.99301f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.53317f * group.Width, group.GetMinY() + 0.14365f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.47916f * group.Width, group.GetMinY() + 0.14276f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.94550f * group.Width, group.GetMinY() + 1.00000f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 1.00000f * group.Width, group.GetMinY() + 0.90641f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.53366f * group.Width, group.GetMinY() + 0.04917f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.50692f * group.Width, group.GetMinY() + 0.00000f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.47966f * group.Width, group.GetMinY() + 0.04827f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.00000f * group.Width, group.GetMinY() + 0.89762f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.05351f * group.Width, group.GetMinY() + 0.99301f * group.Height));
				bezierPath.ClosePath();
				monthChangeColor.SetFill();
				bezierPath.Fill();
			}
		}

		public static void DrawDownArrow(RectangleF frame, UIColor monthChangeColor)
		{


			// Subframes
			RectangleF group = new RectangleF(frame.GetMinX() + (float)Math.Floor(frame.Width * 0.07012f + 0.4f) + 0.1f, frame.GetMinY() + (float)Math.Floor(frame.Height * 0.20643f - 0.46f) + 0.96f, (float)Math.Floor(frame.Width * 0.94965f + 0.01f) - (float)Math.Floor(frame.Width * 0.07012f + 0.4f) + 0.39f, (float)Math.Floor(frame.Height * 0.70148f - 0.31f) - (float)Math.Floor(frame.Height * 0.20643f - 0.46f) - 0.15f);


			// Group
			{
				// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(group.GetMinX() + 0.05351f * group.Width, group.GetMinY() + 0.00699f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.53317f * group.Width, group.GetMinY() + 0.85635f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.47916f * group.Width, group.GetMinY() + 0.85724f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.94550f * group.Width, group.GetMinY() + 0.00000f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 1.00000f * group.Width, group.GetMinY() + 0.09359f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.53366f * group.Width, group.GetMinY() + 0.95083f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.50692f * group.Width, group.GetMinY() + 1.00000f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.47966f * group.Width, group.GetMinY() + 0.95173f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.00000f * group.Width, group.GetMinY() + 0.10238f * group.Height));
				bezierPath.AddLineTo(new PointF(group.GetMinX() + 0.05351f * group.Width, group.GetMinY() + 0.00699f * group.Height));
				bezierPath.ClosePath();
				monthChangeColor.SetFill();
				bezierPath.Fill();
			}
		}


		public static UIImage ImageOfUpArrow(RectangleF frame, UIColor monthChangeColor)
		{
			UIGraphics.BeginImageContextWithOptions(frame.Size, false, 0);
			TimePickerStyleKit.DrawUpArrow(frame, monthChangeColor);

			var imageOfUpArrow = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return imageOfUpArrow;
		}

		public static UIImage ImageOfDownArrow(RectangleF frame, UIColor monthChangeColor)
		{
			UIGraphics.BeginImageContextWithOptions(frame.Size, false, 0);
			TimePickerStyleKit.DrawDownArrow(frame, monthChangeColor);

			var imageOfDownArrow = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return imageOfDownArrow;
		}
	}
}

