using System.Windows.Input;

namespace BrickGameRacing.Commands;

public class Command(
    Action<object?> execute,
    Predicate<object?>? canExecute = null
) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return canExecute == null || canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        execute.Invoke(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}