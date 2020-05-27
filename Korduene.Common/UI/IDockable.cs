using System.ComponentModel;

namespace Korduene.UI
{
    public interface IDockable : INotifyPropertyChanged
    {
        string Name { get; set; }

        bool IsActive { get; set; }

        bool IsSelected { get; set; }

        void OnLoaded();

        void OnShown();
    }
}
