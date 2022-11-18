using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Project.Levels
{
    internal class Level : IGameObject
    {
        private Texture2D blockTexture;
        private Game1 _game;

        int[,] gameboard = new int[,]
        {
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 1,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1 }
        };

        public Level(Game1 game)
        {
            _game = game;
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });
        }

        public ScreenType ScreenType => ScreenType.None;

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(float delta, GameTime gameTime)
        {

        }
    }
}
