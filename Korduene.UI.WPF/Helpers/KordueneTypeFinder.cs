using ICSharpCode.WpfDesign.XamlDom;
using System;
using System.Reflection;

namespace Korduene.UI.WPF.Helpers
{
    public class KordueneTypeFinder : XamlTypeFinder
    {
        public static KordueneTypeFinder Instance { get; }

        static KordueneTypeFinder()
        {
            Instance = new KordueneTypeFinder();
            Instance.ImportFrom(XamlTypeFinder.CreateWpfTypeFinder());
            Current.Instance.BuildFinished += Instance_BuildFinished;
        }

        private static void Instance_BuildFinished(object sender, EventArgs e)
        {
            RefreshAssemblies();
        }

        public override Assembly LoadAssembly(string name)
        {
            foreach (var item in RegisteredAssemblies)
            {
                if (item.GetName().Name == name)
                {
                    return item;
                }
            }

            return null;
            //return base.LoadAssembly(name);
        }

        public override XamlTypeFinder Clone()
        {
            return Instance;
        }

        public static void RefreshAssemblies()
        {
            foreach (var item in Current.Instance.Workspace.TargetPaths)
            {
                var assembly = Assembly.Load(System.IO.File.ReadAllBytes(item));
                Instance.RegisterAssembly(assembly);
            }
        }
    }
}
