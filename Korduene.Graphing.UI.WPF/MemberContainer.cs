using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF
{
    public partial class MemberContainer : Grid
    {
        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public double Top
        {
            set { SetValue(TopProperty, value); }
            get { return (double)GetValue(TopProperty); }
        }

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left), typeof(double), typeof(MemberContainer), new PropertyMetadata(default(double), new PropertyChangedCallback(OnXChanged)));

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(nameof(Top), typeof(double), typeof(MemberContainer), new PropertyMetadata(default(double), new PropertyChangedCallback(OnYChanged)));

        public MemberContainer()
        {
            LayoutUpdated += MemberContainer_LayoutUpdated;
            DataContextChanged += MemberContainer_DataContextChanged;
        }

        private void MemberContainer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is IGraphMember member)
            {
                member.VisualContainer = this;
            }
        }

        private void MemberContainer_LayoutUpdated(object sender, EventArgs e)
        {
            if (DataContext is IGraphMember member)
            {
                member.Width = this.ActualWidth;
                member.Height = this.ActualHeight;
            }
        }

        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;

            Canvas.SetLeft(element, (double)e.NewValue);

            if (VisualTreeHelper.GetParent(element) is ContentPresenter parent)
            {
                Canvas.SetLeft(parent, (double)e.NewValue);
            }
        }

        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;

            Canvas.SetTop(element, (double)e.NewValue);

            if (VisualTreeHelper.GetParent(element) is ContentPresenter parent)
            {
                Canvas.SetTop(parent, (double)e.NewValue);
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "ZIndex")
            {
                if (VisualTreeHelper.GetParent(this) is ContentPresenter parent)
                {
                    SetZIndex(parent, (int)e.NewValue);
                }
            }

            base.OnPropertyChanged(e);
        }
    }
}
