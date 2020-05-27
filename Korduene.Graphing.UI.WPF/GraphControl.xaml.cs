using Korduene.Graphing.Enums;
using Korduene.UI;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Korduene.Graphing.UI.WPF
{
    /// <summary>
    /// Interaction logic for GraphControl.xaml
    /// </summary>
    public partial class GraphControl : Canvas, IGraphControl
    {
        private bool _isLeftMouseDown;
        private GraphPoint _mouseDownPosition;
        private GraphPoint _mousePosition;
        //private GraphState _state;
        private bool _isLeftMouseDownOnElement;
        private GraphPoint _startPosition;
        private IGraphMember _mouseDownMember;
        private bool _shouldDrawSelection;
        private bool _initialLoad;
        private bool _graphEventsInit;

        public GraphPoint MouseDownPosition
        {
            get { return _mouseDownPosition; }
            set
            {
                if (_mouseDownPosition != value)
                {
                    _mouseDownPosition = value;
                }
            }
        }

        public GraphPoint MousePosition
        {
            get { return _mousePosition; }
            set
            {
                if (_mousePosition != value)
                {
                    _mousePosition = value;
                }
            }
        }

        public GraphPoint Offset
        {
            get { return new GraphPoint(Surface.Offset.X, Surface.Offset.Y); }
            set
            {
                Surface.Offset = new Point(value.X, value.Y);
                KeepGraphInBound();
                Graph.Offset = Surface.Offset.ToGraphPoint();
            }
        }

        public double Zoom
        {
            get { return Surface.Scale; }
            set
            {
                if (value == 0)
                {
                    value = 1;
                }
                Surface.Scale = value;
                KeepGraphInBound();
                Graph.Zoom = Surface.Scale;
            }
        }

        public GraphPoint ViewCenter
        {
            get
            {
                double x = (Offset.X + (this.ActualWidth / 2)) / Zoom;
                double y = (Offset.Y + (this.ActualHeight / 2)) / Zoom;

                return new GraphPoint(x, y);
            }
        }

        public GraphPoint GraphCenter
        {
            get
            {
                double x = this.ActualWidth / 2;
                double y = this.ActualHeight / 2;
                var newOffset = new System.Windows.Vector(x, y); // * Zoom;
                return new GraphPoint(newOffset.X, newOffset.Y);
            }
        }

        public GraphContextMenu PopupMenu { get; set; }

        private IGraph Graph
        {
            get
            {
                var graph = this.DataContext as IGraph;

                if (!_graphEventsInit)
                {
                    if (graph != null)
                    {
                        graph.PropertyChanged += Graph_PropertyChanged;
                        _graphEventsInit = true;
                    }
                }

                return graph;
            }
        }

        //public IGraph Graph
        //{
        //    get { return (IGraph)GetValue(GraphProperty); }
        //    set { SetValue(GraphProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Graph.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty GraphProperty =
        //    DependencyProperty.Register("Graph", typeof(IGraph), typeof(GraphControl), new PropertyMetadata(null));

        public GraphControl()
        {
            InitializeComponent();
            this.Background = new SolidColorBrush(Color.FromArgb(250, 64, 64, 64));
            this.Loaded += GraphControl_Loaded;
            //this.PopupMenu = new GraphContextMenu(new GraphContextMenuProvider(Graph));
        }

        #region [Public Methods]

        public void CenterGraph()
        {
            CenterGraph(1.2);
        }

        public void CenterGraph(double zoom)
        {
            Offset = GraphCenter;
            Zoom = zoom;
        }

        public T CenterNode<T>(T node) where T : INode
        {
            var center = new GraphPoint(ViewCenter.X - (node.Width / 2), ViewCenter.Y - (node.Height / 2));
            node.Location = center;

            return node;
        }

        #endregion

        #region [Private Methods]

        private void GraphControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_initialLoad)
            {
                _initialLoad = true;
                Offset = GraphCenter;
            }

            this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            if (Graph.StartupOffset != GraphPoint.Empty)
            {
                Offset = Graph.StartupOffset;
                Zoom = Graph.Zoom;
            }
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            PopupMenu.IsOpen = false;

            if (e.ChangedButton == MouseButton.Left)
            {
                return;
            }

            if (e.OriginalSource is FrameworkElement graphElement && (graphElement.DataContext is IGraph graph || graphElement.DataContext is KDocumentBase kdoc))
            {
                if (Graph.MousePosition == Graph.LastMouseDownPosition)
                {
                    PopupMenu.IsOpen = true;
                }
            }

            if (e.OriginalSource is FrameworkElement nodeElement && nodeElement.DataContext is INode node)
            {
                if (Graph.MousePosition == Graph.LastMouseDownPosition)
                {
                    //TODO: show node menu
                    return;
                }
            }

            base.OnPreviewMouseUp(e);
        }

        private void DrawSelection()
        {
            SelectionBox.Show();

            //this.ReleaseMouseCapture();

            foreach (var node in Graph.Members.Where(x => x.MemberType == Enums.MemberType.Node))
            {
                var x = (node.Location.X * Zoom) - Offset.X;
                var y = (node.Location.Y * Zoom) - Offset.Y;
                var w = Zoom * node.Width;
                var h = Zoom * node.Height;

                node.IsSelected = SelectionBox.IntersectsWith(new Rect(x, y, w, h));
            }

            SelectionBox.CalculateVisuals();
        }

        private void ClearSelection(IGraphMember excludedMember = null)
        {
            if (excludedMember != null)
            {
                Graph.Members.ToList().Where(x => x != excludedMember).ToList().ForEach(x => x.IsSelected = false);
            }
            else
            {
                Graph.Members.ToList().ForEach(x => x.IsSelected = false);
            }
        }

        private void SetLastPositions()
        {
            foreach (var member in Graph.Members)
            {
                member.LastLocation = new GraphPoint(member.Location.X, member.Location.Y);
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            _mouseDownPosition = e.GetPosition(this.gcontrol).ToGraphPoint();
            Graph.LastMouseDownPosition = new GraphPoint(_mouseDownPosition.X, _mouseDownPosition.Y);
            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            //_mouseDownPosition = e.GetPosition(this.gcontrol);
            SelectionBox.MouseDownPosition = e.GetPosition(this);

            if (e.OriginalSource is FrameworkElement memberElement && memberElement.DataContext is IGraphMember member)
            {
                _mouseDownMember = member;
                _isLeftMouseDownOnElement = true;
                _startPosition = _mouseDownPosition;
                SetLastPositions();
            }
            //else if (e.OriginalSource is FrameworkElement portElement && portElement.DataContext is IPort port)
            //{
            //    Graph.MouseDownPort = port;
            //}
            else
            {
                _isLeftMouseDownOnElement = false;
                _mouseDownMember = null;
                ClearSelection();
                gcontrol.Focus();
            }

            if (e.OriginalSource is Canvas)
            {
                _shouldDrawSelection = true;
            }
            else
            {
                _shouldDrawSelection = false;
            }

            _isLeftMouseDown = true;

            //Debug.WriteLine($"mouse down: {_mouseDownPosition}");
            //this.CaptureMouse();

            base.OnPreviewMouseLeftButtonDown(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            _mousePosition = e.GetPosition(this.gcontrol).ToGraphPoint();
            SelectionBox.MousePosition = e.GetPosition(this);

            Graph.MousePosition = new GraphPoint(_mousePosition.X, _mousePosition.Y);

            if (e.MouseDevice.RightButton == MouseButtonState.Pressed)
            {
                PopupMenu.IsOpen = false;

                if (Graph.MousePosition != Graph.LastMouseDownPosition)
                {
                    Offset -= _mousePosition - _mouseDownPosition;
                }
            }

            if (!_isLeftMouseDown)
            {
                return;
            }

            //Debug.WriteLine($"mouse: {_mousePosition}");

            if (_isLeftMouseDownOnElement)
            {
                var delta = _mousePosition - _startPosition;
                _startPosition = _mousePosition;

                if (_mouseDownPosition != _mousePosition)
                {
                    if (Keyboard.Modifiers == ModifierKeys.Control || Keyboard.Modifiers == ModifierKeys.Shift)
                    {
                        _mouseDownMember.IsSelected = true;
                    }
                    else
                    {
                        _mouseDownMember.IsSelected = true;
                        ClearSelection(_mouseDownMember);
                    }
                }

                foreach (var member in Graph.Members.Where(x => x.IsSelected))
                {
                    member.Location = new GraphPoint(member.Location.X + delta.X, member.Location.Y + delta.Y);
                }
            }
            else if (Graph.MouseDownPort != null)
            {
                if (Graph.LinkingLink == null)
                {
                    Graph.LinkingLink = new Link() { ParentGraph = Graph, StartPort = Graph.MouseDownPort, Color = Graph.MouseDownPort.BorderColor };
                    Graph.Members.Add(Graph.LinkingLink);
                }
                else
                {
                    Graph.LinkingLink.UpdateVisuals();
                }
            }
            else
            {
                if (_shouldDrawSelection)
                {
                    DrawSelection();
                }
            }

            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            Graph.MouseUpPort = null;

            this.ReleaseMouseCapture();

            var curPos = e.GetPosition(this.gcontrol).ToGraphPoint();

            if (_mouseDownMember != null)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control || Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    if (_mouseDownPosition == curPos)
                    {
                        _mouseDownMember.IsSelected = !_mouseDownMember.IsSelected;
                    }
                }
                else
                {
                    if (_mouseDownPosition == curPos)
                    {
                        ClearSelection();
                        _mouseDownMember.IsSelected = !_mouseDownMember.IsSelected;
                    }
                    else
                    {
                        _mouseDownMember.IsSelected = true;
                    }
                }
            }

            if (_isLeftMouseDownOnElement)
            {
                if (curPos != _mouseDownPosition)
                {
                    Graph.UndoEngine.AddUndoUnit(new Common.UndoUnits.MemberMoveUndoUnit("Node Move", Graph.Members.Where(x => x.MemberType == MemberType.Node && x.IsSelected)));
                }
            }

            if (Mouse.DirectlyOver != null && Mouse.DirectlyOver is FrameworkElement fe && fe.DataContext is IPort port && port != Graph.MouseDownPort)
            {
                Graph.MouseUpPort = port;
            }

            if (Graph.MouseDownPort != null && Graph.MouseUpPort != null)
            {
                Graph.CreateConnection();
            }
            else if (Graph.LinkingLink != null)
            {
                Graph.DestroyCurrentLink();
            }

            Graph.MouseDownPort = null;
            _mouseDownMember = null;
            SelectionBox.Hide();
            _isLeftMouseDownOnElement = false;
            _isLeftMouseDown = false;

            base.OnPreviewMouseLeftButtonUp(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _isLeftMouseDown = false;
                _isLeftMouseDownOnElement = false;
                SelectionBox.Hide();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            //Graph.Zoom(e.Delta, e.MouseDevice.GetPosition(Graph));
            //if (e.Source is CustomControls.ComboBox || e.Source is TreeView || e.Source is TreeViewItem || e.Source is TextBox)
            //{
            //    return;
            //}

            if (e.Delta > 0)
            {
                if (Zoom < 3.0) { ZoomInOut(e); }
            }
            else
            {
                if (Zoom > 0.3) { ZoomInOut(e); }
            }

            KeepGraphInBound();

            base.OnPreviewMouseWheel(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var selectedLinks = Graph.Members.OfType<ILink>().Where(x => x.IsSelected).ToList();
                var selectedNodes = Graph.Members.OfType<INode>().Where(x => x.IsSelected).ToList();

                foreach (var link in selectedLinks)
                {
                    link.Destroy();
                }

                foreach (var node in selectedNodes)
                {
                    node.Destroy();
                }
            }

            base.OnKeyUp(e);
        }

        private void ZoomInOut(MouseWheelEventArgs e)
        {
            var x = Math.Pow(2, e.Delta / 3.0 / System.Windows.Input.Mouse.MouseWheelDeltaForOneLine);
            Zoom *= x;
            var position = (Vector)e.GetPosition(this);
            Offset = ((Point)((Vector)(Offset.ToPoint() + position) * x - position)).ToGraphPoint();
        }

        private void KeepGraphInBound()
        {
            if (Offset.X <= 0)
            {
                Surface.Offset = new GraphPoint(0, Offset.Y).ToPoint();
            }
            if (Offset.Y <= 0)
            {
                Surface.Offset = new GraphPoint(Offset.X, 0).ToPoint();
            }

            //TODO: FIX KEEPING GRAPH IN-BOUND ON THE RIGHT
            if (Offset.X >= Surface.ActualWidth * Zoom - this.Width)
            {
                Surface.Offset = new GraphPoint(Surface.ActualWidth * Zoom - this.Width, Offset.Y).ToPoint();
            }
            if (Offset.Y >= Surface.ActualHeight * Zoom - this.Height)
            {
                Surface.Offset = new GraphPoint(Offset.X, Surface.ActualHeight * Zoom - this.Height).ToPoint();
            }
        }

        private void Graph_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IGraph.Offset) && Mouse.PrimaryDevice.RightButton != MouseButtonState.Pressed)
            {
                this.Offset = Graph.Offset;
            }
        }

        #endregion

    }
}
