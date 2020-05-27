using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Korduene.Graphing.Collections
{
    /// <summary>
    /// Collection of graph members
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Collection of graph members")]
    [DisplayName("GraphMemberCollection")]
    public class GraphMemberCollection : ObservableCollection<IGraphMember>
    {
        #region [Events]

        public event EventHandler<PropertyChangedEventArgs> MemberPropertyChanged;

        #endregion

        #region [Private Objects]
        private IGraph _parentGraph;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets graph that this node belongs to.
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
        /// Initializes a new instance of the <see cref="GraphMemberCollection"/> class.
        /// </summary>
        public GraphMemberCollection(IGraph parentGraph)
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
                    e.OldItems.Cast<IGraphMember>().ToList().ForEach(x =>
                    {
                        x.ParentGraph = null;
                        x.PropertyChanged -= Member_PropertyChanged;
                    });
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    e.NewItems.Cast<IGraphMember>().ToList().ForEach(x =>
                    {
                        AssignName(x);
                        x.PropertyChanged += Member_PropertyChanged;
                        x.ParentGraph = _parentGraph;
                        x.UpdateVisuals();
                    });

                    //var cur = this.OfType<INode>();

                    //if (cur.Any())
                    //{
                    //    foreach (var n in e.NewItems.OfType<INode>())
                    //    {
                    //        while (cur.Any(x => x != n && x.Overlaps(n)))
                    //        {
                    //            n.Location = new GraphPoint(n.Location.X + n.Width / 2, n.Location.Y + n.Height / 2);
                    //        }
                    //    }
                    //}
                }
            }

            base.OnCollectionChanged(e);
        }

        private void Member_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MemberPropertyChanged?.Invoke(sender, e);
        }

        #endregion

        #region [Private Methods]

        private void AssignName(IGraphMember member)
        {
            if (string.IsNullOrWhiteSpace(member.DefaultName) || !string.IsNullOrWhiteSpace(member.Name))
            {
                return;
            }

            var count = 0;
            var name = member.DefaultName;

            while (true)
            {
                count++;
                name = $"{member.DefaultName}{count}";

                if (!this.Any(x => x.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true))
                {
                    member.Name = name;
                    break;
                }
            }
        }

        #endregion
    }
}
