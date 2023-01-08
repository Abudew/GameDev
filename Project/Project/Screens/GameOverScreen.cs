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
    internal class GameOverScreen : IGameObject
    {
        private readonly Texture2D _Image;
        private float timer = 3;
        private const float TIMER = 3;
        private Game1 _game;

        public GameOverScreen(Texture2D splash, Game1 game)
        {
            _Image = splash;
            _game = game;
        }

        public ScreenType ScreenType => ScreenType.GameOver;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _Image,
                new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight),
                Color.White
            );
        }

        public void Update(float delta, GameTime gameTime)
        {
            var time = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000f);

            timer -= time;
            if (timer <= 0)
            {
                _game.isSwitch = true;
                _game.screenSelection = ScreenType.MainMenu;

                timer = TIMER;
            }
        }
    }
}