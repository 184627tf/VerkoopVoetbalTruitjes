using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.Commands {
    public class KlantUpdateCommand : ICommand {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) {
            return true;
        }

        public void Execute(object? parameter) {
            throw new NotImplementedException();
        }
    }
}
