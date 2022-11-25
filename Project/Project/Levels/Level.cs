﻿using Microsoft.Xna.Framework;
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
        public bool hasRun { get; set; } = false;
        private int X = 0;
        private int Y = 0;
        public int level { get; set; } = 1;
        private static int[,] gameboard = new int[34,20];
        private static int[,] level1 = new int[34, 20];
        private static int[,] level2 = new int[34, 20];
        private static int[,] level3 = new int[34, 20];
        private static int[,] level4 = new int[34, 20];

        public List<Block> blocks = new List<Block>();
        public BlockFactory blockFactory = new BlockFactory();
        private MovementController _movementController;

        public Level(Game1 game, MovementController movementController)
        {
            _game = game;
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.Black });
            _movementController = movementController;
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
            Random random = new Random();
            while (hasRun == false)
            {
                blocks.Clear();
                for (int i = 0; i < gameboard.GetLength(0); i++)
                {
                    for (int j = 0; j < gameboard.GetLength(1); j++)
                    {
                        gameboard[0, j] = 1;
                        gameboard[i, 0] = 1;
                        gameboard[i, 19] = 1;
                        gameboard[33, j] = 1;

                        for (int l = 7; l < 10; l++)
                        {
                            for (int h = 15; h < 16; h++)
                            {
                                level1[l, h] = 1;
                            }
                        }

                        level1[32, 18] = 2;
                        level2[32, 18] = 2;
                        level3[32, 18] = 2;
                        level4[32, 18] = 2;
                    }
                }

                for (int i = 0; i < gameboard.GetLength(0); i++)
                {
                    for (int j = 0; j < gameboard.GetLength(1); j++)
                    {
                        if (gameboard[i, j] == 1)
                        {
                            Y = j * (_game.GraphicsDevice.Viewport.Height / 9)/2;
                            X = i * (_game.GraphicsDevice.Viewport.Width / 16)/2;
                            blocks.Add(blockFactory.CreateBlock("NORMAL", X - (_game.GraphicsDevice.Viewport.Width / 16)/2, Y - (_game.GraphicsDevice.Viewport.Height / 9)/2, _game.GraphicsDevice));
                        }

                        switch (level)
                        {
                            case 1:
                                _movementController.hasRun = false;
                                if (level1[i, j] == 1)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("NORMAL", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                if (level1[i, j] == 2)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("EXIT", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                break;
                            case 2:
                                _movementController.hasRun = false;
                                if (level2[i, j] == 1)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("NORMAL", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                if (level2[i, j] == 2)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("EXIT", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                break;
                            case 3:
                                _movementController.hasRun = false;
                                if (level3[i, j] == 1)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("NORMAL", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                if (level3[i, j] == 2)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("EXIT", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                break;
                            case 4:
                                _movementController.hasRun = false;
                                if (level4[i, j] == 1)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("NORMAL", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                if (level4[i, j] == 2)
                                {
                                    Y = j * (_game.GraphicsDevice.Viewport.Height / 9) / 2;
                                    X = i * (_game.GraphicsDevice.Viewport.Width / 16) / 2;
                                    blocks.Add(blockFactory.CreateBlock("EXIT", X - (_game.GraphicsDevice.Viewport.Width / 16) / 2, Y - (_game.GraphicsDevice.Viewport.Height / 9) / 2, _game.GraphicsDevice));
                                }
                                break;
                            default:
                                level = 1;
                                _game.isSwitch = true;
                                _game.screenSelection = ScreenType.MainMenu;
                                break;
                        }
                    }
                }
                hasRun = true;
            }
        }
    }
}
