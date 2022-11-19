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

        public void Move(IMovable movable, Collision collision, List<Rectangle> blockTexture, Game1 game)
        {
            var direction = movable.InputReader.ReadInput();
            KeyboardState state = Keyboard.GetState();
            var speed = movable.Speed;
            var position = movable.Position;

            #region move
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
            #endregion

            #region jump

            if (direction.Y <= 0 && isGrounded && !isJump)
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

            if (state.IsKeyUp(Keys.W) && isGoingUp && isJump && !isGrounded && position.Y < game.GraphicsDevice.Viewport.Height)
            {
                direction.Y = 1;
            }

            if (!isGoingUp && position.Y < game.GraphicsDevice.Viewport.Height)
            {
                direction.Y = 1;
            }

            if (isJump && !isGoingUp && direction.Y == 0)
            {
                isJump = false;
                isGrounded = true;
            }
            #endregion

            #region colisionDetection
            foreach (var block in blockTexture)
            {
                if (direction.X > 0 && collision.isTouchingLeft(block) || direction.X < 0 && collision.isTouchingRight(block))
                {
                    direction.X = 0;
                }
                if (direction.Y > 0 && collision.isTouchingTop(block) || direction.Y < 0 && collision.isTouchingBottom(block))
                {
                    direction.Y = 0;
                    isJump = false;
                    isGrounded = true;
                }
            }
            #endregion

            var afstand = direction * movable.Speed;
            movable.Position += afstand;
        }
    }
}
