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
        private Character _character;

        public Boundingbox(int x, int y, int w, int h, Texture2D blokTexture, Character character)
        {
            _blokTexture = blokTexture;
            _character = character;
            _box = new Rectangle(x, y, w, h);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_blokTexture, _box, Color.Red);
        }

        public void Update(int x, int y)
        {
            if (_character.isLeft)
            {
                _box.X = x;
            } 
            else 
            {
                _box.X = x + _box.Width;
            }
            _box.Y = y + 16;
        }
    }
}
