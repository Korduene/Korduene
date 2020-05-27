using System;
using System.Windows.Input;

namespace Korduene.UI
{
    public class KCommand<T> : ICommand
    {
        private Predicate<object> _canExecute;
        private Action<T> _execute;

        public event EventHandler CanExecuteChanged;

        public KCommand(Action<T> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public KCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
