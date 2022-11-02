using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    internal class SplashScreen : IScreen
    {
        private readonly Texture2D _splashImage;
        

        public SplashScreen(Texture2D splash)
        {
            _splashImage = splash;
        }

        public ScreenType ScreenType => ScreenType.Splash;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                _splashImage,
                new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight),
                Color.White
            );
            spriteBatch.End();
        }

        public void Update(float delta)
        {
            
        }
    }
}
