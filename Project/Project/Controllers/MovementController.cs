using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Project.Interfaces;
using Project.Levels;
using Microsoft.VisualBasic;
using System.Threading;
using Project.Characters;

namespace Project.Controllers
{
    class MovementController
    {
        private bool goR = true;
        private Trap trap;
        public SpriteEffects s;
        public Vector2 rDir;
        private bool isJump = false;
        private bool isGrounded = true;
        private bool isGoingUp = false;
        private float beginPos = 0;
        public bool hasRun { get; set; } = false;

        public void Move(IMovable movable, Collision collision, List<Block> blockTexture, Level l, Character character, Game1 game)
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
                    character._boundingBox._box.X = character._boundingBox._box.X + character._boundingBox._box.Width;
                }
            }
            if (direction.Y < 0 || direction.Y < 0)
            {
                rDir.Y = direction.Y;
            }
            else
            {
                rDir.Y = 0;
            }
            #endregion

            #region jump

            if (!isJump && !isGoingUp && isGrounded)
            {
                direction.Y = 0;
            }

            if (direction.Y <= 0 && isGrounded && !isJump)
            {
                isJump = true;
                isGrounded = false;
                isGoingUp = true;

                beginPos = position.Y;
                direction.Y = -1;
            }

            if (beginPos - position.Y >= ((game.GraphicsDevice.Viewport.Height / 9)*3) && isGoingUp)
            {
                isGoingUp = false;
            }

            if (state.IsKeyUp(Keys.W) && isGoingUp && isJump && !isGrounded && position.Y < game.GraphicsDevice.Viewport.Height)
            {
                isJump = false;
                isGoingUp = false;
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
                if (direction.X > 0 && collision.isTouchingLeft(block.box) && !block.Passable)
                {
                    direction.X = 0;
                }
                if(direction.X < 0 && collision.isTouchingRight(block.box) && !block.Passable)
                {
                    direction.X = 0;
                }
                if (direction.Y > 0 && collision.isTouchingTop(block.box) && !block.Passable)
                {
                    direction.Y = 0;
                    isJump = false;
                    isGoingUp = false;
                    isGrounded = true;
                }
                if (direction.Y < 0 && collision.isTouchingBottom(block.box) && isGoingUp && !block.Passable)
                {
                    direction.Y = 0;
                    isJump = false;
                    isGoingUp = false;
                }
                if (collision.isTouchingBottom(block.box) || collision.isTouchingTop(block.box) || collision.isTouchingLeft(block.box) || collision.isTouchingRight(block.box))
                {
                    if (block.Passable && block.Type == BlockType.EXIT)
                    {
                        while (hasRun == false)
                        {
                            direction = movable.InputReader.ReadInput();
                            character.Position = new Vector2(0, game.GraphicsDevice.Viewport.Height - character.Selected.Height*2);
                            l.level++;
                            l.hasRun = false;
                            hasRun = true;
                        }
                    }
                    if (block.Passable && block.Type == BlockType.TRAP)
                    {
                        trap = (Trap)block;
                        direction = new Vector2(0,0);
                        character.setDamage(trap.damage);
                    }
                    else
                    {
                        character.damageTaken = 0;
                    }
                }
                if (!collision.isTouchingBottom(block.box) || !collision.isTouchingTop(block.box) || !collision.isTouchingLeft(block.box) || !collision.isTouchingRight(block.box))
                {
                    hasRun = false;
                }
            }
            #endregion

            var afstand = direction * speed;
            movable.Position += afstand;
        }
    }
}
