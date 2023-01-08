using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using Project.Levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Characters
{
    class Mob : IGameObject, IMovable
    {
        public Texture2D _mob;
        public Texture2D _mobAttack;
        public Texture2D _mobWalk;
        public Texture2D _mobHurt;
        public Texture2D _mobDead;
        public Texture2D Selected { get; set; }
        private Animation animation;
        private Animation animationAttack;
        private Animation animationWalk;
        private Animation animationHurt;
        private Animation animationDead;

        public Texture2D blockTexture;
        private Game1 _game;

        public int hp = 100;
        public int damageTaken = 10;
        public int damage = 20;
        public bool hasTakenDamage = false;
        public bool isAttack = false;
        public bool hasDied = false;
        public bool died = false;

        private IInputReader InputReader;
        private Vector2 _position;
        private Vector2 _speed;
        public BasicAi basicAi;
        private float rotation;
        private Boundingbox _boundingBox;
        private Level level;
        private Character _character;
        private Collision collision;
        private List<Block> _blocks = new List<Block>();

        public ScreenType ScreenType => ScreenType.None;

        public bool isLeft { get; set; } = true;

        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Speed { get => _speed; set => _speed = value; }
        IInputReader IMovable.InputReader { get => null; set => InputReader = null; }


        public Mob(Texture2D[] mob, IInputReader inputReader,MovementController movementController, List<Block> blocks, Level l, Game1 game, Character character)
        {
            _character = character;
            _game = game;
            level = l;
            _mob = mob[0];
            _mobWalk = mob[1];
            _mobHurt = mob[3];
            _mobDead = mob[4];
            _mobAttack = mob[2];
            Selected = _mob;
            _position = new Vector2(200, game.GraphicsDevice.Viewport.Height - Selected.Height/10);
            _speed = new Vector2(1, 1);
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });
            this.basicAi = new BasicAi(_position);

            _boundingBox = new Boundingbox((int)Position.X, (int)Position.Y, 36, 35, blockTexture);

            collision = new Collision(_boundingBox._box, Speed);
            _blocks = blocks;

            animation = new Animation();
            animationWalk = new Animation();
            animationDead = new Animation();
            animationHurt = new Animation();
            animationAttack = new Animation();

            animation.GetFramesFromTextureProperties(_mob.Width, _mob.Height, 12, 1);
            animationWalk.GetFramesFromTextureProperties(_mobWalk.Width, _mobWalk.Height, 18, 1);
            animationHurt.GetFramesFromTextureProperties(_mobHurt.Width, _mobHurt.Height, 12, 1);
            animationDead.GetFramesFromTextureProperties(_mobDead.Width, _mobDead.Height, 15, 1);
            animationAttack.GetFramesFromTextureProperties(_mobAttack.Width, _mobAttack.Height, 12, 1);
            
        }

        public void Update(float delta, GameTime gameTime)
        {
            Move(gameTime);
            AnimationUpdate(gameTime);
            _boundingBox.Update((int)Position.X + (Selected.Width / 740), (int)Position.Y-5);
            DamageDetector();
            collision.Update(_boundingBox._box, new Vector2(this.Speed.X, this.Speed.Y));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _boundingBox.Draw(spriteBatch);
            MovementDraw(spriteBatch);
        }

        private void Move(GameTime gameTime)
        {
            basicAi.move(this, collision, _blocks, level, this, _character, _game, gameTime);
        }

        private void Flip()
        {
            if (basicAi.s == SpriteEffects.FlipHorizontally)
            {
                isLeft = false;
            }
            else
            {
                isLeft = true;
            }
        }

        private void AnimationUpdate(GameTime gameTime)
        {
            if (basicAi.rDir.X == 0 && !died)
            {
                animation.Update(gameTime);
            }
            if (basicAi.rDir.X == 1 || basicAi.rDir.X == -1 && !died) 
            {
                animationWalk.Update(gameTime);
            }
            if (isAttack && !hasDied && !died)
            {
                animationAttack.Update(gameTime);
            }
            if (damageTaken > 0 && !died)
            {
                animationHurt.Update(gameTime);
            }
            if(hp <= 0 && !died)
            {
                animationDead.Update(gameTime);
                hasDied = true;
            }

        }

        private void MovementDraw(SpriteBatch spriteBatch)
        {
            if (basicAi.rDir.X == 0 && damageTaken == 0 && !isAttack && !hasDied && !died)
            {
                Selected = _mob;
                spriteBatch.Draw(Selected, _position, animation.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 0.1f, basicAi.s, 0f);
                Flip();
            }
            if (basicAi.rDir.X == 1 || basicAi.rDir.X == -1 && damageTaken == 0 && !isAttack && !hasDied && !died)
            {
                Selected = _mobWalk;
                spriteBatch.Draw(Selected, _position, animationWalk.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 0.1f, basicAi.s, 0f);
                Flip();
            }
            if (damageTaken > 0 && !hasDied && !died)
            {
                Selected = _mobHurt;
                spriteBatch.Draw(Selected, _position, animationHurt.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 0.1f, basicAi.s, 0f);
                Flip();
            }
            if (isAttack && !hasDied && !died)
            {
                Selected = _mobAttack;
                spriteBatch.Draw(Selected, _position, animationAttack.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 0.1f, basicAi.s, 0f);
                Flip();
            }
            if(hasDied && !died)
            {
                Selected = _mobDead;
                spriteBatch.Draw(Selected, _position, animationDead.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 0.1f, basicAi.s, 0f);
                Flip();
                died = true;
            }
        }

        private void DamageDetector()
        {
            if (hasDied)
            {
                basicAi.rDir.X = 0; 
                damageTaken = 0;
                hp = 0;
                damage = 0;
            }
        }

        public void Damage(int d)
        {
            if (d > 0)
            {
                damageTaken = d;
                hp = hp - d;
                if (hp <= 0)
                {
                    hasDied = true;
                }
            }
            else
            {
                damageTaken = 0;
            }
        }
    }
}
