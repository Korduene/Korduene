using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Korduene.Graphing.Collections
{
    /// <summary>
    /// Collection of Ports
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Collection of Ports")]
    [DisplayName("PortCollection")]
    public class PortCollection : ObservableCollection<IPort>
    {
        #region [Events]

        public event EventHandler<PortChangedEventArgs> PortChanged;

        public event EventHandler<IPort> PortConnected;

        public event EventHandler<IPort> PortDisconnected;

        #endregion

        #region [Private Objects]
        private INode _parentNode;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets graph that this node belongs to.
        /// </summary>
        public INode ParentNode
        {
            get { return _parentNode; }
            set
            {
                if (_parentNode != value)
                {
                    _parentNode = value;

                    this.ToList().ForEach(x => x.ParentNode = value);

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ParentNode)));
                }
            }
        }

        /// <summary>
        /// Gets the row count.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.Count > 0 ? this.Max(x => x.RowIndex) + 1 : 0;
            }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="PortCollection"/> class.
        /// </summary>
        public PortCollection(INode parentNode)
        {
            _parentNode = parentNode;
        }

        #endregion

        #region [Public Methods]

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset || e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    e.OldItems.Cast<IPort>().ToList().ForEach(x =>
                    {
                        x.ParentNode = null;
                        x.PropertyChanged -= Port_PropertyChanged;
                    });
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    e.NewItems.Cast<IPort>().ToList().ForEach(x =>
                    {
                        x.ParentNode = _parentNode;
                        x.PropertyChanged -= Port_PropertyChanged;
                        x.PropertyChanged += Port_PropertyChanged;
                        x.ConnectedPorts.CollectionChanged -= ConnectedPorts_CollectionChanged;
                        x.ConnectedPorts.CollectionChanged += ConnectedPorts_CollectionChanged;
                    });
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Move)
            {
                if (e.OldItems != null)
                {
                    e.NewItems.Cast<IPort>().ToList().ForEach(x =>
                    {
                        x.OnPropertyChanged(nameof(IPort.Index));
                        x.OnPropertyChanged(nameof(IPort.RowIndex));
                    });
                }

                if (e.NewItems != null)
                {
                    e.NewItems.Cast<IPort>().ToList().ForEach(x =>
                    {
                        x.OnPropertyChanged(nameof(IPort.Index));
                        x.OnPropertyChanged(nameof(IPort.RowIndex));
                    });
                }
            }

            RaiseRowIndexChanged();

            base.OnCollectionChanged(e);
        }

        private void ConnectedPorts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                PortConnected?.Invoke(sender, sender as IPort);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                PortDisconnected?.Invoke(sender, sender as IPort);
            }
        }

        private void Port_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPortPropertyChanged(sender as IPort, e.PropertyName);
        }

        public void RaiseCollectionChanged()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void OnPortPropertyChanged(IPort port, string propertyName)
        {
            PortChanged?.Invoke(port, new PortChangedEventArgs(port, propertyName));
        }

        public void RaiseRowIndexChanged()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].OnPropertyChanged(nameof(IPort.Index));
                this[i].OnPropertyChanged(nameof(IPort.RowIndex));
            }
        }

        #endregion

        #region [Private Methods]

        #endregion
    }
}
