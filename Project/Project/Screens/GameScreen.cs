using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Controllers;
using Project.Interfaces;
using Project.Levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    internal class GameScreen : IGameObject
    {
        private Character _character;
        private Texture2D _idle;
        private Texture2D _run;
        private Game1 _game;
        private Level level;
        private Rectangle blockTexture2;
        public ScreenType ScreenType => ScreenType.Game;


        public GameScreen(Texture2D idle, Texture2D run, Game1 game)
        {
            _game = game;
            level = new Level(game);
            _character = new Character(idle, run, new KeyboardController(), new MovementController(), level.blocks, _game);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_game.backGround[1], new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height), Color.White);
            level.Draw(spriteBatch);
            _character.Draw(spriteBatch);
        }

        public void Update(float delta, GameTime gameTime) 
        {
            _character.Update(delta, gameTime);
        }
    }
}
