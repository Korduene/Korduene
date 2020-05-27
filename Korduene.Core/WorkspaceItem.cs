using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Korduene
{
    /// <summary>
    /// Workspace Item
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Workspace Item")]
    [DisplayName("WorkspaceItem")]
    [DebuggerDisplay("WorkspaceItem {Name}")]
    public class WorkspaceItem : INotifyPropertyChanged
    {
        #region [Events]

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region [Private Objects]

        private WorkspaceItem _project;
        private WorkspaceItemType _type;
        private string _name;
        private string _path;
        private object _tag;
        private WorkspaceItemCollection _items;
        private bool _isSelected;
        private bool _isExpanded;

        #endregion

        #region [Public Properties]

        public WorkspaceItemType Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public WorkspaceItem Project
        {
            get { return _project; }
            set
            {
                if (_project != value)
                {
                    _project = value;
                    OnPropertyChanged();
                }
            }
        }

        public WorkspaceItemCollection Items
        {
            get { return _items ??= new WorkspaceItemCollection(); }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged();
                }
            }
        }

        public object Tag
        {
            get { return _tag; }
            set
            {
                if (_tag != value)
                {
                    _tag = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsExpanded
        {
            get
            {
                if (Type == WorkspaceItemType.Solution)
                {
                    return true;
                }

                return _isExpanded;
            }
            set
            {
                if (Type == WorkspaceItemType.Solution)
                {
                    value = true;
                    OnPropertyChanged();
                }

                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceItem"/> class.
        /// </summary>
        public WorkspaceItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceItem"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public WorkspaceItem(WorkspaceItemType type) : this()
        {
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceItem" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="project">The project.</param>
        /// <param name="name">The name.</param>
        public WorkspaceItem(WorkspaceItemType type, WorkspaceItem project, string name) : this(type)
        {
            this.Project = project;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceItem" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="project">The project.</param>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        public WorkspaceItem(WorkspaceItemType type, WorkspaceItem project, string name, string path) : this(type, project, name)
        {
            this.Path = path;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceItem" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="project">The project.</param>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        /// <param name="tag">The tag.</param>
        public WorkspaceItem(WorkspaceItemType type, WorkspaceItem project, string name, string path, object tag) : this(type, project, name, path)
        {
            this.Type = type;
            this.Tag = tag;
        }

        #endregion

        #region [Public Methods]

        public bool HasFile(string file)
        {
            return HasFile(this, file);
        }

        public IEnumerable<WorkspaceItem> GetSelected()
        {
            var selected = new List<WorkspaceItem>();

            GetSelected(this, selected);

            return selected;
        }

        #endregion

        #region [Private Methods]

        public bool HasFile(WorkspaceItem item, string path)
        {
            if (item.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            foreach (var sub in item.Items)
            {
                if (HasFile(sub, path))
                {
                    return true;
                }
            }

            return false;
        }

        public void GetSelected(WorkspaceItem item, List<WorkspaceItem> selected)
        {
            if (item.IsSelected)
            {
                selected.Add(item);
            }

            foreach (var sub in item.Items)
            {
                GetSelected(sub, selected);
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
}
