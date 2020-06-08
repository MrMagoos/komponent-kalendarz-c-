using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace kalendar
{
    public enum CalendarViews
        {
            Month = 1,
            Day = 2
        }

        
        public class createCustoCalendar : UserControl
        {
        public DateTime _calendarDate;
        public Font _dayOfWeekFont;
        public Font _daysFont;
        public Font _todayFont;
        public Font _dateHeaderFont;
        public Font _dayViewTimeFont;
        public bool _showArrowControls;
        public bool _showTodayButton;
        public bool _showDateInHeader;
        public Button _btnToday;
        public Button _btnLeft;
        public Button _btnRight;
        public bool _showingToolTip;
        public bool _highlightCurrentDay;
            public CalendarViews _calendarView;

        public readonly List<Rectangle> _rectangles;
        public readonly Dictionary<int, Point> _calendarDays;
        public ContextMenuStrip _contextMenuStrip1;
        public System.ComponentModel.IContainer components;
        public ToolStripMenuItem _miProperties;

        public const int MarginSize = 20;

            
            public Font DayViewTimeFont
            {
                get { return _dayViewTimeFont; }
                set
                {
                    _dayViewTimeFont = value;
                     Refresh();
                }
            }

           
            public CalendarViews CalendarView
            {
                get { return _calendarView; }
                set
                {
                    _calendarView = value;
                    Refresh();
                }
            }

            public bool HighlightCurrentDay
            {
                get { return _highlightCurrentDay; }
                set
                {
                    _highlightCurrentDay = value;
                    Refresh();
                }
            }


            public Font DateHeaderFont
            {
                get { return _dateHeaderFont; }
                set
                {
                    _dateHeaderFont = value;
                    Refresh();
                }
            }

            
            public bool ShowDateInHeader
            {
                get { return _showDateInHeader; }
                set
                {
                    _showDateInHeader = value;
                    if (_calendarView == CalendarViews.Day)
                        ResizeScrollPanel();

                    Refresh();
                }
            }

            
            public bool ShowArrowControls
            {
                get { return _showArrowControls; }
                set
                {
                    _showArrowControls = value;
                    _btnLeft.Visible = value;
                    _btnRight.Visible = value;
                    if (_calendarView == CalendarViews.Day)
                        ResizeScrollPanel();
                    Refresh();
                }
            }

          
            public bool ShowTodayButton
            {
                get { return _showTodayButton; }
                set
                {
                    _showTodayButton = value;
                    _btnToday.Visible = value;
                    if (_calendarView == CalendarViews.Day)
                        ResizeScrollPanel();
                    Refresh();
                }
            }

            
            public Font TodayFont
            {
                get { return _todayFont; }
                set
                {
                    _todayFont = value;
                    Refresh();
                }
            }

           
            public Font DaysFont
            {
                get { return _daysFont; }
                set
                {
                    _daysFont = value;
                    Refresh();
                }
            }

           
            public Font DayOfWeekFont
            {
                get { return _dayOfWeekFont; }
                set
                {
                    _dayOfWeekFont = value;
                    Refresh();
                }
            }

            
            public DateTime CalendarDate
            {
                get { return _calendarDate; }
                set
                {
                    _calendarDate = value;
                    Refresh();
                }
            }

            /// <summary>
            /// konstruktor
            /// </summary>
            public createCustoCalendar()
            {
                InitializeComponent();
                _calendarDate = DateTime.Now;
                _dayOfWeekFont = new Font("Arial", 10, FontStyle.Regular);
                _daysFont = new Font("Arial", 10, FontStyle.Regular);
                _todayFont = new Font("Arial", 10, FontStyle.Bold);
                _dateHeaderFont = new Font("Arial", 12, FontStyle.Bold);
                _dayViewTimeFont = new Font("Arial", 10, FontStyle.Bold);
                _showArrowControls = true;
                _showDateInHeader = true;
                _showTodayButton = true;
                _showingToolTip = false;
                _highlightCurrentDay = true;
                _calendarView = CalendarViews.Month;
          
                _rectangles = new List<Rectangle>();
                _calendarDays = new Dictionary<int, Point>();
      
            }

            private void InitializeComponent()
            {
                this.components = new System.ComponentModel.Container();
                this._btnToday = new Button();
                this._btnLeft = new Button();
                this._btnRight = new Button();
                this._contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
                this._miProperties = new System.Windows.Forms.ToolStripMenuItem();
                this._contextMenuStrip1.SuspendLayout();
                this.SuspendLayout();
                // 
                // _btnToday
                // 
                this._btnToday.BackColor = System.Drawing.Color.Transparent;
               this._btnToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
                this._btnToday.ForeColor = Color.Black;
            this._btnToday.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
                this._btnToday.Text = "Today";
                this._btnToday.Location = new System.Drawing.Point(19, 20);
                this._btnToday.Name = "_btnToday";
                this._btnToday.Size = new System.Drawing.Size(72, 29);
                this._btnToday.TabIndex = 0;
                  this._btnToday.Click += new System.EventHandler(this.BtnTodayButtonClicked);
            // 
            // _btnLeft
            // 
                this._btnLeft.BackColor = System.Drawing.Color.Transparent;
                this._btnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this._btnLeft.ForeColor = Color.Black;
                this._btnLeft.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
                this._btnLeft.Text = "<";
                this._btnLeft.Location = new System.Drawing.Point(98, 20);
                this._btnLeft.Name = "_btnLeft";
                this._btnLeft.Size = new System.Drawing.Size(42, 29);
                this._btnLeft.TabIndex = 1;
                this._btnLeft.Click += new System.EventHandler(this.BtnLeftButtonClicked);
                // 
                // _btnRight
                // 
                this._btnRight.BackColor = System.Drawing.Color.Transparent;
               this._btnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
               this._btnRight.ForeColor = Color.Black;
            this._btnRight.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
               this._btnRight.Text = ">";
               this._btnRight.Location = new System.Drawing.Point(138, 20);
               this._btnRight.Name = "_btnRight";
               this._btnRight.Size = new System.Drawing.Size(42, 29);
               this._btnRight.TabIndex = 2;
            this._btnRight.Click += new System.EventHandler(this.BtnRightButtonClicked);
            this._btnRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            // 
            // _contextMenuStrip1
            // 
            this._contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
             this._miProperties});
                 this._contextMenuStrip1.Name = "_contextMenuStrip1";
                 this._contextMenuStrip1.Size = new System.Drawing.Size(137, 26);
                 // 
                 // _miProperties
                 // 
                 this._miProperties.Name = "_miProperties";
                 this._miProperties.Size = new System.Drawing.Size(136, 22);
                 this._miProperties.Text = "Properties...";
                 // 
                 // Calendar
                 // 
                 this.Controls.Add(this._btnRight);
                 this.Controls.Add(this._btnLeft);
                 this.Controls.Add(this._btnToday);
                 this.DoubleBuffered = true;
                 this.Name = "Calendar";
                 this.Size = new System.Drawing.Size(512, 440);
                 this.Load += new System.EventHandler(this.CalendarLoad);
                 this.Paint += new System.Windows.Forms.PaintEventHandler(this.CalendarPaint);
                 this.Resize += new System.EventHandler(this.CalendarResize);
                 this._contextMenuStrip1.ResumeLayout(false);
                 this.ResumeLayout(false);

             }
             private void CalendarLoad(object sender, EventArgs e)
             {
                 if (Parent != null)
                     Parent.Resize += ParentResize;
                 ResizeScrollPanel();
             }

             private void CalendarPaint(object sender, PaintEventArgs e)
             {
                 if (_showingToolTip)
                     return;

                 if (_calendarView == CalendarViews.Month)
                     RenderMonthCalendar(e);
                 if (_calendarView == CalendarViews.Day)
                     RenderDayCalendar(e);
             }
 
            public void BtnTodayButtonClicked(object sender, EventArgs e)
            {
                _calendarDate = DateTime.Now;
                Refresh();
            }

            public void BtnLeftButtonClicked(object sender, EventArgs e)
            {
                if (_calendarView == CalendarViews.Month)
                    _calendarDate = _calendarDate.AddMonths(-1);
                else if (_calendarView == CalendarViews.Day)
                    _calendarDate = _calendarDate.AddDays(-1);
                Refresh();
            }

            public void BtnRightButtonClicked(object sender, EventArgs e)
            {
                if (_calendarView == CalendarViews.Day)
                    _calendarDate = _calendarDate.AddDays(1);
                else if (_calendarView == CalendarViews.Month)
                    _calendarDate = _calendarDate.AddMonths(1);
                Refresh();
            }

            private void ParentResize(object sender, EventArgs e)
            {
                ResizeScrollPanel();
                Refresh();
            }

            private DateTime LastDayOfWeekInMonth(DateTime day, DayOfWeek dow)
            {
                DateTime lastDay = new DateTime(day.Year, day.Month, 1).AddMonths(1).AddDays(-1);
                DayOfWeek lastDow = lastDay.DayOfWeek;

                int diff = dow - lastDow;

                if (diff > 0) diff -= 7;

                System.Diagnostics.Debug.Assert(diff <= 0);

                return lastDay.AddDays(diff);
            }

            private int Max(params float[] value)
            {
                return (int)value.Max(i => Math.Ceiling(i));
            }


            private void ResizeScrollPanel()
            {
                int controlsSpacing = ((!_showTodayButton) && (!_showDateInHeader) && (!_showArrowControls)) ? 0 : 30;
            }

            private void RenderDayCalendar(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                if (_showDateInHeader)
                {
                    SizeF dateHeaderSize = g.MeasureString(
                        _calendarDate.ToString("MMMM") + " " + _calendarDate.Day.ToString(CultureInfo.InvariantCulture) +
                        ", " + _calendarDate.Year.ToString(CultureInfo.InvariantCulture), DateHeaderFont);

                    g.DrawString(
                        _calendarDate.ToString("MMMM") + " " + _calendarDate.Day.ToString(CultureInfo.InvariantCulture) +
                        ", " + _calendarDate.Year.ToString(CultureInfo.InvariantCulture),
                        _dateHeaderFont, Brushes.Black, ClientSize.Width - MarginSize - dateHeaderSize.Width,
                        MarginSize);
                }
            }

            private void RenderMonthCalendar(PaintEventArgs e)
            {
                _calendarDays.Clear();
                var bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
                Graphics g = Graphics.FromImage(bmp);
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                SizeF sunSize = g.MeasureString("Sun", _dayOfWeekFont);
                SizeF monSize = g.MeasureString("Mon", _dayOfWeekFont);
                SizeF tueSize = g.MeasureString("Tue", _dayOfWeekFont);
                SizeF wedSize = g.MeasureString("Wed", _dayOfWeekFont);
                SizeF thuSize = g.MeasureString("Thu", _dayOfWeekFont);
                SizeF friSize = g.MeasureString("Fri", _dayOfWeekFont);
                SizeF satSize = g.MeasureString("Sat", _dayOfWeekFont);
                SizeF dateHeaderSize = g.MeasureString(
                    _calendarDate.ToString("MMMM") + " " + _calendarDate.Year.ToString(CultureInfo.InvariantCulture), _dateHeaderFont);
                int headerSpacing = Max(sunSize.Height, monSize.Height, tueSize.Height, wedSize.Height, thuSize.Height, friSize.Height,
                              satSize.Height) + 5;
                int controlsSpacing = ((!_showTodayButton) && (!_showDateInHeader) && (!_showArrowControls)) ? 0 : 30;
                int cellWidth = (ClientSize.Width - MarginSize * 2) / 7;
                int numWeeks = NumberOfWeeks(_calendarDate.Year, _calendarDate.Month);
                int cellHeight = (ClientSize.Height - MarginSize * 2 - headerSpacing - controlsSpacing) / numWeeks;
                int xStart = MarginSize;
                int yStart = MarginSize;
                DayOfWeek startWeekEnum = new DateTime(_calendarDate.Year, _calendarDate.Month, 1).DayOfWeek;
                int startWeek = ((int)startWeekEnum) + 1;
                int rogueDays = startWeek - 1;

                yStart += headerSpacing + controlsSpacing;

                int counter = 1;
                int counter2 = 1;

                bool first = false;
                bool first2 = false;

                _btnToday.Location = new Point(MarginSize, MarginSize);

                for (int y = 0; y < numWeeks; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        if (rogueDays == 0 && counter <= DateTime.DaysInMonth(_calendarDate.Year, _calendarDate.Month))
                        {
                            if (!_calendarDays.ContainsKey(counter))
                                _calendarDays.Add(counter, new Point(xStart, (int)(yStart + 2f + g.MeasureString(counter.ToString(CultureInfo.InvariantCulture), _daysFont).Height)));

                            if (_calendarDate.Year == DateTime.Now.Year && _calendarDate.Month == DateTime.Now.Month
                             && counter == DateTime.Now.Day && _highlightCurrentDay)
                            {
                                g.FillRectangle(new SolidBrush(Color.FromArgb(234, 234, 234)), xStart, yStart, cellWidth, cellHeight);
                            }

                            if (first == false)
                            {
                                first = true;
                                if (_calendarDate.Year == DateTime.Now.Year && _calendarDate.Month == DateTime.Now.Month
                             && counter == DateTime.Now.Day)
                                {
                                    g.DrawString(
                                        _calendarDate.ToString("MMM") + " " + counter.ToString(CultureInfo.InvariantCulture),
                                        _todayFont, Brushes.Black, xStart + 5, yStart + 2);
                                }
                                else
                                {
                                    g.DrawString(
                                        _calendarDate.ToString("MMM") + " " + counter.ToString(CultureInfo.InvariantCulture),
                                        _daysFont, Brushes.Black, xStart + 5, yStart + 2);
                                }
                            }
                            else
                            {
                                if (_calendarDate.Year == DateTime.Now.Year && _calendarDate.Month == DateTime.Now.Month
                             && counter == DateTime.Now.Day)
                                {
                                    g.DrawString(counter.ToString(CultureInfo.InvariantCulture), _todayFont, Brushes.Black, xStart + 5, yStart + 2);
                                }
                                else
                                {
                                    g.DrawString(counter.ToString(CultureInfo.InvariantCulture), _daysFont, Brushes.Black, xStart + 5, yStart + 2);
                                }
                            }
                            counter++;
                        }
                        else if (rogueDays > 0)
                        {
                            int dm =
                                DateTime.DaysInMonth(_calendarDate.AddMonths(-1).Year, _calendarDate.AddMonths(-1).Month) -
                                rogueDays + 1;
                            g.DrawString(dm.ToString(CultureInfo.InvariantCulture), _daysFont, new SolidBrush(Color.FromArgb(170, 170, 170)), xStart + 5, yStart + 2);
                            rogueDays--;
                        }

                        g.DrawRectangle(Pens.DarkGray, xStart, yStart, cellWidth, cellHeight);
                        if (rogueDays == 0 && counter > DateTime.DaysInMonth(_calendarDate.Year, _calendarDate.Month))
                        {
                            if (first2 == false)
                                first2 = true;
                            else
                            {
                                if (counter2 == 1)
                                {
                                    g.DrawString(_calendarDate.AddMonths(1).ToString("MMM") + " " + counter2.ToString(CultureInfo.InvariantCulture), _daysFont,
                                                 new SolidBrush(Color.FromArgb(170, 170, 170)), xStart + 5, yStart + 2);
                                }
                                else
                                {
                                    g.DrawString(counter2.ToString(CultureInfo.InvariantCulture), _daysFont,
                                                 new SolidBrush(Color.FromArgb(170, 170, 170)), xStart + 5, yStart + 2);
                                }
                                counter2++;
                            }
                        }
                        xStart += cellWidth;
                    }
                    xStart = MarginSize;
                    yStart += cellHeight;
                }
                xStart = MarginSize + ((cellWidth - (int)sunSize.Width) / 2);
                yStart = MarginSize + controlsSpacing;

                g.DrawString("Pon", _dayOfWeekFont, Brushes.Black, xStart, yStart);
                xStart = MarginSize + ((cellWidth - (int)monSize.Width) / 2) + cellWidth;
                g.DrawString("Wto", _dayOfWeekFont, Brushes.Black, xStart, yStart);

                xStart = MarginSize + ((cellWidth - (int)tueSize.Width) / 2) + cellWidth * 2;
                g.DrawString("Śro", _dayOfWeekFont, Brushes.Black, xStart, yStart);

                xStart = MarginSize + ((cellWidth - (int)wedSize.Width) / 2) + cellWidth * 3;
                g.DrawString("Czw", _dayOfWeekFont, Brushes.Black, xStart, yStart);

                xStart = MarginSize + ((cellWidth - (int)thuSize.Width) / 2) + cellWidth * 4;
                g.DrawString("Pią", _dayOfWeekFont, Brushes.Black, xStart, yStart);

                xStart = MarginSize + ((cellWidth - (int)friSize.Width) / 2) + cellWidth * 5;
                g.DrawString("Sob", _dayOfWeekFont, Brushes.Black, xStart, yStart);

                xStart = MarginSize + ((cellWidth - (int)satSize.Width) / 2) + cellWidth * 6;
                g.DrawString("Nie", _dayOfWeekFont, Brushes.Black, xStart, yStart);

                if (_showDateInHeader)
                {
                    g.DrawString(
                        _calendarDate.ToString("MMMM") + " " + _calendarDate.Year.ToString(CultureInfo.InvariantCulture),
                        _dateHeaderFont, Brushes.Black, ClientSize.Width - MarginSize - dateHeaderSize.Width,
                        MarginSize);
                }

               _rectangles.Clear();

                g.Dispose();
                e.Graphics.DrawImage(bmp, 0, 0, ClientSize.Width, ClientSize.Height);
                bmp.Dispose();
            } 

            private int NumberOfWeeks(int year, int month)
            {
                return NumberOfWeeks(new DateTime(year, month, DateTime.DaysInMonth(year, month)));
            }

            private int NumberOfWeeks(DateTime date)
            {
                var beginningOfMonth = new DateTime(date.Year, date.Month, 1);

                while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                    date = date.AddDays(1);

                return (int)Math.Truncate(date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
            }

            private void CalendarResize(object sender, EventArgs e)
            {
                if (_calendarView == CalendarViews.Day)
                    ResizeScrollPanel();
            }


        }

    }

