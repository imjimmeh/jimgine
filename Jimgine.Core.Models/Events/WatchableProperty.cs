using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Events
{
    public class WatchableProperty<T> 
    {
        T _value;
        public T Value => _value;

        event EventHandler Events;

        public WatchableProperty(T value)
        {
            _value = value;
        }

        public void SetValue(T value)
        {
            _value = value;
        }

        public void AddEvent(EventHandler d)
        {
            Events += d;
        }

        public void RemoveEvent(EventHandler d)
        {
            Events -= d;
        }

        public void Invoke(object sender, EventArgs args)
        {
            Events?.Invoke(sender, args);
        }
    }
}
