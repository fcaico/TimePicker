using System;
using Foundation;
using UIKit;
using CoreGraphics;
using System.ComponentModel;

namespace Fcaico.iOS.Controls.TimePicker
{
	enum AmPm 
	{
		AM = 0,
		PM = 1
	};



	[Register ("TimePickerView"), DesignTimeVisible(true)]
	public class TimePickerView : UIView, IComponent
	{
		private static TimeSpan OneHour = TimeSpan.FromHours(1);
		private static TimeSpan FifteenMinutes = TimeSpan.FromMinutes(15);
		private static TimeSpan TwelveHours = TimeSpan.FromHours(12);

		#region Data Members

		private DateTime _time = new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours (1).Hour, 0, 0);
		private DateTime _minTime = DateTime.Now;
		private DateTime _maxTime = new DateTime (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 45, 0);
		private bool _twentyFourHourTime = false;

		private UILabel _label = new UILabel();
		private UILabel _hoursLabel = new UILabel();
		private UILabel _minutesLabel = new UILabel();
		private UILabel _amPmLabel = new UILabel();
		private UILabel _separatorLabel = new UILabel();
		private UIView _topRule = new UIView();
		private UIView _bottomRule = new UIView();
		private UIView _timeContainer = new UIView();

		private UIButton _hourNextButton = new UIButton(UIButtonType.Custom);
		private UIButton _hourPrevButton = new UIButton(UIButtonType.Custom);
		private UIButton _minutesNextButton = new UIButton(UIButtonType.Custom);
		private UIButton _minutesPrevButton = new UIButton(UIButtonType.Custom);
		private UIButton _amPmNextButton = new UIButton(UIButtonType.Custom);
		private UIButton _amPmPrevButton = new UIButton(UIButtonType.Custom);

		private UIImage _backImage;
		private UIImage _forwardImage;

		private UIColor _labelColor = TimePickerStyleKit.LabelColor;
		private UIColor _timeColor = TimePickerStyleKit.TimeColor;
		private UIColor _amPmColor = TimePickerStyleKit.TimeColor;
		private UIColor _ruleColor = TimePickerStyleKit.RuleColor;

		private UIFont _labelFont = TimePickerStyleKit.LabelFont;
		private UIFont _timeFont = TimePickerStyleKit.TimeFont;
		private UIFont _amPmFont = TimePickerStyleKit.AmPmFont;

        private float _timeFontOffset = 0;
        private NSLayoutConstraint _timeOffsetConstraint;

		#endregion 

		public event EventHandler TimeChanged;

		#region Properties

		public DateTime Time
		{
			get
			{
				return _time;
			}
			set
			{
				_time = value;
				SetNeedsDisplay();
			}
		}

		public DateTime MinTime
		{
			get
			{
				return _minTime;
			}
			set
			{
				_minTime = value;
				SetNeedsDisplay();
			}
		}

		public DateTime MaxTime
		{
			get
			{
				return _maxTime;
			}
			set
			{
				_maxTime = value;
				SetNeedsDisplay();
			}
		}


		public string Label
		{
			get
			{
				return _label.Text;
			}
			set
			{
				_label.Text = value;
				SetNeedsDisplay();
			}
		}

		public string Separator
		{
			get
			{
				return _separatorLabel.Text;
			}
			set
			{
				_separatorLabel.Text = value;
				SetNeedsDisplay();
			}
		}


		#region Look and Feel customizations

		[Export("TwentyFourHourTime"), Browsable(true)]
		public bool TwentyFourHourTime
		{ 
			get
			{
				return _twentyFourHourTime; 
			}
			set
			{
				if (_twentyFourHourTime != value)
				{
					_twentyFourHourTime = value; 
					SetNeedsDisplay();
				}
			}
		}

		[Export("BackImage"), Browsable(true)]
		public UIImage BackImage
		{ 
			get
			{
				return _backImage;
			}
			set
			{
				if (_backImage != value)
				{
					_backImage = value;
					SetNeedsDisplay();
				}
			}
		}

		[Export("ForwardImage"), Browsable(true)]
		public UIImage ForwardImage
		{ 
			get
			{
				return _forwardImage;
			}
			set
			{
				if (_forwardImage != value)
				{
					_forwardImage = value;
					SetNeedsDisplay();
				}
			}
		}


		[Export("LabelColor"), Browsable(true)]
		public UIColor LabelColor
		{
			get
			{
				return _labelColor;
			}
			set
			{
				if (_labelColor != value)
				{
					_labelColor = value;
					SetNeedsDisplay();
				}
			}
		}


		[Export("TimeColor"), Browsable(true)]
		public UIColor TimeColor
		{
			get
			{
				return _timeColor;
			}
			set
			{
				if (_timeColor != value)
				{
					_timeColor = value;
					SetNeedsDisplay();
				}
			}
		}

		[Export("AmPmColor"), Browsable(true)]
		public UIColor AmPmColor
		{
			get
			{
				return _amPmColor;
			}
			set
			{
				if (_amPmColor != value)
				{
					_amPmColor = value;
					SetNeedsDisplay();
				}
			}
		}


		[Export("RuleColor"), Browsable(true)]
		public UIColor RuleColor
		{
			get
			{
				return _ruleColor;
			}
			set
			{
				if (_ruleColor != value)
				{
					_ruleColor = value;
					SetNeedsDisplay();
				}
			}
		}


		[Export("LabelFont"), Browsable(true)]
		public UIFont LabelFont
		{ 
			get
			{
				return _labelFont;
			}
			set
			{
				if (_labelFont != value)
				{
					_labelFont = value;
					SetNeedsDisplay();
				}
			}
		}


		[Export("TimeFont"), Browsable(true)]
		public UIFont TimeFont 
		{
			get
			{
				return _timeFont;
			}
			set
			{
				if (_timeFont != value)
				{
					_timeFont = value;
					SetNeedsDisplay();
				}
			}
		}


		[Export("AmPmFont"), Browsable(true)]
		public UIFont AmPmFont 
		{
			get
			{
				return _amPmFont;
			}
			set
			{
				if (_amPmFont != value)
				{
					_amPmFont = value;
					SetNeedsDisplay();
				}
			}
		}

        [Export("TimeFontOffset"), Browsable(true)]
        public float TimeFontOffset 
        {
            get
            {
                return _timeFontOffset;
            }
            set
            {
                _timeFontOffset = value;
                RecalculateTimeConstraints();
            }
        }




		#endregion

		public ISite Site
		{
			get;
			set;
		}

		#endregion

		#region Construction and Destruction

		public TimePickerView (IntPtr handle) : base (handle)
		{
		}


		public TimePickerView () : base ()
		{
			Initialize ();
		}

		private void Initialize()
		{
			BackgroundColor = UIColor.Clear;
			SetupSubviews();
			SetupConstraints();
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			Initialize ();
		}        

		public event EventHandler Disposed;

		private void SetupSubviews()
		{
			this.ClipsToBounds = true;
			Add(_label);

			_hourNextButton.TouchUpInside += OnNextHour;
			Add(_hourNextButton);

			_hourPrevButton.TouchUpInside += OnPrevHour;
			Add(_hourPrevButton);

			_minutesNextButton.TouchUpInside += OnNextMinute;
			Add(_minutesNextButton);

			_minutesPrevButton.TouchUpInside += OnPrevMinute;
			Add(_minutesPrevButton);

			_amPmNextButton.TouchUpInside += OnNextAmPm;
			Add(_amPmNextButton);

			_amPmPrevButton.TouchUpInside += OnPrevAmPm;
			Add(_amPmPrevButton);

			Add(_topRule);
			Add(_bottomRule);

			Add(_hourNextButton);
			Add(_hourPrevButton);
			Add(_minutesNextButton);
			Add(_minutesPrevButton);
			Add(_amPmNextButton);
			Add(_amPmPrevButton);

			_timeContainer.Add(_hoursLabel);
			_timeContainer.Add(_minutesLabel);
			_timeContainer.Add(_separatorLabel);
			_timeContainer.Add(_amPmLabel);

			_timeContainer.ClipsToBounds = true;

			Add(_timeContainer);

			_label.Text = "time";
			_label.AdjustsFontSizeToFitWidth = true;
			_hoursLabel.Text = "12";
			_hoursLabel.TextAlignment = UITextAlignment.Center;
			_hoursLabel.AdjustsFontSizeToFitWidth = true;
			_separatorLabel.Text = ":";
			_separatorLabel.TextAlignment = UITextAlignment.Center;
			_separatorLabel.AdjustsFontSizeToFitWidth = true;
			_minutesLabel.Text = "34";
			_minutesLabel.TextAlignment = UITextAlignment.Center;
			_minutesLabel.AdjustsFontSizeToFitWidth = true;
			_amPmLabel.Text = "AM";
			_amPmLabel.TextAlignment = UITextAlignment.Center;
			_amPmLabel.AdjustsFontSizeToFitWidth = true;

			_backImage = TimePickerStyleKit.ImageOfDownArrow (new CGRect (0, 0, 30, 30), TimePickerStyleKit.ArrowColor);
			_forwardImage = TimePickerStyleKit.ImageOfUpArrow (new CGRect (0, 0, 30, 30), TimePickerStyleKit.ArrowColor);
		}


        private NSLayoutConstraint CreateTimeOffsetConstraint(float offset)
        {
            return NSLayoutConstraint.Create(_timeContainer, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _topRule, NSLayoutAttribute.Bottom, 1, 5f + offset);

        }

        internal void RecalculateTimeConstraints()
        {
            RemoveConstraint(_timeOffsetConstraint); 
            _timeOffsetConstraint = CreateTimeOffsetConstraint(_timeFontOffset);
            AddConstraint(_timeOffsetConstraint);
            SetNeedsLayout();
        }

        private void SetupConstraints()
		{
			_label.TranslatesAutoresizingMaskIntoConstraints = false;
			_topRule.TranslatesAutoresizingMaskIntoConstraints = false;
			_bottomRule.TranslatesAutoresizingMaskIntoConstraints = false;
			_hoursLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			_minutesLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			_separatorLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			_amPmLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			_timeContainer.TranslatesAutoresizingMaskIntoConstraints = false;
			_hourNextButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_minutesNextButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_amPmNextButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_hourPrevButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_minutesPrevButton.TranslatesAutoresizingMaskIntoConstraints = false;
			_amPmPrevButton.TranslatesAutoresizingMaskIntoConstraints = false;


			AddConstraint (NSLayoutConstraint.Create (_hourPrevButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _bottomRule, NSLayoutAttribute.Bottom, 1, 5f));
			AddConstraint (NSLayoutConstraint.Create (_hourPrevButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _hourNextButton, NSLayoutAttribute.CenterX, 1, 0));

			AddConstraint (NSLayoutConstraint.Create (_minutesPrevButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _hourPrevButton, NSLayoutAttribute.Top, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_minutesPrevButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _minutesNextButton, NSLayoutAttribute.CenterX, 1, 0));

			AddConstraint (NSLayoutConstraint.Create (_amPmPrevButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _hourPrevButton, NSLayoutAttribute.Top, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_amPmPrevButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _amPmNextButton, NSLayoutAttribute.CenterX, 1, 0));

            AddConstraint(NSLayoutConstraint.Create(_label, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_label, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _topRule, NSLayoutAttribute.Left, 1, 0));

			AddConstraint (NSLayoutConstraint.Create (_hourNextButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_hourNextButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _hoursLabel, NSLayoutAttribute.CenterX, 1, 0));

			AddConstraint (NSLayoutConstraint.Create (_minutesNextButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _hourNextButton, NSLayoutAttribute.Top, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_minutesNextButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _minutesLabel, NSLayoutAttribute.CenterX, 1, 0));

			AddConstraint (NSLayoutConstraint.Create (_amPmNextButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _hourNextButton, NSLayoutAttribute.Top, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_amPmNextButton, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, _amPmLabel, NSLayoutAttribute.CenterX, 1, 0));

			AddConstraint (NSLayoutConstraint.Create (_topRule, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _label, NSLayoutAttribute.Bottom, 1, 5f));
			AddConstraint (NSLayoutConstraint.Create (_topRule, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_topRule, NSLayoutAttribute.Width, NSLayoutRelation.Equal, this, NSLayoutAttribute.Width, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_topRule, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 1f));

            _timeOffsetConstraint = CreateTimeOffsetConstraint(_timeFontOffset);
            AddConstraint(_timeOffsetConstraint);
			//AddConstraint (NSLayoutConstraint.Create (_timeContainer, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _topRule, NSLayoutAttribute.Bottom, 1, 5f));
			AddConstraint (NSLayoutConstraint.Create (_timeContainer, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _label, NSLayoutAttribute.Right, 1, 10f));
			AddConstraint (NSLayoutConstraint.Create (_timeContainer, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _topRule, NSLayoutAttribute.Right, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_timeContainer, NSLayoutAttribute.Height, NSLayoutRelation.Equal, _hoursLabel, NSLayoutAttribute.Height, 1, 0));

			AddConstraint (NSLayoutConstraint.Create (_bottomRule, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _timeContainer, NSLayoutAttribute.Bottom, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_bottomRule, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterX, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_bottomRule, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _topRule, NSLayoutAttribute.Left, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_bottomRule, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _topRule, NSLayoutAttribute.Right, 1, 0));
			AddConstraint (NSLayoutConstraint.Create (_bottomRule, NSLayoutAttribute.Height, NSLayoutRelation.Equal, _topRule, NSLayoutAttribute.Height, 1, 0));

            _timeContainer.AddConstraint (NSLayoutConstraint.Create(_hoursLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _timeContainer, NSLayoutAttribute.Top, 1, 0));
            _timeContainer.AddConstraint (NSLayoutConstraint.Create (_hoursLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _timeContainer, NSLayoutAttribute.Left, 1, 0));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_hoursLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _timeContainer, NSLayoutAttribute.Width, 0.3f, 0));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_separatorLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _hoursLabel, NSLayoutAttribute.CenterY, 1, 0));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_separatorLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _hoursLabel, NSLayoutAttribute.Right, 1, 0));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_separatorLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _timeContainer, NSLayoutAttribute.Width, 0.06f, 0));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_minutesLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal, _hoursLabel, NSLayoutAttribute.Top, 1, 0));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_minutesLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _separatorLabel, NSLayoutAttribute.Right, 1, 0));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_minutesLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _timeContainer, NSLayoutAttribute.Width, 0.3f, 0));

            _timeContainer.AddConstraint (NSLayoutConstraint.Create(_amPmLabel, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, _hoursLabel, NSLayoutAttribute.CenterY, 1, 0));
            _timeContainer.AddConstraint (NSLayoutConstraint.Create (_amPmLabel, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _minutesLabel, NSLayoutAttribute.Right, 1, 5f));
			_timeContainer.AddConstraint (NSLayoutConstraint.Create (_amPmLabel, NSLayoutAttribute.Width, NSLayoutRelation.Equal, _timeContainer, NSLayoutAttribute.Width, 0.15f, 0));

		}

		#endregion

		#region Methods

		public override void Draw (CGRect rect)
		{
			int hour = Time.Hour;
			int minute = Time.Minute;

			if (!TwentyFourHourTime)
			{
				if (hour >= 0 && hour < 12)
				{
					_amPmLabel.Text = "AM";
				}
				else
				{
					hour -= 12;
					_amPmLabel.Text = "PM";
				}               
				if (hour == 0)
				{
					hour = 12;
				}
			}
			else
			{
				_amPmLabel.Text = "24HR";
			}

			_label.Font = LabelFont;
			_label.TextColor = LabelColor;

			_hoursLabel.Text = hour.ToString();
			_hoursLabel.Font = TimeFont;
			_hoursLabel.TextColor = TimeColor;

			_separatorLabel.Font = TimeFont;
			_separatorLabel.TextColor = TimeColor;

			_minutesLabel.Text = minute.ToString("00");
			_minutesLabel.Font = TimeFont;
			_minutesLabel.TextColor = TimeColor;


			_amPmLabel.Font = AmPmFont;
			_amPmLabel.TextColor = AmPmColor;

			_topRule.BackgroundColor = RuleColor;
			_bottomRule.BackgroundColor = RuleColor;

			_hourPrevButton.SetImage (_backImage, UIControlState.Normal);
			_hourNextButton.SetImage (_forwardImage, UIControlState.Normal);

			_minutesPrevButton.SetImage (_backImage, UIControlState.Normal);
			_minutesNextButton.SetImage (_forwardImage, UIControlState.Normal);

			_amPmPrevButton.SetImage (_backImage, UIControlState.Normal);
			_amPmNextButton.SetImage (_forwardImage, UIControlState.Normal);

			_hourPrevButton.Hidden = (Time.Subtract (OneHour) < MinTime);
			_hourNextButton.Hidden = (Time.Add (OneHour) > MaxTime);

			_minutesPrevButton.Hidden = (Time.Subtract (FifteenMinutes) < MinTime);
			_minutesNextButton.Hidden = (Time.Add (FifteenMinutes) > MaxTime);

			_amPmPrevButton.Hidden = (Time.Subtract (TwelveHours) < MinTime);
			_amPmNextButton.Hidden = (Time.Add (TwelveHours) > MaxTime);

			base.Draw(rect);
		}

		void OnNextHour (object sender, EventArgs e)
		{
			Time = Time.Add(OneHour);
			RaiseTimeChanged();
		}

		void OnPrevHour (object sender, EventArgs e)
		{
			Time = Time.Subtract(OneHour);
			RaiseTimeChanged();
		}

		void OnNextMinute (object sender, EventArgs e)
		{
			Time = Time.Add(FifteenMinutes);
			RaiseTimeChanged();
		}

		void OnPrevMinute (object sender, EventArgs e)
		{
			Time = Time.Subtract(FifteenMinutes);
			RaiseTimeChanged();
		}

		void OnNextAmPm (object sender, EventArgs e)
		{
			Time = Time.Add(TwelveHours);
			RaiseTimeChanged();
		}

		void OnPrevAmPm (object sender, EventArgs e)
		{
			Time = Time.Subtract(TwelveHours);
			RaiseTimeChanged();
		}

		private void RaiseTimeChanged()
		{
			// Make a temporary copy of the event to avoid possibility of 
			// a race condition if the last subscriber unsubscribes 
			// immediately after the null check and before the event is raised.
			EventHandler  handler = TimeChanged;

			// Event will be null if there are no subscribers 
			if (handler != null)
			{
				// Use the () operator to raise the event.
				handler(this, new EventArgs());
			}
		}

		#endregion
	}



}

