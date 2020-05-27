namespace Korduene.Templates.Files
{
    public class CSharpClassFileTemplate : FileTemplate
    {
        public CSharpClassFileTemplate()
        {
            this.Name = "Class";
            this.DefaultName = "Class";
            this.Language = "C#";
            this.MainFile = "FileName.cs";
            this.Icon = "CS";
        }
    }
}
