namespace Korduene.GlobalCommands
{
    public static class GlobalCommandUtilities
    {
        public static void RaiseAllCanExecuteChanged()
        {
            SolutionCommands.RaiseAllCanExecuteChanged();
        }
    }
}
