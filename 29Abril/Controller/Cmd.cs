using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _29Abril.Controller
{
    public class Cmd : ICommand
    {
        private Predicate<Object> _canExecute { get; set; }
        private Action<Object> _execute { get; set; }
       

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
            return _canExecute.Invoke(parameter);  // v or False 
        }

        public void Execute(object? parameter)
        {

            _execute.Invoke(parameter);
        }
    }
}
