using Microsoft.Xna.Framework;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Characters
{
    internal class Collision
    {
        private Boundingbox _boundingbox;
        private Vector2 velocity;

        protected bool isTouchingLeft(Rectangle box)
        {
            return _boundingbox._box.Right + velocity.X > box.Left &&
                _boundingbox._box.Left < box.Left &&
                _boundingbox._box.Bottom > box.Top &&
                _boundingbox._box.Top < box.Bottom;
        }
        protected bool isTouchingRight(Rectangle box)
        {
            return _boundingbox._box.Left + velocity.X < box.Right &&
                _boundingbox._box.Right > box.Right &&
                _boundingbox._box.Bottom > box.Top &&
                _boundingbox._box.Top < box.Bottom;
        }
        protected bool isTouchingTop(Rectangle box)
        {
            return _boundingbox._box.Bottom + velocity.Y > box.Top &&
                _boundingbox._box.Top < box.Top &&
                _boundingbox._box.Right > box.Left &&
                _boundingbox._box.Left < box.Right;
        }
        protected bool isTouchingBottom(Rectangle box)
        {
            return _boundingbox._box.Top + velocity.Y < box.Bottom &&
                _boundingbox._box.Bottom > box.Top &&
                _boundingbox._box.Right > box.Left &&
                _boundingbox._box.Left < box.Right;
        }
    }
}
