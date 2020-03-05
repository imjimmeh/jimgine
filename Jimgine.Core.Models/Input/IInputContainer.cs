using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jimgine.Core.Models
{
    public interface IInputContainer
    {
        ICommand Command { get;  }
        void CheckForInput();
    }
}
