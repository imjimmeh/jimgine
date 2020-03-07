using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Events
{
    public class TextValueChangeEventArgs : EventArgs
    {
        public string NewText { get; set; }

        public override string ToString()
        {
            return NewText;
        }
    }
}
