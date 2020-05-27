using System;

namespace Korduene.UI
{
    public interface IKToolboxItem
    {
        string Category { get; set; }
        string DocumentTypeId { get; set; }
        string IconSource { get; set; }
        string Name { get; set; }
        Type Type { get; }
    }
}