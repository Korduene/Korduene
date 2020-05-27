using Korduene.UI;
using Korduene.UI.Dialogs.Data;
using Korduene.UI.Enums;
using System;
using System.IO;
using System.Linq;

namespace Korduene.GlobalCommands
{
    public static class SolutionCommands
    {
        public static readonly KCommand BuildCommand = new KCommand(ExecuteBuildCommand, CanExecuteBuildCommand);
        public static readonly KCommand RebuildCommand = new KCommand(ExecuteRebuildCommand, CanExecuteRebuildCommand);
        public static readonly KCommand CleanCommand = new KCommand(ExecuteCleanCommand, CanExecuteCleanCommand);
        public static readonly KCommand PublishCommand = new KCommand(ExecutePublishCommand, CanExecutePublishCommand);
        public static readonly KCommand RestoreCommand = new KCommand(ExecuteRestoreCommand, CanExecuteRestoreCommand);

        public static readonly KCommand NewProjectCommand = new KCommand(ExecuteNewProjectCommand, CanExecuteNewProjectCommand);
        public static readonly KCommand NewFolderCommand = new KCommand(ExecuteNewFolderCommand, CanExecuteNewFolderCommand);
        public static readonly KCommand NewFileCommand = new KCommand(ExecuteNewFileCommand, CanExecuteNewFileCommand);

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

        private static bool CanExecuteNewProjectCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        public static void ExecuteNewProjectCommand()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Korduene", "Projects");

            if (Current.Instance.Workspace == null)
            {
                var newProject = Current.Instance.UIServices.CreateDialog<NewProjectData>(DialogType.NewProject, dir);

                if (newProject.Success)
                {
                    Current.Instance.CreateSolution(newProject.Data.Template.ChosenName, newProject.Data.Directory, newProject.Data.Template);
                }

                return;
            }

            dir = Path.GetDirectoryName(Current.Instance.Workspace.SlnFile.FullPath);

            var result = Current.Instance.UIServices.CreateDialog<NewProjectData>(DialogType.NewProject, dir);

            if (!result.Success)
            {
                return;
            }

            var template = result.Data.Template;
            var name = result.Data.Template.ChosenName;

            //create proj dir
            var projDir = Path.Combine(result.Data.Directory, name);

            template.CreateFiles(projDir);

            var fullPath = Path.Combine(result.Data.Directory, name, result.Data.Template.ChosenName + Path.GetExtension(result.Data.Template.MainFile));

            var proj = new Microsoft.DotNet.Cli.Sln.Internal.SlnProject()
            {
                Id = template.Id,
                FilePath = Path.GetRelativePath(dir, fullPath),
                Name = name,
                TypeGuid = template.ProjectTypeGuid
            };

            Current.Instance.Workspace.SlnFile.Projects.Add(proj);
            Current.Instance.Workspace.SlnFile.Write();
            Current.Instance.OpenSolution(Current.Instance.Workspace.SlnFile.FullPath);
        }

        private static bool CanExecuteNewFolderCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        private static void ExecuteNewFolderCommand()
        {
            var selected = Current.Instance.WorkspaceItems.First().GetSelected().First();
            var result = Current.Instance.UIServices.CreateDialog<PromptData>(UI.Enums.DialogType.GenericPrompt, "Folder Name", "New Folder");

            if (!result.Success)
            {
                return;
            }

            var dir = selected.Path;

            if (System.IO.File.GetAttributes(selected.Path) != System.IO.FileAttributes.Directory)
            {
                dir = System.IO.Path.GetDirectoryName(selected.Path);
            }

            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(dir, result.Data.Value));
            Current.Instance.UpdateWorkspaceItems();
        }

        private static bool CanExecuteNewFileCommand(object obj)
        {
            return Current.Instance.State == AppState.SolutionReady;
        }

        private static void ExecuteNewFileCommand()
        {
            var selected = Current.Instance.WorkspaceItems.First().GetSelected().First();
            var dir = selected.Path;

            if (System.IO.File.GetAttributes(selected.Path) != System.IO.FileAttributes.Directory)
            {
                dir = System.IO.Path.GetDirectoryName(selected.Path);
            }

            var newFile = Current.Instance.UIServices.CreateDialog(DialogType.NewFile, dir);

            var ns = string.Empty;

            if (selected.Type == WorkspaceItemType.Project)
            {
                ns = selected.Name;
            }
            else if (selected.Type == WorkspaceItemType.File)
            {
                var fileDir = System.IO.Path.GetDirectoryName(selected.Path);
                var projDir = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(selected.Project.Path));
                ns = fileDir.Replace(projDir, string.Empty, StringComparison.OrdinalIgnoreCase).Trim('\\').Replace(System.IO.Path.DirectorySeparatorChar, '.');
            }
            else if (selected.Type == WorkspaceItemType.Folder)
            {
                var projDir = System.IO.Path.GetDirectoryName(selected.Project.Path);
                ns = selected.Path.Replace(projDir, string.Empty, StringComparison.OrdinalIgnoreCase).Replace(System.IO.Path.DirectorySeparatorChar, '.');
            }

            if (newFile.Success)
            {
                var data = newFile.Data as NewFileData;
                data.Template.Namespace = ns;
                data.Template.CreateFiles(dir);
                Current.Instance.UpdateWorkspaceItems();
            }
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
