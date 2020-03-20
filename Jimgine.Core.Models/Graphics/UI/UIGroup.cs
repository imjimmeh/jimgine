using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jimgine.Core.Models.Graphics.UI
{
    public class UIGroup
    {
        UIComponent[] _uiComponents;
        public UIComponent[] UIComponents => _uiComponents;

        internal Point _position;
        public Point Position => _position;

        public UIGroup(UIComponent[] components, Point position)
        {
            _uiComponents = components;
            _position = position;
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            for (var x = 0; x < UIComponents.Length; x++)
            {
                if (UIComponents[x] != null)
                {
                    UIComponents[x].Draw(ref spriteBatch, _position);
                }
            }
        }

        public void SetVisible(bool visible)
        {
            for (var x = 0; x < UIComponents.Length; x++)
            {
                if (UIComponents[x] != null)
                {
                    UIComponents[x].SetVisible(visible);
                }
            }
        }
    }
}
