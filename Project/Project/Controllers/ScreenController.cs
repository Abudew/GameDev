using Microsoft.Xna.Framework.Graphics;
using Project.Interfaces;
using System.Collections.Generic;

namespace Project.Controllers
{
    internal class ScreenController
    {
        private IReadOnlyCollection<IScreen> _screens;
        private IScreen _activeScreen;
        private IScreen _nextScreen;

        public ScreenController(IReadOnlyCollection<IScreen> screens)
        {
            _screens = screens;
        }

        public void SetScreen(ScreenType screentype)
        {
            foreach (var screen in _screens)
            {
                if (screen.ScreenType == screentype)
                {
                    _nextScreen = screen;
                    return;
                }
            }
        }

        public void SwitchToNextScreen()
        {
            if (_nextScreen != null)
            {
                _activeScreen = _nextScreen;
            }

            _nextScreen = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _activeScreen?.Draw(spriteBatch);
        }

        public void Update(float delta)
        {
            _activeScreen?.Update(delta);
        }
    }
}
