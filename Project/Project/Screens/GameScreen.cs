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
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    internal class GameScreen : IGameObject
    {
        private Character _character;
        private Boundingbox _boundingBox;
        private MovementController _movementController;
        private Texture2D _idle;
        private Texture2D _run;
        private Texture2D blockTexture;
        private Rectangle blockTexture2;
        private Game1 _game;
        private Level level;
        private Collision collision;
        public ScreenType ScreenType => ScreenType.Game;


        public GameScreen(Texture2D idle, Texture2D run, MovementController movement, Game1 game)
        {
            _game = game;
            _movementController = movement;
            level = new Level(game);
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });

            blockTexture2 = new Rectangle(200, 300, 20, 50);

            _character = new Character(idle, run,new KeyboardController(), new MovementController());
            _boundingBox = new Boundingbox((int)_character.Position.X, (int)_character.Position.Y, 24 , 32, blockTexture, _character);
            collision = new Collision(_boundingBox, _character);

            _idle = idle;
            _run = run;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blockTexture, blockTexture2, Color.Red);
            _boundingBox.Draw(spriteBatch);
            _character.Draw(spriteBatch);
        }

        public void Update(float delta, GameTime gameTime) 
        {
            _character.Update(delta, gameTime);
            _boundingBox.Update((int)_character.Position.X, (int)_character.Position.Y);
        }
    }
}
