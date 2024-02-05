using System.Windows.Input;

namespace ClientLauncher.Command;

public class CommandHandler:ICommand
{
    private readonly Predicate<object> _canExecute;
    private readonly Action<object> _execute;

    public CommandHandler(Action<object> execute)
    {
        _execute = execute;
        _canExecute = null;
    }
    
    public CommandHandler(Action<object> execute, Predicate<object> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}