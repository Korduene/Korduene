using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Korduene.Graphing.UI.WPF
{
    /// <summary>
    /// Interaction logic for NodeContainer.xaml
    /// </summary>
    public partial class PortContainer : StackPanel
    {
        private double _nodeX = 0;
        private double _nodeY = 0;

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left), typeof(double), typeof(PortContainer), new PropertyMetadata(default(double), new PropertyChangedCallback(OnXChanged)));

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(nameof(Top), typeof(double), typeof(PortContainer), new PropertyMetadata(default(double), new PropertyChangedCallback(OnYChanged)));

        public PortContainer()
        {
            InitializeComponent();
            LayoutUpdated += PortContainer_LayoutUpdated;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (e.Source is Ellipse && DataContext is IPort port)
            {
                port.ParentNode.ParentGraph.MouseDownPort = port;
            }

            base.OnPreviewMouseLeftButtonDown(e);
        }

        private void PortContainer_LayoutUpdated(object sender, EventArgs e)
        {
            if (DataContext is IPort port)
            {
                var node = port.ParentNode;
                var nx = node.Location.X;
                var ny = node.Location.Y + 28;

                //if (nx == _nodeX && ny == _nodeY)
                //{
                //    return;
                //}

                _nodeX = nx;
                _nodeY = ny;

                if (port.PortType == Enums.PortType.In)
                {
                    var x = nx + (Constants.PORT_WIDTH_HEIGHT / 5);
                    var y = ny;

                    if (port.RowIndex > 0)
                    {
                        y = (this.ActualHeight + this.Margin.Top + this.Margin.Bottom) * Convert.ToDouble(port.RowIndex) + ny;
                    }
                    port.Location = new GraphPoint(x, y);
                }
                else
                {
                    var x = (nx + node.Width) - (Constants.PORT_WIDTH_HEIGHT / 5);
                    var y = ny;

                    if (port.RowIndex > 0)
                    {
                        y = (this.ActualHeight + this.Margin.Top + this.Margin.Bottom) * Convert.ToDouble(port.RowIndex) + ny;
                    }

                    port.Location = new GraphPoint(x, y);
                }
            }
        }

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var element = d as UIElement;

            //var parent = VisualTreeHelper.GetParent(element) as UIElement;

            //Canvas.SetLeft(element, (double)e.NewValue);
            //Canvas.SetLeft(parent, (double)e.NewValue);
        }

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var element = d as UIElement;

            //var parent = VisualTreeHelper.GetParent(element) as UIElement;

            //Canvas.SetTop(element, (double)e.NewValue);
            //Canvas.SetTop(parent, (double)e.NewValue);
        }
    }
}
