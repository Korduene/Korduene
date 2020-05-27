using System.Collections.Generic;
using System.Xml.Serialization;

namespace Korduene
{
    [System.Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class FileList
    {
        [XmlElement("File")]
        public List<FileListFile> Files { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string TargetFrameworkIdentifier { get; set; }

        [XmlAttribute]
        public decimal TargetFrameworkVersion { get; set; }

        [XmlAttribute]
        public string FrameworkName { get; set; }
    }

    [System.Serializable]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class FileListFile
    {
        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string Path { get; set; }

        [XmlAttribute]
        public string AssemblyName { get; set; }

        [XmlAttribute]
        public string PublicKeyToken { get; set; }

        [XmlAttribute]
        public string AssemblyVersion { get; set; }

        [XmlAttribute]
        public string FileVersion { get; set; }

        [XmlAttribute]
        public string Profile { get; set; }
    }
}
