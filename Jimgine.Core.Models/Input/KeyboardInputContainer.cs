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

        ICommand inputCommand;
        public ICommand InputCommand { get => inputCommand; }

        ICommand inputFinishCommand;
        public ICommand InputFinishCommand { get => inputFinishCommand; }

        public KeyboardInputContainer(Keys key, ICommand inputCommand, ICommand inputFinishedCommand = null)
        {
            monitoredKey = key;
            this.inputCommand = inputCommand;
            if(inputFinishedCommand != null)
            {
                inputFinishCommand = inputFinishedCommand;
            }
        }
    }
}
