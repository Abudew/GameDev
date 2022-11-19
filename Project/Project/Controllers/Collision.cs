using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controllers
{
    internal class Collision
    {
        private Rectangle _boxObj;
        private Vector2 v;

        public Collision(Rectangle box, Vector2 velocity)
        {
            _boxObj = box;
            v = velocity;
        }

        public ScreenType ScreenType => ScreenType.None;

        public void Update(Rectangle box, Vector2 velocity)
        {
            _boxObj = box;
            v = velocity;
        }

        public bool isTouchingLeft(Rectangle box)
        {
            return _boxObj.Right + v.X >= box.Left &&
                _boxObj.Left <= box.Left &&
                _boxObj.Bottom >= box.Top &&
                _boxObj.Top <= box.Bottom;
        }
        public bool isTouchingRight(Rectangle box)
        {
            return _boxObj.Left - v.X <= box.Right &&
                _boxObj.Right >= box.Right &&
                _boxObj.Bottom >= box.Top &&
                _boxObj.Top <= box.Bottom;
        }
        public bool isTouchingTop(Rectangle box)
        {
            return _boxObj.Bottom + v.Y > box.Top &&
                _boxObj.Top < box.Top &&
                _boxObj.Right > box.Left &&
                _boxObj.Left < box.Right;
        }
        public bool isTouchingBottom(Rectangle box)
        {
            return _boxObj.Top - v.Y < box.Bottom &&
                _boxObj.Bottom > box.Top &&
                _boxObj.Right > box.Left &&
                _boxObj.Left < box.Right;
        }
    }
}
