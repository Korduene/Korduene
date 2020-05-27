using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Korduene.Graphing
{
    /// <summary>
    /// GraphContextMenuProvider
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("GraphContextMenuProvider")]
    [DisplayName("GraphContextMenuProvider")]
    [DebuggerDisplay("GraphContextMenuProvider")]
    public class GraphContextMenuProvider : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private IGraph _graph;
        private bool _isSearching;
        private bool _isFiltered;
        private GraphMenuItemCollection _items;
        private GraphMenuItemCollection _flatItemsResult;
        private IGraphContextMenuControl _contextMenuControl;
        #endregion

        #region [Public Properties]

        public bool IsSearching
        {
            get { return _isSearching; }
            set
            {
                if (_isSearching != value)
                {
                    _isSearching = value;
                    OnPropertyChanged();
                }
            }
        }

        public GraphMenuItemCollection Items
        {
            get
            {
                if (_isFiltered)
                {
                    return _flatItemsResult;
                }

                return _items;
            }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        public GraphMenuItemCollection FlatItems { get; private set; }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphContextMenuProvider" /> class.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="contextMenuControl">The context menu control.</param>
        public GraphContextMenuProvider(IGraph graph, IGraphContextMenuControl contextMenuControl)
        {
            FlatItems = new GraphMenuItemCollection((GraphMenuItem)null);
            _items = new GraphMenuItemCollection((GraphMenuItem)null);
            _flatItemsResult = new GraphMenuItemCollection((GraphMenuItem)null);
            _graph = graph;
            _contextMenuControl = contextMenuControl;
        }

        #endregion

        #region [Public Methods]

        public async void LoadNodes()
        {
        }

        public void Search(string text)
        {
            Task.Factory.StartNew(() => IsSearching = true)
                .ContinueWith(a => SearchCore(text))
                .ContinueWith(b => IsSearching = false)
                .ContinueWith(c => _contextMenuControl.Invoke(() => OnPropertyChanged(nameof(Items))));
        }

        public virtual void AddNode(GraphMenuItem nodeTreeViewItem)
        {
            var node = Activator.CreateInstance(nodeTreeViewItem.NodeType) as INode;
            node.Location = new GraphPoint(_graph.LastMouseDownPosition.X, _graph.LastMouseDownPosition.Y);
            _graph.Members.Add(node);
        }

        #endregion

        #region [Private Methods]

        private void SearchCore(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || text?.Length < 2)
            {
                _isFiltered = false;
                //_items.ToList().ForEach(x => x.IsVisible = true);
                return;
            }

            _isFiltered = true;

            //foreach (var item in FlatItems)
            //{
            //    SearchItem(item, text);
            //}

            //var str = FlatItems.FirstOrDefault(x => x.DisplayName.Contains("string"));
            //var result = _flatItemsResult;

            //_flatItemsResult = new GraphMenuItemCollection(result.OrderBy(x => x.Name));
            var result = FlatItems.Where(x => x.SearchableContent.Contains(text));
            _flatItemsResult = new GraphMenuItemCollection(result.OrderBy(x => x.Name, new ClosestStringComparer(text, _flatItemsResult.Count())));
        }

        private void SearchItem(GraphMenuItem item, string text)
        {
            //if (item.SearchableContent.Contains(text))
            //{
            //    _flatItemsResult.Add(item);
            //}

            //if (item.Name.Contains(text, StringComparison.OrdinalIgnoreCase) || item.Items.Any(x => x.Name.Contains(text, StringComparison.OrdinalIgnoreCase)))
            //{
            //    _flatItemsResult.Add(item);
            //    //item.IsVisible = true;
            //}
            ////else
            ////{
            ////    item.IsVisible = false;
            ////}

            //foreach (var sub in item.Items)
            //{
            //    SearchItem(sub, text);
            //}
        }

        private void MakeAllVisible()
        {
            foreach (var item in this.Items)
            {
                item.MakeItemsVisible();
            }
        }

        #endregion

        /// <summary>
        /// Called when public properties changed.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class ClosestStringComparer : IComparer<string>
    {
        private string _term = string.Empty;
        private int _collectionSize = 0;
        private int _curr = 0;
        public ClosestStringComparer()
        {
        }

        public ClosestStringComparer(string term, int collectionSize)
        {
            _term = term;
            _collectionSize = collectionSize;
        }

        public int Compare(string x, string y)
        {
            if (!string.IsNullOrWhiteSpace(_term))
            {
                if (_term.Equals(x, StringComparison.OrdinalIgnoreCase))
                {
                    return _collectionSize * -1;
                }
                else if (x.StartsWith(_term, StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }

                return _curr++;
            }

            return _collectionSize;

            //var c = Compute(_term, x);
            //Debug.WriteLine(c);
            //return c;
        }

        private int Compute(string a, string b)
        {
            if (String.IsNullOrEmpty(a) && String.IsNullOrEmpty(b))
            {
                return 0;
            }
            if (String.IsNullOrEmpty(a))
            {
                return b.Length;
            }
            if (String.IsNullOrEmpty(b))
            {
                return a.Length;
            }
            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min
                        (
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                        );
                }
            return distances[lengthA, lengthB];

            //int n = s.Length;
            //int m = t.Length;
            //int[,] d = new int[n + 1, m + 1];

            //// Step 1
            //if (n == 0)
            //{
            //    return m;
            //}

            //if (m == 0)
            //{
            //    return n;
            //}

            //// Step 2
            //for (int i = 0; i <= n; d[i, 0] = i++)
            //{
            //}

            //for (int j = 0; j <= m; d[0, j] = j++)
            //{
            //}

            //// Step 3
            //for (int i = 1; i <= n; i++)
            //{
            //    //Step 4
            //    for (int j = 1; j <= m; j++)
            //    {
            //        // Step 5
            //        int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

            //        // Step 6
            //        d[i, j] = Math.Min(
            //            Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
            //            d[i - 1, j - 1] + cost);
            //    }
            //}
            //// Step 7
            //return d[n, m];
        }
    }
}
