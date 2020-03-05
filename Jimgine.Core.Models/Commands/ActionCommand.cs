using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Commands
{
    public class ActionCommand : Command
    {
        Action _action;

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _action();
        }
    }
}
