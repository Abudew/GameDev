using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Controllers;
using Project.Interfaces;
using Project.Levels;
using Project.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks.Sources;
using System.Xml.Linq;

namespace Project
{
    class Character : IGameObject, IMovable
    {
        private Texture2D _character;
        private Texture2D _characterWalk;
        private Texture2D _characterRun;
        private Texture2D _characterJump;
        private Texture2D _characterDamage;
        public Texture2D Selected { get; set; }
        private Animation animation;
        private Animation animationWalk;
        private Animation animationRun;
        private Animation animationJump;
        private Animation animationDamage;

        public Texture2D blockTexture;
        private Game1 _game;

        public int lives = 3;
        public int hp = 100;
        public int damageTaken = 0;
        private int damage = 0;
        public bool hasTakenDamage = false;
        public bool hasDied = false;
        public bool gameOver = false;

        private Vector2 _position;
        private Vector2 _speed;
        private IInputReader InputReader;
        public MovementController _movementController;
        private float rotation;
        private Boundingbox _boundingBox;
        private Level level;
        private Collision collision;
        private List<Block> _blocks = new List<Block>();

        public IMovable move { get => this; }
        public bool isLeft { get; set; } = true;

        public Character(Texture2D[] character, IInputReader inputReader, MovementController movementController, List<Block> blocks, Level l, Game1 game)
        {
            _game = game;
            level = l;
            _character = character[0];
            _characterWalk = character[1];
            _characterRun = character[2];
            _characterJump = character[3];
            _characterDamage = character[4];
            Selected = _character;
            InputReader = inputReader;
            _movementController = movementController;
            _position = new Vector2(0, game.GraphicsDevice.Viewport.Height - Selected.Height*2);
            _speed = new Vector2(4,8);
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });
            if (Selected == _character)
            {
                _boundingBox = new Boundingbox((int)Position.X, (int)Position.Y, ((Selected.Width / 4)/2) - ((Selected.Width / 4) / 8), Selected.Height - (Selected.Height / 3), blockTexture, this);
            }
            else if (Selected == _characterWalk || Selected == _characterRun || Selected == _characterJump)
            {
                _boundingBox = new Boundingbox((int)Position.X, (int)Position.Y, ((Selected.Width / 6)/2) - ((Selected.Width / 6) / 8), Selected.Height - (Selected.Height / 3), blockTexture, this);
            }
            else if(Selected == _characterDamage)
            {
                _boundingBox = new Boundingbox((int)Position.X, (int)Position.Y, ((Selected.Width / 3) / 2) - ((Selected.Width / 3) / 8), Selected.Height - (Selected.Height / 3), blockTexture, this);
            }

            collision = new Collision(_boundingBox._box, Speed);
            _blocks = blocks;

            animation = new Animation();
            animationWalk = new Animation();
            animationRun = new Animation();
            animationJump = new Animation();
            animationDamage = new Animation();

            animation.GetFramesFromTextureProperties(_character.Width, _character.Height, 4, 1);
            animationWalk.GetFramesFromTextureProperties(_characterWalk.Width, _characterWalk.Height, 6, 1);
            animationRun.GetFramesFromTextureProperties(_characterRun.Width, _characterRun.Height, 6, 1);
            animationJump.GetFramesFromTextureProperties(_characterJump.Width, _characterJump.Height, 6, 1);
            animationDamage.GetFramesFromTextureProperties(_characterDamage.Width, _characterDamage.Height, 3, 1);
        }

        public ScreenType ScreenType => ScreenType.None;

        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Speed { get => _speed; set => _speed = value; }
        IInputReader IMovable.InputReader { get => InputReader; set => InputReader = value; }

        public void Draw(SpriteBatch spriteBatch)
        {
            _boundingBox.Draw(spriteBatch);
            MovementDraw(spriteBatch);
        }

        public void Update(float delta, GameTime gameTime)
        {
            Move();
            AnimationUpdate(gameTime);
            if (Selected == _character)
            {
                _boundingBox.Update((int)Position.X + (Selected.Width / 32), (int)Position.Y);
            }
            else if (Selected == _characterWalk || Selected == _characterRun || Selected == _characterJump)
            {
                _boundingBox.Update((int)Position.X + (Selected.Width / 48), (int)Position.Y);
            }
            else if (Selected == _characterDamage)
            {
                _boundingBox.Update((int)Position.X + (Selected.Width / 16), (int)Position.Y);
            }
            DamageDetector();
            collision.Update(_boundingBox._box, new Vector2(this.Speed.X * this.InputReader.ReadInput().X, this.Speed.Y));
        }

        #region Methods
        private void Move()
        {
            _movementController.Move(this, collision, _blocks, level, this,_game);
        }

        private void Flip()
        {
            if (_movementController.s == SpriteEffects.FlipHorizontally)
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
            if (_movementController.rDir.X == 0 && _movementController.rDir.Y == 0)
            {
                animation.Update(gameTime);
            }
            if (_movementController.rDir.X == 1 && _movementController.rDir.Y == 0 || _movementController.rDir.X == -1 && _movementController.rDir.Y == 0)
            {
                animationWalk.Update(gameTime);
            }
            if (_movementController.rDir.X == 2 && _movementController.rDir.Y == 0 || _movementController.rDir.X == -2 && _movementController.rDir.Y == 0)
            {
                animationRun.Update(gameTime);
            }
            if (_movementController.rDir.Y != 0)
            {
                animationJump.Update(gameTime);
            }
            if (damageTaken > 0)
            {
                animationDamage.Update(gameTime);
            }
        }

        private void MovementDraw(SpriteBatch spriteBatch)
        {
            if (_movementController.rDir.X == 0 && _movementController.rDir.Y == 0 && damageTaken == 0)
            {
                Selected = _character;
                spriteBatch.Draw(Selected, _position, animation.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 1f, _movementController.s, 0f);
                Flip();
            }
            if (_movementController.rDir.X == 1 && _movementController.rDir.Y == 0 || _movementController.rDir.X == -1 && _movementController.rDir.Y == 0 && damageTaken == 0)
            {
                Selected = _characterWalk;
                spriteBatch.Draw(Selected, _position, animationWalk.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 1f, _movementController.s, 0f);
                Flip();
            }
            if (_movementController.rDir.X == 2 && _movementController.rDir.Y == 0 || _movementController.rDir.X == -2 && _movementController.rDir.Y == 0 && damageTaken == 0)
            {
                Selected = _characterRun;
                spriteBatch.Draw(Selected, _position, animationRun.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 1f, _movementController.s, 0f);
                Flip();
            }
            if (_movementController.rDir.Y != 0 && damageTaken == 0)
            {
                Selected = _characterJump;
                spriteBatch.Draw(Selected, _position, animationJump.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 1f, _movementController.s, 0f);
                Flip();
            }
            if(damageTaken > 0)
            {
                Selected = _characterDamage;
                spriteBatch.Draw(Selected, _position, animationDamage.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 1f, _movementController.s, 0f);
                Flip();
            }
        }

        private void DamageDetector()
        {
            if (damageTaken > 0)
            {
                hp = hp - damageTaken;
                if (hp <= 0)
                {
                    hasDied = true;
                    
                }
            }

            if (hasDied)
            {
                Position = new Vector2(0, _game.GraphicsDevice.Viewport.Height - this.Selected.Height * 2);
                damageTaken = 0;
                hp = 101;
                lives--;
                if (lives <= 0)
                {
                    level.level = 1;
                    level.hasRun = false;
                    gameOver = true;
                }
                hasDied = false;
            }
            if (gameOver)
            {
                _game.isSwitch = true;
                _game.screenSelection = ScreenType.MainMenu;
                lives = 3;
                hp = 101;
                gameOver = false;
            }
        }
        #endregion
    }
}
