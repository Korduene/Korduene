using Korduene.Graphing.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Korduene.Graphing.UI.WPF
{
    /// <summary>
    /// Interaction logic for PortsContainer.xaml
    /// </summary>
    public partial class PortsContainer : Grid
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

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left), typeof(double), typeof(PortsContainer), new PropertyMetadata(default(double), new PropertyChangedCallback(OnXChanged)));

        public static readonly DependencyProperty TopProperty = DependencyProperty.Register(nameof(Top), typeof(double), typeof(PortsContainer), new PropertyMetadata(default(double), new PropertyChangedCallback(OnYChanged)));

        public PortCollection PortCollection
        {
            get { return GetValue(PortCollectionProperty) as PortCollection; }
            set { SetValue(PortCollectionProperty, value); }
        }

        public static readonly DependencyProperty PortCollectionProperty = DependencyProperty.Register(nameof(PortCollection), typeof(PortCollection), typeof(PortsContainer), new PropertyMetadata(default(PortCollection), new PropertyChangedCallback(OnPortsCollectionChanged)));

        private static void OnPortsCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as PortsContainer;
            var portCollection = e.NewValue as PortCollection;

            if (portCollection == null)
            {
                return;
            }

            portCollection.CollectionChanged -= PortCollection_CollectionChanged;
            portCollection.CollectionChanged += PortCollection_CollectionChanged;

            //if (grid.RowDefinitions.Count == 0 && portCollection.Count > 0)
            //{
            //    var toCreate = portCollection.RowCount;

            //    for (int i = 0; i < toCreate; i++)
            //    {
            //        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Star) });
            //    }
            //}

            void PortCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs ea)
            {
                UpdateGrid(grid, portCollection);
            }

            //portCollection.RaiseRowIndexChanged();

            //foreach (var item in grid.RowDefinitions)
            //{
            //    item.Height = new GridLength(0);
            //    item.Height = new GridLength(0, GridUnitType.Star);
            //}

            //grid.UpdateLayout();
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

        private void UpdatePortPos()
        {
            //var point = this.TranslatePoint(new Point(), VisualTreeHelper.GetParent(this) as UIElement);
            //var nx = Left;
            //var ny = Top;

            //foreach (var port in PortCollection)
            //{
            //    var node = port.ParentNode;

            //    if (port.PortType == Enums.PortType.IN)
            //    {
            //        port.X = nx - (Constants.PORT_WIDTH_HEIGHT / 2);
            //        if (port.RowIndex > 0)
            //        {
            //            port.Y = ny * port.RowIndex;
            //        }
            //        else
            //        {
            //            port.Y = ny;
            //        }
            //    }
            //    else
            //    {
            //        port.X = (nx + node.Width) + (Constants.PORT_WIDTH_HEIGHT / 2);
            //        if (port.RowIndex > 0)
            //        {
            //            port.Y = ny * port.RowIndex;
            //        }
            //        else
            //        {
            //            port.Y = ny;
            //        }
            //    }
            //}

        }

        public PortsContainer()
        {
            InitializeComponent();

            Loaded += PortsContainer_Loaded;
            //PortCollection.CollectionChanged += PortCollection_CollectionChanged;
        }

        private static void UpdateGrid(Grid grid, PortCollection portCollection)
        {
            if (portCollection == null)
            {
                return;
            }

            grid.RowDefinitions.Clear();

            for (int i = 0; i < portCollection.RowCount; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            }

            //grid.RowDefinitions.Clear();

            //if (grid.RowDefinitions.Count == 0 && portCollection.Count > 0)
            //{
            //    var toCreate = portCollection.RowCount;

            //    for (int i = 0; i < toCreate; i++)
            //    {
            //        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            //    }
            //}
            //else if (portCollection.RowCount > grid.RowDefinitions.Count)
            //{
            //    var toCreate = portCollection.RowCount - grid.RowDefinitions.Count;

            //    for (int i = 0; i < toCreate; i++)
            //    {
            //        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
            //    }
            //}
            //else if (portCollection.RowCount < grid.RowDefinitions.Count)
            //{
            //    var toDelete = portCollection.RowCount - grid.RowDefinitions.Count;

            //    for (int i = toDelete; i < 0; i++)
            //    {
            //        grid.RowDefinitions.RemoveAt(i);
            //    }
            //}

            //portCollection.RaiseRowIndexChanged();
            //grid.UpdateLayout();
        }

        private void PortsContainer_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGrid(this, PortCollection);
        }
    }
}
