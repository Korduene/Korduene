using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Korduene.Graphing.Collections
{
    /// <summary>
    /// Collection of nodes
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Collection of nodes")]
    [DisplayName("NodeCollection")]
    public class NodeCollection : ObservableCollection<INode>
    {
        #region [Events]

        #endregion

        #region [Private Objects]
        private IGraph _parentGraph;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets graph that this node belongs to.
        /// </summary>
        public IGraph ParentGraph
        {
            get { return _parentGraph; }
            set
            {
                if (_parentGraph != value)
                {
                    _parentGraph = value;

                    this.ToList().ForEach(x => x.ParentGraph = value);

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ParentGraph)));
                }
            }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeCollection"/> class.
        /// </summary>
        public NodeCollection(IGraph parentGraph)
        {
            _parentGraph = parentGraph;
        }

        #endregion

        #region [Public Methods]

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset || e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    e.NewItems.Cast<INode>().ToList().ForEach(x => x.ParentGraph = null);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    e.NewItems.Cast<INode>().ToList().ForEach(x => x.ParentGraph = _parentGraph);
                }
            }

            base.OnCollectionChanged(e);
        }

        #endregion

        #region [Private Methods]

        #endregion
    }
}
