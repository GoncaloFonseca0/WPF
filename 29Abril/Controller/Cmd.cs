using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _29Abril.Controller
{
    internal class Cmd : ICommand
    {

        private Action<Object> _execute { get; set; }
        private Predicate<Object> _canExecute { get; set; }

        public Cmd(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }   

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; } 
        }


        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);  // v or False 
        }

        public void Execute(object? parameter)
        {

            _execute.Invoke(parameter);
        }
    }
}
