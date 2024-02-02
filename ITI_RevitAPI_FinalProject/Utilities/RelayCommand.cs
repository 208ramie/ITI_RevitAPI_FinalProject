using System;
using System.Windows.Input;

namespace ITI_RevitAPI_FinalProject.Utilities
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> Exec { get; set; }
        public Predicate<object> CanExec { get; set; }
        public RelayCommand(Action<object> exec) => Exec = exec;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => Exec(parameter);
    }
}
