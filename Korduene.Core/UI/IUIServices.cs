using Korduene.UI.Enums;

namespace Korduene.UI
{
    public interface IUIServices
    {
        OperationResult CreateDialog(string name, params object[] parameters);

        OperationResult<TData> CreateDialog<TData>(string name, params object[] parameters);

        OperationResult CreateDialog(DialogType dialogType, params object[] parameters);

        OperationResult<TData> CreateDialog<TData>(DialogType dialogType, params object[] parameters);
    }
}
