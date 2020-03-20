using Jimgine.Core.Graphics.UI;
using Jimgine.Core.Models.Graphics.UI;
using Jimgine.Core.Models.World.Characters.Players;
using Microsoft.Xna.Framework;
using System;

namespace Jimgine.Test.UI
{
    public class UIService
    {
        UIComponentFactory _uiComponentFactory;
        int _screenWidth;
        int _screenHeight;
        UIGroup _sidebar;
        UIGroup _sidebarShower;

        DateTime _lastTimeSidebarShown;

        public UIService(UIComponentFactory uiComponentFactory, int screenWidth, int screenHeight)
        {
            _uiComponentFactory = uiComponentFactory;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _lastTimeSidebarShown = DateTime.Now;
        }

        public void Initialise()
        {
            CreateSidebar();
        }

        public IUIComponent CreatePlayerStatsBar(Player player)
        {
            var health = _uiComponentFactory.CreateText(new Point(0, 0), 5, $"Health: {player.Health.ToString()}", Color.Black, "default", false);
            var group = _uiComponentFactory.CreateUIGroup(new[] { health }, new Point(0, 0));

            return health;
        }

        void CreateSidebar()
        {
            int number = (_screenWidth / 6);

            var sidebarBackground = _uiComponentFactory.CreateButtonWithBlockColour(number, _screenHeight, Color.Gray, new Vector2(0, 0));
            var hideButton = _uiComponentFactory.CreateButtonWithBlockColour(number / 10, 20, Color.Black, new Vector2(number - (number / 10), 0));

            var hideText = _uiComponentFactory.CreateText(new Point(number - (number / 10), 0), 5, ">", Color.White, "default", false);
            var group = _uiComponentFactory.CreateUIGroup(new[] { sidebarBackground, hideButton, hideText }, new Point(_screenWidth - (number), 0));
            hideButton.SetValue<Action<object>>(HideSidebar);

            var hideButton2 = _uiComponentFactory.CreateButtonWithBlockColour((number) / 10, 20, Color.Black, new Vector2(_screenWidth - (number / 10), 0));
            var hideText2 = _uiComponentFactory.CreateText(new Point(_screenWidth - (number / 10), 0), 5, ">", Color.White, "default", false);
            var group2 = _uiComponentFactory.CreateUIGroup(new[] { hideButton2, hideText2 }, new Point(0, 0));

            hideButton2.SetVisible(false);
            hideText2.SetVisible(false);
            hideButton2.SetValue<Action<object>>(HideSidebar);

            _sidebar = group;
            _sidebarShower = group2;
        }

        void HideSidebar(object o)
        {
            var sender = (UIComponent)o;
            var test = (DateTime.Now - _lastTimeSidebarShown).TotalMilliseconds;
            if ((DateTime.Now - _lastTimeSidebarShown).TotalMilliseconds < 10)
            {
                return;
            }
            if(sender.Owner == _sidebar)
            {
                _sidebarShower.SetVisible(sender.Visible);
                _sidebar.SetVisible(!sender.Visible);
                _lastTimeSidebarShown = DateTime.Now;
            }
            else
            {
                _sidebar.SetVisible(sender.Visible);
                _sidebarShower.SetVisible(!sender.Visible);
                _lastTimeSidebarShown = DateTime.Now;
            }
        }
    }
}
