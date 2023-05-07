using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace moodtracker
{
    public class ViewModelCommand : ICommand
    {
        //поле, которое хранит ссылку на делегат, который будет вызываться при выполнении команды
        private readonly Action<object> execute;
        //поле, которое хранит ссылку на метод, который определяет, может ли команда выполняться
        private readonly Func<object, bool> canExecute;

        //определяет команду, которая передает действие(execute) и метод, 
        //который проверяет, может ли команда выполняться(canExecute).
        public ViewModelCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }
        //событие, которое возникает, когда изменяется возможность выполнения команды
        public event EventHandler CanExecuteChanged
        {   
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //метод, который проверяет, может ли команда выполниться.
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;
        //метод, который вызывается при выполнении команды
        public void Execute(object parameter) => execute(parameter);

    }
}
