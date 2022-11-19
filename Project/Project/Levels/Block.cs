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

        public ScreenType ScreenType => ScreenType.None;

        public Block(int x, int y, GraphicsDevice graphics)
        {
            box = new Rectangle(x, y, graphics.Viewport.Width / 16, graphics.Viewport.Height / 9);
            Passable = false;
            Color = Color.Green;
            Texture = new Texture2D(graphics, 1, 1);
            Texture.SetData(new[] { Color.White });
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, box, Color);
        }

        public void Update(float delta, GameTime gameTime)
        {
        }

    }
    
    class Trap : Block
    {
        public int damage { get; set; }
        public Trap(int x, int y, GraphicsDevice graphics) : base(x, y, graphics)
        {
            box = new Rectangle(x, y, graphics.Viewport.Width / 16, graphics.Viewport.Height / 9);
            Passable = true;
            Color = Color.Red;
            damage = 10;
            Texture = new Texture2D(graphics, 1, 1);
        }
    }
    class Slow : Block
    {
        public float slow { get; set; }
        public Slow(int x, int y, GraphicsDevice graphics) : base(x, y, graphics)
        {
            box = new Rectangle(x, y, graphics.Viewport.Width / 16, graphics.Viewport.Height / 9);
            Passable = true;
            Color = Color.Blue;
            slow = 2;
            Texture = new Texture2D(graphics, 1, 1);
        }
    }
}