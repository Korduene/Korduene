using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Korduene.Graphing
{
    public class GraphMenuItemCollection : ObservableCollection<GraphMenuItem>
    {
        public GraphMenuItem Parent { get; }

        public GraphMenuItemCollection(GraphMenuItem parent)
        {
            this.Parent = parent;
        }

        public GraphMenuItemCollection(IEnumerable<GraphMenuItem> graphMenuItems)
        {
            foreach (var item in graphMenuItems)
            {
                Add(item);
            }
        }

        public void CacheSearchableContent()
        {
            foreach (var item in Items)
            {
                item.SearchableContent = $"{item.Name.ToLower()}, {string.Join(",", item.Items.Select(x => x.Name.ToLower()))}".Replace(", ", string.Empty);
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    AddParents(e.NewItems.Cast<GraphMenuItem>());
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems != null)
                {
                    RemoveParents(e.OldItems.Cast<GraphMenuItem>());
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems != null)
                {
                    AddParents(e.NewItems.Cast<GraphMenuItem>());
                }
                if (e.OldItems != null)
                {
                    RemoveParents(e.OldItems.Cast<GraphMenuItem>());
                }
            }

            //base.OnCollectionChanged(e);
        }

        private void AddParents(IEnumerable<GraphMenuItem> graphMenuItems)
        {
            foreach (var item in graphMenuItems)
            {
                item.Parent = Parent;
            }
        }

        private void RemoveParents(IEnumerable<GraphMenuItem> graphMenuItems)
        {
            foreach (var item in graphMenuItems)
            {
                item.Parent = null;
            }
        }
    }
}
