using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace VolumeMaster_Windows
{
    public class CustomScrollBar : Control
    {
        private bool isVertical = true;
        private int thumbPosition = 0;
        private int largeChange = 10;
        private int smallChange = 1;
        private int minimum = 0;
        private int maximum = 100;
        private int value = 0;
        private bool isDragging = false;
        private int dragStartPosition = 0;
        private int thumbStartPosition = 0;
        private Rectangle thumbRect;

        // Colors - matching the app theme
        private Color thumbColor = Color.FromArgb(123, 104, 238); // Medium slate blue
        private Color thumbHoverColor = Color.FromArgb(143, 124, 255); // Lighter purple
        private Color thumbPressedColor = Color.FromArgb(103, 84, 218); // Darker purple
        private Color trackColor = Color.FromArgb(40, 42, 60); // Dark background
        private Color borderColor = Color.FromArgb(50, 53, 70); // Subtle border

        [DefaultValue(true)]
        [Browsable(true)]
        [Description("Determines if the scrollbar is vertical or horizontal")]
        public bool IsVertical
        {
            get { return isVertical; }
            set 
            { 
                isVertical = value;
                UpdateThumbRect();
                Invalidate();
            }
        }

        [Browsable(true)]
        [Description("The current position of the scrollbar")]
        public int Value
        {
            get { return value; }
            set
            {
                if (value < minimum)
                    this.value = minimum;
                else if (value > maximum)
                    this.value = maximum;
                else
                    this.value = value;

                // Calculate the thumb position based on the value
                UpdateThumbPosition();
                OnValueChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        [Browsable(true)]
        [Description("The minimum value of the scrollbar")]
        public int Minimum
        {
            get { return minimum; }
            set 
            { 
                minimum = value;
                if (this.value < minimum)
                    Value = minimum;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Description("The maximum value of the scrollbar")]
        public int Maximum
        {
            get { return maximum; }
            set 
            { 
                maximum = value;
                if (this.value > maximum)
                    Value = maximum;
                Invalidate();
            }
        }

        [DefaultValue(10)]
        [Browsable(true)]
        [Description("The large change value")]
        public int LargeChange
        {
            get { return largeChange; }
            set { largeChange = value; }
        }

        [DefaultValue(1)]
        [Browsable(true)]
        [Description("The small change value")]
        public int SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; }
        }

        [Browsable(true)]
        public event EventHandler? ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public CustomScrollBar()
        {
            SetStyle(ControlStyles.UserPaint | 
                    ControlStyles.AllPaintingInWmPaint | 
                    ControlStyles.OptimizedDoubleBuffer | 
                    ControlStyles.ResizeRedraw | 
                    ControlStyles.SupportsTransparentBackColor, true);
            
            this.BackColor = Color.Transparent;
            
            // Set default size
            if (isVertical)
                this.Size = new Size(18, 200);
            else
                this.Size = new Size(200, 18);

            UpdateThumbRect();
        }

        private void UpdateThumbPosition()
        {
            // Calculate the range in which the thumb can move
            int valueRange = maximum - minimum;
            if (valueRange <= 0) 
                valueRange = 1; // Prevent division by zero

            if (isVertical)
            {
                int trackHeight = Height - 2; // Account for borders
                int thumbHeight = Math.Max(20, trackHeight * largeChange / (valueRange + largeChange));
                int trackSpace = trackHeight - thumbHeight;
                
                if (trackSpace <= 0)
                    thumbPosition = 0;
                else
                    thumbPosition = (value - minimum) * trackSpace / valueRange;
            }
            else
            {
                int trackWidth = Width - 2; // Account for borders
                int thumbWidth = Math.Max(20, trackWidth * largeChange / (valueRange + largeChange));
                int trackSpace = trackWidth - thumbWidth;
                
                if (trackSpace <= 0)
                    thumbPosition = 0;
                else
                    thumbPosition = (value - minimum) * trackSpace / valueRange;
            }

            UpdateThumbRect();
        }

        private void UpdateThumbRect()
        {
            if (isVertical)
            {
                int thumbHeight = Math.Max(20, (Height - 2) * largeChange / (maximum - minimum + largeChange));
                thumbRect = new Rectangle(1, 1 + thumbPosition, Width - 2, thumbHeight);
            }
            else
            {
                int thumbWidth = Math.Max(20, (Width - 2) * largeChange / (maximum - minimum + largeChange));
                thumbRect = new Rectangle(1 + thumbPosition, 1, thumbWidth, Height - 2);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateThumbPosition();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw track
            using (SolidBrush trackBrush = new SolidBrush(trackColor))
            {
                FillRoundedRectangle(g, trackBrush, 0, 0, Width, Height, 8);
            }

            // Draw thumb
            Color currentThumbColor = isDragging ? thumbPressedColor : thumbColor;
            using (SolidBrush thumbBrush = new SolidBrush(currentThumbColor))
            {
                FillRoundedRectangle(g, thumbBrush, thumbRect, 6);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            
            if (thumbRect.Contains(e.Location))
            {
                isDragging = true;
                dragStartPosition = isVertical ? e.Y : e.X;
                thumbStartPosition = thumbPosition;
                Invalidate();
            }
            else
            {
                // Clicking on the track - move the thumb to that position
                if (isVertical)
                {
                    if (e.Y < thumbRect.Y)
                        Value -= largeChange;
                    else if (e.Y > thumbRect.Bottom)
                        Value += largeChange;
                }
                else
                {
                    if (e.X < thumbRect.X)
                        Value -= largeChange;
                    else if (e.X > thumbRect.Right)
                        Value += largeChange;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            
            if (isDragging)
            {
                if (isVertical)
                {
                    int delta = e.Y - dragStartPosition;
                    int trackHeight = Height - 2 - thumbRect.Height;
                    int newThumbPos = Math.Max(0, Math.Min(trackHeight, thumbStartPosition + delta));
                    
                    if (trackHeight > 0)
                    {
                        Value = minimum + (maximum - minimum) * newThumbPos / trackHeight;
                    }
                }
                else
                {
                    int delta = e.X - dragStartPosition;
                    int trackWidth = Width - 2 - thumbRect.Width;
                    int newThumbPos = Math.Max(0, Math.Min(trackWidth, thumbStartPosition + delta));
                    
                    if (trackWidth > 0)
                    {
                        Value = minimum + (maximum - minimum) * newThumbPos / trackWidth;
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isDragging = false;
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            
            // Scroll when using the mouse wheel
            int scrollLines = SystemInformation.MouseWheelScrollLines;
            
            if (e.Delta > 0)
                Value -= smallChange * scrollLines;
            else
                Value += smallChange * scrollLines;
        }
        
        // Helper method for drawing rounded rectangles
        private void FillRoundedRectangle(Graphics g, Brush brush, float x, float y, float width, float height, float radius)
        {
            RectangleF rect = new RectangleF(x, y, width, height);
            using (GraphicsPath path = GetRoundedRect(rect, radius))
            {
                g.FillPath(brush, path);
            }
        }
        
        private void FillRoundedRectangle(Graphics g, Brush brush, Rectangle rect, float radius)
        {
            FillRoundedRectangle(g, brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        }
        
        private GraphicsPath GetRoundedRect(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // Constrain corner radius based on the smallest dimension
            if (radius > (Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
                radius = (float)(Math.Min(baseRect.Width, baseRect.Height) / 2.0);

            GraphicsPath path = new GraphicsPath();
            path.AddArc(baseRect.X, baseRect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(baseRect.Right - radius * 2, baseRect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(baseRect.Right - radius * 2, baseRect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(baseRect.X, baseRect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
} 