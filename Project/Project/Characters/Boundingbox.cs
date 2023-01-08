using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Characters
{
    class Boundingbox
    {
        public Texture2D _blokTexture;
        public Rectangle _box;

        public Boundingbox(int x, int y, int w, int h, Texture2D blokTexture)
        {
            _blokTexture = blokTexture;
            _box = new Rectangle(x, y, w, h);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_blokTexture, _box, Color.Transparent);
        }

        public void Update(int x, int y)
        {
            _box.X = x;
            _box.Y = y + 16;
        }
    }
}
