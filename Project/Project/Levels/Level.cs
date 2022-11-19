using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private bool hasRun = false;
        private int X = 0;
        private int Y = 0;
        private static int[,] gameboard = new int[,]
        {
            { 1,1,1,1,1,1,1,1,1,1,1 },
            { 1,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,0,1,0,0,1,0,1 },
            { 1,0,0,0,0,1,0,0,1,0,1 },
            { 1,0,0,0,0,1,0,0,1,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,0,0,1,0,0,1,0,0,1 },
            { 1,0,0,0,0,0,0,1,0,0,1 },
            { 1,0,0,1,0,0,0,1,0,0,1 },
            { 1,0,0,0,0,0,0,0,0,0,1 },
            { 1,0,1,0,0,0,0,0,0,0,1 },
            { 1,0,1,0,0,0,0,0,0,0,1 },
            { 1,0,1,0,0,0,0,0,0,0,1 },
            { 1,0,1,0,0,0,0,0,0,0,1 },
            { 1,0,1,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,1,1 },
        };

        public List<Block> blocks = new List<Block>();
        public BlockFactory blockFactory = new BlockFactory();

        public Level(Game1 game)
        {
            _game = game;
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.Black });

            //X = -(_game.GraphicsDevice.Viewport.Width / 16);
            //Y = -(_game.GraphicsDevice.Viewport.Height / 9);
        }

        public ScreenType ScreenType => ScreenType.None;

        public void Draw(SpriteBatch spriteBatch)
        {
            createBlocks();
            foreach (var b in blocks)
            {
                if (b != null)
                {
                    b.Draw(spriteBatch);
                }
            }
        }

        public void Update(float delta, GameTime gameTime)
        {

        }
        private void createBlocks()
        {
            while (hasRun == false)
            {
                for (int i = 0; i < gameboard.GetLength(0); i++)
                {
                    for (int j = 0; j < gameboard.GetLength(1); j++)
                    {
                        if (gameboard[i, j] == 1)
                        {
                            Y = j * (_game.GraphicsDevice.Viewport.Height / 9);
                            X = i * (_game.GraphicsDevice.Viewport.Width / 16);
                            Debug.WriteLine(X + " " + Y);
                            blocks.Add(blockFactory.CreateBlock("NORMAL", X - (_game.GraphicsDevice.Viewport.Width / 16), Y - (_game.GraphicsDevice.Viewport.Height / 9), _game.GraphicsDevice));
                        }
                    }
                }
                hasRun = true;
            }
        }
    }
}
