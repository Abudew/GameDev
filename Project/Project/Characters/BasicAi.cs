using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using Project.Levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Project.Characters
{
    class BasicAi
    {
        public Vector2 pos;
        public Vector2 rDir;
        public SpriteEffects s;
        private bool goR = true;
        private Trap trap;
        private bool isJump = false;
        private bool isGrounded = true;
        private bool isGoingUp = false;
        private bool WalkRight = true;
        private bool WalkLeft = false;
        private float beginPos = 0;
        float timer = 2;
        const float TIMER = 2;
        public bool hasRun { get; set; } = false;
        public BasicAi(Vector2 position)
        {
            pos = position;
        }

        public void move(IMovable movable, Collision collision, List<Block> blockTexture, Level l, Mob mob, Character character, Game1 game, GameTime gameTime)
        {
            Vector2 direction = new Vector2(0,1);
            var position = movable.Position;

            #region colisionDetection
            if (collision.isTouchingLeft(character._boundingBox._box) )
            {
                WalkLeft = false;
                WalkRight = true;
                mob.isAttack = true;
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed;
                character.setDamage(0);
                if (timer < 0)
                {
                    character.setDamage(mob.damage);

                    if (character.isLightAttack || character.isLightAttack)
                    {
                        mob.Damage(character.damage);
                    }
                    timer = TIMER;
                }
            }
            if (collision.isTouchingRight(character._boundingBox._box))
            {
                WalkRight = false;
                WalkLeft = true;
                mob.isAttack = true;
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed;
                character.setDamage(0);
                if (timer < 0)
                {
                    character.setDamage(mob.damage);
                    if (character.isLightAttack || character.isHeavyAttack)
                    {
                        mob.Damage(character.damage);
                    }
                    timer = TIMER;
                }
            }
            else
            {
                mob.isAttack = false;
                character.damageTaken = 0;
            }


            foreach (var block in blockTexture)
            {
                if(WalkRight && !mob.isAttack)
                {
                    direction.X = 1;
                    rDir.X = direction.X;
                    goR = true;
                    s = SpriteEffects.None;
                }
                else if(WalkLeft && !mob.isAttack)
                {
                    direction.X = -1;
                    rDir.X = direction.X;
                    goR = false;
                    s = SpriteEffects.FlipHorizontally;
                }
                else if (mob.isAttack)
                {
                    direction.X = 0;
                    rDir.X = direction.X;
                }

                if (direction.X == 1 && collision.isTouchingLeft(block.box) && !block.Passable)
                {
                    WalkRight = false;
                    WalkLeft = true;
                }
                if (direction.X == -1 && collision.isTouchingRight(block.box) && !block.Passable)
                {
                    WalkLeft = false;
                    WalkRight = true;
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
                    if (block.Passable && block.Type == BlockType.TRAP)
                    {
                        trap = (Trap)block;
                        mob.damageTaken = trap.damage;
                    }
                    else
                    {
                        mob.damageTaken = 0;
                    }
                }
                if (!collision.isTouchingBottom(block.box) || !collision.isTouchingTop(block.box) || !collision.isTouchingLeft(block.box) || !collision.isTouchingRight(block.box))
                {
                    hasRun = false;
                }
            }
            #endregion

            movable.Position += direction;
        }
    }
}
