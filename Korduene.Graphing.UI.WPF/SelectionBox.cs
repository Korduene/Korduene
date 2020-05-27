using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF
{
    /// <summary>
    /// Selection box, which is drawn on top of the grid control
    /// </summary>
    public class SelectionBox : Border, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double _x;
        private double _y;
        private Point _mouseDownPosition;
        private Point _mousePosition;

        public Point MouseDownPosition
        {
            get { return _mouseDownPosition; }
            set
            {
                if (_mouseDownPosition != value)
                {
                    _mouseDownPosition = value;
                    Position = value;
                    OnPropertyChanged();
                }
            }
        }

        public Point MousePosition
        {
            get { return _mousePosition; }
            set
            {
                if (_mousePosition != value)
                {
                    _mousePosition = value;
                    OnPropertyChanged();
                }
            }
        }

        public double X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    Canvas.SetLeft(this, _x);
                    OnPropertyChanged();
                }
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    Canvas.SetTop(this, _y);
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <value>
        /// The rectangle.
        /// </value>
        public Rect Rectangle
        {
            get { return new Rect(X, Y, Width, Height); }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Point Position
        {
            get { return new Point(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Checks to see if the provided rectangle intersects with the selection box.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns><c>true</c> if rectangle intersects with the selection box; otherwise, <c>false</c></returns>
        public bool IntersectsWith(Rect rectangle)
        {
            return Rectangle.IntersectsWith(rectangle);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionBox"/> class.
        /// </summary>
        public SelectionBox()
        {
            this.Width = 0;
            this.Height = 0;
            this.Background = new SolidColorBrush(Colors.BlueViolet) { Opacity = 0.1 };
            this.BorderBrush = new SolidColorBrush(Colors.DodgerBlue);
            this.BorderThickness = new Thickness(1);
            Canvas.SetZIndex(this, 99999999);
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            this.Visibility = Visibility.Collapsed;
        }

        public void CalculateVisuals()
        {
            if (MouseDownPosition.X < MousePosition.X)
            {
                X = MouseDownPosition.X;
                Width = MousePosition.X - MouseDownPosition.X;
            }
            else
            {
                X = MousePosition.X;
                Width = MouseDownPosition.X - MousePosition.X;
            }

            if (MouseDownPosition.Y < MousePosition.Y)
            {
                Y = MouseDownPosition.Y;
                Height = MousePosition.Y - MouseDownPosition.Y;
            }
            else
            {
                Y = MousePosition.Y;
                Height = MouseDownPosition.Y - MousePosition.Y;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
