using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Project.Interfaces;

namespace Project.Controllers
{
    class MovementController
    {
        private bool goR = true;
        public SpriteEffects s;
        public Vector2 rDir;
        private bool isJump = false;
        private bool isGrounded = true;
        private bool isGoingUp = false;
        private float beginPos = 0;
        public void Move(IMovable movable)
        {
            var direction = movable.InputReader.ReadInput();
            KeyboardState state = Keyboard.GetState();
            var speed = movable.Speed;
            var position = movable.Position;

            if (direction.X < 0)
            {
                s = SpriteEffects.FlipHorizontally;
                goR = false;
                rDir.X = direction.X;
            }
            else if (direction.X > 0)
            {
                s = SpriteEffects.None;
                goR = true;
                rDir.X = direction.X;
            }
            else
            {
                rDir.X = 0;
                if (goR)
                {
                    s = SpriteEffects.None;
                }
                else
                {
                    s = SpriteEffects.FlipHorizontally;
                }
            }

            if (direction.Y < 0 && isGrounded && !isJump)
            {
                isJump = true;
                isGrounded = false;
                isGoingUp = true;

                beginPos = position.Y;
                direction.Y = -1;
            }

            if (beginPos - position.Y >= 100 && isGoingUp)
            {
                isGoingUp = false;
            }

            if (state.IsKeyUp(Keys.W) && isGoingUp && isJump && !isGrounded && position.Y < 300)
            {
                direction.Y = 1;
            }

            if (!isGoingUp && position.Y < 300)
            {
                direction.Y = 1;
            }

            if (isJump && !isGoingUp && position.Y == 300)
            {
                isJump = false;
                isGrounded = true;
            }

            var afstand = direction * movable.Speed;
            movable.Position += afstand;
        }
    }
}
