using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Xaml;

namespace Korduene.UI.WPF.ToolWindows.ViewModels
{
    /// <summary>
    /// ToolBox ViewModel
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("ToolBox ViewModel")]
    [DisplayName("ToolBoxViewModel")]
    [DebuggerDisplay("{Name}")]
    public class ToolBoxViewModel : KToolWindow
    {
        #region [Events]

        #endregion

        #region [Private Objects]

        private Assembly[] _wpfAssemblies = { typeof(Point).Assembly, typeof(IAddChild).Assembly, typeof(System.Windows.Markup.XamlReader).Assembly, typeof(XamlType).Assembly, typeof(Type).Assembly };

        private IEnumerable<Type> _wpfTypes = new Type[]
        {
            typeof(Border),
            typeof(Button),
            typeof(Calendar),
            typeof(Canvas),
            typeof(CheckBox),
            typeof(ComboBox),
            typeof(ComboBoxItem),
            typeof(ContentControl),
            typeof(DataGrid),
            typeof(DatePicker),
            typeof(DockPanel),
            typeof(DocumentViewer),
            typeof(Ellipse),
            typeof(Expander),
            typeof(Frame),
            typeof(Grid),
            typeof(GridSplitter),
            typeof(GroupBox),
            typeof(Image),
            typeof(Label),
            typeof(ListBox),
            typeof(ListView),
            typeof(MediaElement),
            typeof(Menu),
            typeof(PasswordBox),
            typeof(ProgressBar),
            typeof(RadioButton),
            typeof(Rectangle),
            typeof(RichTextBox),
            typeof(ScrollViewer),
            typeof(Separator),
            typeof(Slider),
            typeof(StackPanel),
            typeof(StatusBar),
            typeof(TabControl),
            typeof(TextBlock),
            typeof(TextBox),
            typeof(ToggleButton),
            typeof(ToolBar),
            typeof(ToolBarPanel),
            typeof(ToolBarTray),
            typeof(ToolTip),
            typeof(TreeView),
            typeof(Viewbox),
            //typeof(Viewport3D),
            typeof(WebBrowser),
            typeof(WrapPanel)
        };

        #endregion

        #region [Public Properties]

        public ObservableCollection<ToolboxItemCategory> Items { get; }

        #endregion

        #region [Commands]

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolBoxViewModel"/> class.
        /// </summary>
        public ToolBoxViewModel()
        {
            this.ContentId = Constants.ContentIds.TOOLBOX;
            this.Name = "Toolbox";
            this.Items = new ObservableCollection<ToolboxItemCategory>();

            //_wpfTypes = _wpfAssemblies.SelectMany(x => x.GetExportedTypes()).Where(x => !x.IsAbstract && !x.IsGenericTypeDefinition && x.IsSubclassOf(typeof(UIElement)) && x.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null) != null).OrderBy(x => x.Name);

            Current.Instance.SolutionLoaded += Instance_SolutionLoaded;
            Current.Instance.BuildFinished += Instance_BuildFinished;
            Current.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Current.ActiveDocument))
            {
                RefreshVisibleItems();
            }
        }

        #endregion

        #region [Command Handlers]

        #endregion

        #region [Public Methods]

        public override void OnLoaded()
        {
            RefreshItems();
            base.OnLoaded();
        }

        public void RefreshItems()
        {
            if (Current.Instance.Workspace == null)
            {
                return;
            }

            //custom items
            foreach (var item in Current.Instance.Workspace.TargetPaths)
            {
                if (!File.Exists(item))
                {
                    continue;
                }

                var assembly = Assembly.Load(System.IO.File.ReadAllBytes(item));
                var types = assembly.GetExportedTypes();

                var wpfTypes = types.Where(x => !x.IsAbstract && !x.IsGenericTypeDefinition && x.IsSubclassOf(typeof(UIElement)) && x.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null) != null && x.BaseType != typeof(Window));
                var otherTypes = types.Where(x => typeof(IKToolboxItem).IsAssignableFrom(x));

                var catName = System.IO.Path.GetFileNameWithoutExtension(item);
                var customCat = this.Items.FirstOrDefault(x => x.Name == catName && x.DocumentTypeId == Korduene.Constants.WPF_DESIGNER);

                if (customCat == null)
                {
                    customCat = new ToolboxItemCategory(catName, Korduene.Constants.WPF_DESIGNER);
                    this.Items.Add(customCat);
                }

                foreach (var type in wpfTypes)
                {
                    if (!customCat.Items.Any(x => x.Type == type && x.Category == catName && x.DocumentTypeId == Korduene.Constants.WPF_DESIGNER))
                    {
                        customCat.Items.Add(new KToolboxItem(type.Name, type, catName, Korduene.Constants.WPF_DESIGNER));
                    }
                }

                foreach (var tbi in customCat.Items.ToArray())
                {
                    if (!wpfTypes.Any(x => x == tbi.Type))
                    {
                        customCat.Items.Remove(tbi);
                    }
                }

                if (!customCat.Items.Any())
                {
                    Items.Remove(customCat);
                }

                //var otherCat = new ToolboxItemCategory("WPF Controls", Constants.ContentIds.WPF_DESIGNER);

                //foreach (var type in otherTypes)
                //{
                //    wpfCat.Items.Add(new KToolboxItem(type.Name, type, "WPF Controls", Constants.ContentIds.WPF_DESIGNER));
                //}
            }

            //WPF items
            var wpfcatName = "WPF Controls";
            var wpfCat = this.Items.FirstOrDefault(x => x.Name == wpfcatName && x.DocumentTypeId == Korduene.Constants.WPF_DESIGNER);

            if (wpfCat == null)
            {
                wpfCat = new ToolboxItemCategory(wpfcatName, Korduene.Constants.WPF_DESIGNER) { IsExpanded = true };
                this.Items.Add(wpfCat);
            }

            foreach (var type in _wpfTypes)
            {
                if (!wpfCat.Items.Any(x => x.Type == type && x.Category == wpfcatName && x.DocumentTypeId == Korduene.Constants.WPF_DESIGNER))
                {
                    wpfCat.Items.Add(new KToolboxItem(type.Name, type, wpfcatName, Korduene.Constants.WPF_DESIGNER));
                }
            }

            RefreshVisibleItems();
        }

        #endregion

        #region [Private Methods]

        private void Instance_SolutionLoaded(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void Instance_BuildFinished(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void RefreshVisibleItems()
        {
            if (Current.Instance.ActiveDocument != null)
            {
                foreach (var item in this.Items)
                {
                    item.IsVisible = item.DocumentTypeId == Current.Instance.ActiveDocument.ContentId;
                }
            }
        }

        #endregion
    }
}
