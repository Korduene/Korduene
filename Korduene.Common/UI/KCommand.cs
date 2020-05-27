using System;
using System.Windows.Input;

namespace Korduene.UI
{
    public class KCommand : ICommand
    {
        private Predicate<object> _canExecute;
        private Action _execute;

        public event EventHandler CanExecuteChanged;

        public KCommand(Action execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public KCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
