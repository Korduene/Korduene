using System;

namespace Korduene.UI
{
    public sealed class KToolboxItem : IKToolboxItem
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string IconSource { get; set; }

        public string DocumentTypeId { get; set; }

        public Type Type { get; }

        public KToolboxItem(string name, Type type, string category, string documentTypeId)
        {
            this.Name = name;
            this.Type = type;
            this.Category = category;
            this.DocumentTypeId = documentTypeId;
        }

        public KToolboxItem(string name, string category, Type type, string documentTypeId, string iconSource)
        {
            this.Name = name;
            this.Category = category;
            this.Type = type;
            this.DocumentTypeId = documentTypeId;
            this.IconSource = iconSource;
        }
    }
}
