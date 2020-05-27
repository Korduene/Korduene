using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Korduene.UI.WPF.Converters
{
    public class TypeToImageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value as Type;

            if (type == null)
            {
                return null;
            }

            if (type == typeof(AccessText))
            {
                return GetResource("AccessText_16x");
            }
            else if (type == typeof(AdornedElementPlaceholder))
            {
                return GetResource("AdornedElementPlaceholder_16x");
            }
            else if (type == typeof(AdornerDecorator))
            {
                return GetResource("AdornerDecorator_16x");
            }
            else if (type == typeof(Border))
            {
                return GetResource("BorderElement_16x");
            }
            else if (type == typeof(BulletDecorator))
            {
                return GetResource("BulletDecorator_16x");
            }
            else if (type == typeof(Button))
            {
                return GetResource("Button_16x");
            }
            else if (type == typeof(System.Windows.Controls.Calendar))
            {
                return GetResource("Calendar_16x");
            }
            else if (type == typeof(CalendarButton))
            {
                return GetResource("CalendarButton_16x");
            }
            else if (type == typeof(CalendarDayButton))
            {
                return GetResource("CalendarDayButton_16x");
            }
            else if (type == typeof(CalendarItem))
            {
                return GetResource("CalendarItem_16x");
            }
            else if (type == typeof(Canvas))
            {
                return GetResource("CanvasElement_16x");
            }
            else if (type == typeof(CheckBox))
            {
                return GetResource("CheckBox_16x");
            }
            else if (type == typeof(ComboBox))
            {
                return GetResource("ComboBox_16x");
            }
            else if (type == typeof(ComboBoxItem))
            {
                return GetResource("ComboBoxItem_16x");
            }
            else if (type == typeof(ContentControl))
            {
                return GetResource("ContentControlElement_16x");
            }
            else if (type == typeof(ContentPresenter))
            {
                return GetResource("ContentPresenter_16x");
            }
            else if (type == typeof(ContextMenu))
            {
                return GetResource("ContextMenu_16x");
            }
            else if (type == typeof(Control))
            {
                return GetResource("Control_16x");
            }
            else if (type == typeof(DataGrid))
            {
                return GetResource("DataGrid_16x");
            }
            else if (type == typeof(DataGridCell))
            {
                return GetResource("DataGridCell_16x");
            }
            else if (type == typeof(DataGridCellsPanel))
            {
                return GetResource("DataGridCellsPanel_16x");
            }
            else if (type == typeof(DataGridCellsPresenter))
            {
                return GetResource("DataGridCellsPresenter_16x");
            }
            else if (type == typeof(DataGridColumnHeader))
            {
                return GetResource("DataGridColumnHeader_16x");
            }
            else if (type == typeof(DataGridColumnHeadersPresenter))
            {
                return GetResource("DataGridColumnHeadersPresenter_16x");
            }
            else if (type == typeof(DataGridDetailsPresenter))
            {
                return GetResource("DataGridDetailsPresenter_16x");
            }
            else if (type == typeof(DataGridRow))
            {
                return GetResource("DataGridRow_16x");
            }
            else if (type == typeof(DataGridRowHeader))
            {
                return GetResource("DataGridRowHeader_16x");
            }
            else if (type == typeof(DataGridRowsPresenter))
            {
                return GetResource("DataGridRowsPresenter_16x");
            }
            else if (type == typeof(DatePicker))
            {
                return GetResource("DatePicker_16x");
            }
            else if (type == typeof(DatePickerTextBox))
            {
                return GetResource("DatePickerTextBox_16x");
            }
            else if (type == typeof(Decorator))
            {
                return GetResource("Decorator_16x");
            }
            else if (type == typeof(DockPanel))
            {
                return GetResource("DockPanel_16x");
            }
            else if (type == typeof(DocumentPageView))
            {
                return GetResource("DocumentPageView_16x");
            }
            else if (type == typeof(DocumentReference))
            {
                return GetResource("DocumentReference_16x");
            }
            else if (type == typeof(DocumentViewer))
            {
                return GetResource("DocumentViewer_16x");
            }
            else if (type == typeof(Ellipse))
            {
                return GetResource("Ellipse_16x");
            }
            else if (type == typeof(Expander))
            {
                return GetResource("Expander_16x");
            }
            else if (type == typeof(FixedPage))
            {
                return GetResource("FixedPage_16x");
            }
            else if (type == typeof(FlowDocumentPageViewer))
            {
                return GetResource("FlowDocumentPageViewer_16x");
            }
            else if (type == typeof(FlowDocumentReader))
            {
                return GetResource("FlowDocumentReader_16x");
            }
            else if (type == typeof(FlowDocumentScrollViewer))
            {
                return GetResource("FlowDocumentScrollViewer_16x");
            }
            else if (type == typeof(Frame))
            {
                return GetResource("FrameContainer_16x");
            }
            else if (type == typeof(FrameworkElement))
            {
                return GetResource("FrameworkElement_16x");
            }
            else if (type == typeof(Glyphs))
            {
                return GetResource("Glyphs_16x");
            }
            else if (type == typeof(Grid))
            {
                return GetResource("Grid_16x");
            }
            else if (type == typeof(GridSplitter))
            {
                return GetResource("GridSplitter_16x");
            }
            else if (type == typeof(GridViewColumnHeader))
            {
                return GetResource("GridViewColumnHeader_16x");
            }
            else if (type == typeof(GridViewHeaderRowPresenter))
            {
                return GetResource("GridViewHeaderRowPresenter_16x");
            }
            else if (type == typeof(GridViewRowPresenter))
            {
                return GetResource("GridViewRowPresenter_16x");
            }
            else if (type == typeof(GroupBox))
            {
                return GetResource("GroupBox_16x");
            }
            else if (type == typeof(GroupItem))
            {
                return GetResource("GroupItem_16x");
            }
            else if (type == typeof(HeaderedContentControl))
            {
                return GetResource("HeaderedContentControl_16x");
            }
            else if (type == typeof(HeaderedItemsControl))
            {
                return GetResource("HeaderedItemsControl_16x");
            }
            else if (type == typeof(Image))
            {
                return GetResource("Image_16x");
            }
            else if (type == typeof(InkCanvas))
            {
                return GetResource("InkCanvasControl_16x");
            }
            else if (type == typeof(InkPresenter))
            {
                return GetResource("InkPresenter_16x");
            }
            else if (type == typeof(ItemsControl))
            {
                return GetResource("ItemsControl_16x");
            }
            else if (type == typeof(ItemsPresenter))
            {
                return GetResource("ItemsPresenter_16x");
            }
            else if (type == typeof(Label))
            {
                return GetResource("Label_16x");
            }
            else if (type == typeof(Line))
            {
                return GetResource("Line_16x");
            }
            else if (type == typeof(ListBox))
            {
                return GetResource("ListBox_16x");
            }
            else if (type == typeof(ListBoxItem))
            {
                return GetResource("ListBoxItem_16x");
            }
            else if (type == typeof(ListView))
            {
                return GetResource("ListView_16x");
            }
            else if (type == typeof(ListViewItem))
            {
                return GetResource("ListViewItem_16x");
            }
            else if (type == typeof(MediaElement))
            {
                return GetResource("Media_16x");
            }
            else if (type == typeof(Menu))
            {
                return GetResource("MainMenuControl_16x");
            }
            else if (type == typeof(MenuItem))
            {
                return GetResource("MenuItem_16x");
            }
            else if (type == typeof(NavigationWindow))
            {
                return GetResource("NavigationWindow_16x");
            }
            else if (type == typeof(Page))
            {
                return GetResource("Page_16x");
            }
            else if (type == typeof(PageContent))
            {
                return GetResource("PageContent_16x");
            }
            else if (type == typeof(PasswordBox))
            {
                return GetResource("PasswordBox_16x");
            }
            else if (type == typeof(Path))
            {
                return GetResource("Path_16x");
            }
            else if (type == typeof(Polygon))
            {
                return GetResource("Polygon_16x");
            }
            else if (type == typeof(Polyline))
            {
                return GetResource("Polyline_16x");
            }
            else if (type == typeof(Popup))
            {
                return GetResource("Popup_16x");
            }
            else if (type == typeof(ProgressBar))
            {
                return GetResource("ProgressBar_16x");
            }
            else if (type == typeof(RadioButton))
            {
                return GetResource("RadioButton_16x");
            }
            else if (type == typeof(Rectangle))
            {
                return GetResource("Rectangle_16x");
            }
            else if (type == typeof(RepeatButton))
            {
                return GetResource("RepeatButton_16x");
            }
            else if (type == typeof(ResizeGrip))
            {
                return GetResource("ResizeGrip_16x");
            }
            else if (type == typeof(RichTextBox))
            {
                return GetResource("RichTextBox_16x");
            }
            else if (type == typeof(ScrollBar))
            {
                return GetResource("ScrollBar_16x");
            }
            else if (type == typeof(ScrollContentPresenter))
            {
                return GetResource("ScrollContentPresenter_16x");
            }
            else if (type == typeof(ScrollViewer))
            {
                return GetResource("ImageScrollViewer_16x");
            }
            else if (type == typeof(SelectiveScrollingGrid))
            {
                return GetResource("SelectiveScrollingGrid_16x");
            }
            else if (type == typeof(Separator))
            {
                return GetResource("Separator_16x");
            }
            else if (type == typeof(Slider))
            {
                return GetResource("Slider_16x");
            }
            else if (type == typeof(StackPanel))
            {
                return GetResource("StackPanel_16x");
            }
            else if (type == typeof(StatusBar))
            {
                return GetResource("StatusStrip_16x");
            }
            else if (type == typeof(StatusBarItem))
            {
                return GetResource("StatusBarItem_16x");
            }
            else if (type == typeof(TabControl))
            {
                return GetResource("Tab_16x");
            }
            else if (type == typeof(TabItem))
            {
                return GetResource("TabItem_16x");
            }
            else if (type == typeof(TabPanel))
            {
                return GetResource("TabPanel_16x");
            }
            else if (type == typeof(TextBlock))
            {
                return GetResource("TextBlock_16x");
            }
            else if (type == typeof(TextBox))
            {
                return GetResource("TextBox_16x");
            }
            else if (type == typeof(Thumb))
            {
                return GetResource("Thumb_16x");
            }
            else if (type == typeof(TickBar))
            {
                return GetResource("TickBar_16x");
            }
            else if (type == typeof(ToggleButton))
            {
                return GetResource("ToggleButton_16x");
            }
            else if (type == typeof(ToolBar))
            {
                return GetResource("ToolBar_16x");
            }
            else if (type == typeof(ToolBarOverflowPanel))
            {
                return GetResource("ToolBarOverflowPanel_16x");
            }
            else if (type == typeof(ToolBarPanel))
            {
                return GetResource("ToolBarPanelElement_16x");
            }
            else if (type == typeof(ToolBarTray))
            {
                return GetResource("ToolBarTrayElement_16x");
            }
            else if (type == typeof(ToolTip))
            {
                return GetResource("ToolTip_16x");
            }
            else if (type == typeof(Track))
            {
                return GetResource("Track_16x");
            }
            else if (type == typeof(TreeView))
            {
                return GetResource("TreeView_16x");
            }
            else if (type == typeof(TreeViewItem))
            {
                return GetResource("TreeViewItem_16x");
            }
            else if (type == typeof(UniformGrid))
            {
                return GetResource("UniformGrid_16x");
            }
            else if (type == typeof(UserControl))
            {
                return GetResource("UserControl_16x");
            }
            else if (type == typeof(Viewbox))
            {
                return GetResource("ViewBox_16x");
            }
            else if (type == typeof(Viewport3D))
            {
                return GetResource("3DScene_16x");
            }
            else if (type == typeof(VirtualizingStackPanel))
            {
                return GetResource("VirtualizingStackPanel_16x");
            }
            else if (type == typeof(WebBrowser))
            {
                return GetResource("ASPNETWebApplication_16x");
            }
            else if (type == typeof(Window))
            {
                return GetResource("Window_16x");
            }
            else if (type == typeof(WrapPanel))
            {
                return GetResource("WrapPanel_16x");
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        private object GetResource(string name)
        {
            foreach (var md in System.Windows.Application.Current.Resources.MergedDictionaries)
            {
                foreach (var key in md.Keys)
                {
                    if (key.ToString().Equals(name))
                    {
                        return md[key];
                    }
                }
            }

            return null;
        }
    }
}
