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
        public IEnumerable<IUIComponent> GetInteractableComponentsForClickPoint(IEnumerable<UIGroup> groups, Point clickPosition, bool? movableOnly = null)
        {
            foreach(var group in groups)
            {
                foreach (var component in group.UIComponents)
                {
                    if (movableOnly ?? true && !component.IsMovable)
                        continue;

                    if (component.IntersectsMouseCoordinates(group.Position, clickPosition))
                    {
                        yield return component;
                    }
                }
            }
        }

    }
}
