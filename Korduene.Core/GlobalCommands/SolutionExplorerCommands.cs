using Korduene.UI;
using System.Linq;
using System.Threading.Tasks;

namespace Korduene.GlobalCommands
{
    public static class SolutionExplorerCommands
    {
        public static readonly KCommand BuildCommand = new KCommand(ExecuteBuildCommand, CanExecuteBuildCommand);
        public static readonly KCommand RebuildCommand = new KCommand(ExecuteRebuildCommand, CanExecuteRebuildCommand);
        public static readonly KCommand CleanCommand = new KCommand(ExecuteCleanCommand, CanExecuteCleanCommand);
        public static readonly KCommand PublishCommand = new KCommand(ExecutePublishCommand, CanExecutePublishCommand);
        public static readonly KCommand RestoreCommand = new KCommand(ExecuteRestoreCommand, CanExecuteRestoreCommand);

        public static readonly KCommand OpenFileCommand = new KCommand(ExecuteOpenFileCommand, CanExecuteOpenFileCommand);
        public static readonly KCommand OpenFolderCommand = new KCommand(ExecuteOpenFolderCommand, CanExecuteOpenFolderCommand);

        public static readonly KCommand RemoveCommand = new KCommand(ExecuteRemoveCommand, CanExecuteRemoveCommand);
        public static readonly KCommand DeleteCommand = new KCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        public static readonly KCommand RenameCommand = new KCommand(ExecuteRenameCommand, CanExecuteRenameCommand);

        private static bool CanExecuteBuildCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        public static async void ExecuteBuildCommand()
        {
            await Current.Instance.BuildSolution("build");
        }

        private static bool CanExecuteRebuildCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        public static async void ExecuteRebuildCommand()
        {
            await Current.Instance.BuildSolution("rebuild");
        }

        private static bool CanExecuteCleanCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        public static async void ExecuteCleanCommand()
        {
            await Current.Instance.BuildSolution("clean");
        }

        private static bool CanExecutePublishCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        public static async void ExecutePublishCommand()
        {
            await Current.Instance.BuildSolution("publish");
        }

        private static bool CanExecuteRestoreCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        public static async void ExecuteRestoreCommand()
        {
            await Current.Instance.BuildSolution("restore");
        }

        private static bool CanExecuteOpenFileCommand(object obj)
        {
            return true;
        }

        public static void ExecuteOpenFileCommand()
        {
            var selected = Current.Instance.WorkspaceItems.First().GetSelected().FirstOrDefault();

            if (selected.Tag is Microsoft.CodeAnalysis.Document document)
            {
                Current.Instance.OpenDocument(document);
            }
        }

        private static bool CanExecuteOpenFolderCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        public static void ExecuteOpenFolderCommand()
        {
            var selected = Current.Instance.WorkspaceItems.First().GetSelected().FirstOrDefault();

            var dir = selected.Path;

            if (System.IO.File.GetAttributes(dir) != System.IO.FileAttributes.Directory)
            {
                dir = System.IO.Path.GetDirectoryName(selected.Path);
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dir) { UseShellExecute = true });
        }

        private static bool CanExecuteRemoveCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        private static void ExecuteRemoveCommand()
        {
            var selected = Current.Instance.WorkspaceItems.First().GetSelected().FirstOrDefault();
            Current.Instance.RemoveProject(selected.Path);
        }

        private static bool CanExecuteDeleteCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        private async static void ExecuteDeleteCommand()
        {
            var selected = Current.Instance.WorkspaceItems.First().GetSelected().FirstOrDefault();

            var path = selected.Path;

            if (System.IO.File.GetAttributes(path) != System.IO.FileAttributes.Directory)
            {
                System.IO.File.Delete(selected.Path);
            }
            else
            {
                System.IO.Directory.Delete(selected.Path, true);
            }

            await Task.Delay(300).ContinueWith(_ => Current.Instance.UpdateWorkspaceItems(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private static bool CanExecuteRenameCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        private static void ExecuteRenameCommand()
        {
            Current.Instance.RenameItem(Current.Instance.WorkspaceItems.First().GetSelected().FirstOrDefault());
        }

        public static void RaiseAllCanExecuteChanged()
        {
            BuildCommand.RaiseCanExecuteChanged();
            RebuildCommand.RaiseCanExecuteChanged();
            CleanCommand.RaiseCanExecuteChanged();
            PublishCommand.RaiseCanExecuteChanged();
            RestoreCommand.RaiseCanExecuteChanged();
        }
    }
}
