using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jimgine.Core.Models.Input
{
    public class KeyboardInputContainer
    {
        Keys monitoredKey;
        public Keys MonitoredKey { get => monitoredKey; }

        ICommand command;
        public ICommand Command { get => command; }

        public KeyboardInputContainer(Keys key, ICommand command)
        {
            monitoredKey = key;
            this.command = command;
        }
    }
}
