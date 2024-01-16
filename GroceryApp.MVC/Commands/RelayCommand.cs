using System.Windows.Input;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }

        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
            RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
            SimpleCommandManager.AddRaiseCanExecuteChangedAction(ref RaiseCanExecuteChangedAction);
        }

        ~RelayCommand()
        {
            RemoveCommand();
        }

        public void RemoveCommand()
        {
            SimpleCommandManager.RemoveRaiseCanExecuteChangedAction(RaiseCanExecuteChangedAction);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
            SimpleCommandManager.RefreshCommandStates();
        }

        public bool CanExecute
        {
            get { return _canExecute == null || _canExecute(); }
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private readonly Action RaiseCanExecuteChangedAction;

        public event EventHandler CanExecuteChanged;
    }
}
