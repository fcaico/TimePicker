using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Drawing;
using System.ComponentModel;
using Cirrious.FluentLayouts.Touch;

namespace YuFit.IOS.Controls.TimePicker
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

        private DateTime _time = DateTime.MinValue;
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

        private UIImage _backImage = UIImage.FromBundle("TimePicker/DownArrow");
        private UIImage _forwardImage = UIImage.FromBundle("TimePicker/UpArrow");

        private UIColor _labelColor = UIColor.LightGray;
        private UIColor _timeColor = UIColor.White;
        private UIColor _amPmColor = UIColor.White;
        private UIColor _ruleColor = UIColor.White;

        private UIFont _labelFont = UIFont.SystemFontOfSize(22);
        private UIFont _timeFont = UIFont.SystemFontOfSize(60);
        private UIFont _amPmFont = UIFont.SystemFontOfSize(22);

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


        public string Label
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
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

                    _hourPrevButton.SetImage(_backImage, UIControlState.Normal);
                    _minutesPrevButton.SetImage(_backImage, UIControlState.Normal);
                    _amPmPrevButton.SetImage(_backImage, UIControlState.Normal);

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

                    _hourNextButton.SetImage(_forwardImage, UIControlState.Normal);
                    _minutesNextButton.SetImage(_forwardImage, UIControlState.Normal);
                    _amPmNextButton.SetImage(_forwardImage, UIControlState.Normal);

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

            _hourNextButton.SetImage(
                Themes.AnnotationStyleKit.ImageOfUpArrow(new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow), 
                UIControlState.Normal);

            _hourPrevButton.SetImage(
                Themes.AnnotationStyleKit.ImageOfDownArrow(new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow), 
                UIControlState.Normal);

            _minutesNextButton.SetImage(
                Themes.AnnotationStyleKit.ImageOfUpArrow(new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow), 
                UIControlState.Normal);

            _minutesPrevButton.SetImage(
                Themes.AnnotationStyleKit.ImageOfDownArrow(new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow), 
                UIControlState.Normal);

            _amPmNextButton.SetImage(
                Themes.AnnotationStyleKit.ImageOfUpArrow(new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow), 
                UIControlState.Normal);

            _amPmPrevButton.SetImage(
                Themes.AnnotationStyleKit.ImageOfDownArrow(new RectangleF(0, 0, 30, 30), Themes.Colors.YuYellow), 
                UIControlState.Normal);

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

            _timeContainer.AddConstraints(
                _hoursLabel.WithSameTop(_timeContainer),
                _hoursLabel.Left().EqualTo().LeftOf(_timeContainer),
                _hoursLabel.Width().EqualTo().WidthOf(_timeContainer).WithMultiplier(0.30f),
                _separatorLabel.Top().EqualTo().TopOf(_hoursLabel),
                _separatorLabel.Left().EqualTo().RightOf(_hoursLabel),
                _separatorLabel.Width().EqualTo().WidthOf(_timeContainer).WithMultiplier(0.06f),
                _minutesLabel.Top().EqualTo().TopOf(_hoursLabel),
                _minutesLabel.Left().EqualTo().RightOf(_separatorLabel),
                _minutesLabel.Width().EqualTo().WidthOf(_timeContainer).WithMultiplier(0.30f),
                _amPmLabel.CenterY().EqualTo().CenterYOf(_hoursLabel),
                _amPmLabel.Left().EqualTo(5).RightOf(_minutesLabel),
                _amPmLabel.Width().EqualTo().WidthOf(_timeContainer).WithMultiplier(0.15f)

            );

            this.AddConstraints(
                _label.WithSameTop(this),
                _label.WithSameLeft(_topRule),

                _hourNextButton.WithSameTop(this),
                _hourNextButton.CenterX().EqualTo().CenterXOf(_hoursLabel),
                _minutesNextButton.CenterX().EqualTo().CenterXOf(_minutesLabel),
                _minutesNextButton.Top().EqualTo().TopOf(_hourNextButton),
                _amPmNextButton.CenterX().EqualTo().CenterXOf(_amPmLabel),
                _amPmNextButton.Top().EqualTo().TopOf(_hourNextButton),

                _topRule.Top().EqualTo(5).BottomOf(_label),
                _topRule.CenterX().EqualTo().CenterXOf(this),
                _topRule.WithSameWidth(this),
                _topRule.Height().EqualTo(1),
                _timeContainer.Top().EqualTo(5).BottomOf(_topRule),
                _timeContainer.Left().EqualTo(10).RightOf(_label),
                _timeContainer.Right().EqualTo().RightOf(_topRule),
                _timeContainer.Height().EqualTo().HeightOf(_hoursLabel),

                _bottomRule.Top().EqualTo().BottomOf(_timeContainer),
                _bottomRule.CenterX().EqualTo().CenterXOf(this),
                _bottomRule.Left().EqualTo().LeftOf(_topRule),
                _bottomRule.Right().EqualTo().RightOf(_topRule),
                _bottomRule.Height().EqualTo(1),

                _hourPrevButton.Top().EqualTo(5).BottomOf(_bottomRule),
                _hourPrevButton.CenterX().EqualTo().CenterXOf(_hourNextButton),
                _minutesPrevButton.CenterX().EqualTo().CenterXOf(_minutesNextButton),
                _minutesPrevButton.Top().EqualTo().TopOf(_hourPrevButton),
                _amPmPrevButton.CenterX().EqualTo().CenterXOf(_amPmNextButton),
                _amPmPrevButton.Top().EqualTo().TopOf(_hourPrevButton)

            );
        }

        #endregion

        #region Methods

        public override void Draw (RectangleF rect)
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

