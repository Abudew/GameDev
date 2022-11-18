using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Levels
{
    class Block : IGameObject
    {
        public Rectangle BoundingBox { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }

        public ScreenType ScreenType => ScreenType.None;

        //public CollideWithEvent CollideWithEvent { get; set; }

        public Block(int x, int y, GraphicsDevice graphics)
        {
            BoundingBox = new Rectangle(x, y, 10, 10);
            Passable = false;
            Color = Color.Green;
            Texture = new Texture2D(graphics, 1, 1);
            //CollideWithEvent = new NoEvent();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color);
        }

        public void Update(float delta, GameTime gameTime)
        {
            
        }
        /*public virtual void IsCollidedWithEvent
(Character collider)
{
   CollideWithEvent.Execute();
}*/

    }
}