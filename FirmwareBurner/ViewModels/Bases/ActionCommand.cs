using System;
using System.Windows.Input;

namespace FirmwareBurner.ViewModels.Bases
{
    public class ActionCommand : ICommand
    {
        public ActionCommand(Action ExecuteAction)
            : this(p => ExecuteAction()) { }

        public ActionCommand(Action ExecuteAction, Func<bool> Check)
            : this(p => ExecuteAction(), p => Check()) { }

        public ActionCommand(Action<object> ExecuteAction)
            : this(ExecuteAction, p => true) { }

        public ActionCommand(Action<object> ExecuteAction, Func<object, bool> Check)
        {
            Action = ExecuteAction;
            this.Check = Check;
        }

        public Action<Object> Action { get; private set; }
        public Func<object, bool> Check { get; private set; }
        public bool CanExecute(object parameter) { return Check(parameter); }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) { Action(parameter); }
    }
}
