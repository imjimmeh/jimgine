using Jimgine.Core.Models.Graphics.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Input
{
    public class UIInputService
    {
        public IEnumerable<IUIComponent> GetInteractableComponentsForClickPoint(IEnumerable<UIComponent> components, Point clickPosition, bool? movableOnly = null)
        {
            foreach(var component in components)
            {
                if (movableOnly ?? true && !component.IsMovable)
                    continue;

                if(component.IntersectsMouseCoordinates(clickPosition))
                {
                    yield return component;
                }
            }
        }

    }
}
