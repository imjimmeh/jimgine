using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jimgine.Core.Models.Input
{
    public class MouseInputContainer
    {
        MouseButton _monitoredButton;
        public MouseButton MonitoredButton { get => _monitoredButton; }

        ButtonState _monitoredButtonState;
        public ButtonState MonitoredButtonState => _monitoredButtonState;

        ICommand _inputCommand;
        public ICommand InputCommand => _inputCommand; 

        ICommand _inputFinishCommand;
        public ICommand InputFinishCommand => _inputFinishCommand; 

        public MouseInputContainer(MouseButton button, ButtonState buttonState, ICommand inputCommand, ICommand inputFinishedCommand = null)
        {
            _monitoredButton = button;
            _monitoredButtonState = buttonState;

            this._inputCommand = inputCommand;
            if(inputFinishedCommand != null)
            {
                _inputFinishCommand = inputFinishedCommand;
            }
        }
    }
}
