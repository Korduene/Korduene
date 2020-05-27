using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Korduene
{
    public class WorkspaceItemCollection : ObservableCollection<WorkspaceItem>
    {
        public void AddItem(WorkspaceItem item)
        {
            if (this.Count < 1)
            {
                this.Add(item);
                return;
            }

            //TODO: do this properly
            var index = new List<WorkspaceItem>(this.Items) { item }.OrderBy(x => x.Type).ThenBy(x => x.Name).ToList().IndexOf(item);

            this.Insert(index, item);
        }
    }
}
