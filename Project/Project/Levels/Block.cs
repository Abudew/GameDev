using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Project.Levels
{
    class Block : IGameObject
    {
        public Rectangle box { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public BlockType Type { get;  set; }

        public ScreenType ScreenType => ScreenType.None;

        public Block(int x, int y, GraphicsDevice graphics, Texture2D[] tile)
        {
            box = new Rectangle(x, y, (graphics.Viewport.Width / 16)/2, (graphics.Viewport.Height / 9)/2);
            Passable = false;
            Color = Color.White;
            Type = BlockType.NORMAL;
            Texture = tile[0];
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, box, Color);
        }

        public void Update(float delta, GameTime gameTime)
        {
        }

    }

    class Exit : Block
    {
        public Exit(int x, int y, GraphicsDevice graphics, Texture2D[] tiles) : base(x, y, graphics, tiles)
        {
            box = new Rectangle(x, y, (graphics.Viewport.Width / 16) / 2, (graphics.Viewport.Height / 9) / 2);
            Passable = true;
            Type = BlockType.EXIT;
            Color = Color.White;
            Texture = tiles[2];
        }
    }

    class Trap : Block
    {
        public int damage { get; set; }
        public Trap(int x, int y, GraphicsDevice graphics, Texture2D[] tiles) : base(x, y, graphics, tiles)
        {
            Type = BlockType.TRAP;
            box = new Rectangle(x , y, (graphics.Viewport.Width / 16) / 2, (graphics.Viewport.Height / 9) / 2);
            Passable = true;
            Color = Color.White;
            damage = 100;
            Texture = tiles[1];
        }
    }
    class Slow : Block
    {
        public float slow { get; set; }
        public Slow(int x, int y, GraphicsDevice graphics, Texture2D[] tiles) : base(x, y, graphics, tiles)
        {
            box = new Rectangle(x, y, (graphics.Viewport.Width / 16) / 2, (graphics.Viewport.Height / 9) / 2);
            Passable = true;
            Type = BlockType.SLOW;
            Color = Color.White;
            slow = 2;
            Texture = new Texture2D(graphics, 1, 1);
            Texture.SetData(new[] { Color.White });
        }
    }
}