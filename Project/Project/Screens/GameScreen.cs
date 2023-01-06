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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    internal class GameScreen : IGameObject
    {
        private Character _character;
        private Game1 _game;
        private Level level;
        private MovementController movementController;
        private Texture2D blockTexture;
        private double HP;
        private double oneHp;
        private int x;
        private bool hasRun = false;
        public ScreenType ScreenType => ScreenType.Game;


        public GameScreen(Texture2D[] characters, Game1 game)
        {
            movementController = new MovementController();
            _game = game;
            x = _game.GraphicsDevice.Viewport.Width / 4;
            level = new Level(game, movementController);
            _character = new Character(characters, new KeyboardController(), movementController, level.blocks, level, _game);
            oneHp = ((_game.GraphicsDevice.Viewport.Width / 2) / 100) + 0.4;
            HP = (_game.GraphicsDevice.Viewport.Width / 2);
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_game.backGround[1], new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(blockTexture, new Rectangle(_game.GraphicsDevice.Viewport.Width/4, 0,(int)HP, 20), Color.Red);
            spriteBatch.Draw(blockTexture, new Rectangle(_game.GraphicsDevice.Viewport.Width / 4, 20, _game.GraphicsDevice.Viewport.Width / 2, 1), Color.Black);
            while(hasRun == false)
            {
                for (int i = 0; i < _character.lives; i++)
                {
                    spriteBatch.Draw(blockTexture, new Rectangle(x, 25, 10, 10), Color.Red);
                    x = x + 15;
                }
                hasRun = true;
            }
            level.Draw(spriteBatch);
            _character.Draw(spriteBatch);
        }

        public void Update(float delta, GameTime gameTime) 
        {
            level.Update(delta, gameTime);
            _character.Update(delta, gameTime);
            x = _game.GraphicsDevice.Viewport.Width / 4;
            hasRun = false;
            HP = oneHp * _character.hp;
        }
    }
}
