using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AllModelCreatorInWPF
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The action to run.
        /// </summary>
        private Action mAction;

        /// <summary>
        /// The event that is fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
