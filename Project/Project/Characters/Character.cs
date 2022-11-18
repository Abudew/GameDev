using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using Project.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Project
{
    class Character : IGameObject, IMovable
    {
        private Texture2D _character;
        private Texture2D _characterRun;
        private Animation animation;
        private Animation animationRun;
        private Vector2 _direction;
        private Vector2 _position;
        private Vector2 _speed;
        private IInputReader InputReader;
        private MovementController _movementController;
        private float rotation;

        public bool isLeft { get; set; } = true;

        public Character(Texture2D character, Texture2D characterRun, IInputReader inputReader, MovementController movementController)
        {
            _character = character;
            _characterRun = characterRun;
            InputReader = inputReader;
            _movementController = movementController;
            _direction = inputReader.ReadInput();
            _position = new Vector2(10, 300);
            _speed = new Vector2(5, 10f);            

            animation = new Animation();
            animationRun = new Animation();
            animation.GetFramesFromTextureProperties(_character.Width, _character.Height, 4, 1);
            animationRun.GetFramesFromTextureProperties(_characterRun.Width, _characterRun.Height, 6, 1);
        }

        public ScreenType ScreenType => ScreenType.None;

        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Speed { get => _speed; set => _speed = value; }
        IInputReader IMovable.InputReader { get => InputReader; set => InputReader = value; }

        public void Draw(SpriteBatch spriteBatch)
        {
            Debug.WriteLine(animation.CurrentFrame.SourceRectangle);
            if (_movementController.rDir.X == 0)
            {
                spriteBatch.Draw(_character, _position, animation.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 1f, _movementController.s, 0f);
                if(_movementController.s == SpriteEffects.FlipHorizontally)
                {
                    isLeft = false;
                } 
                else
                {
                    isLeft = true;
                }
            }
            if (_movementController.rDir.X != 0)
            {
                spriteBatch.Draw(_characterRun, _position, animationRun.CurrentFrame.SourceRectangle, Color.White, rotation, new Vector2(0, 0), 1f, _movementController.s, 0f);
                if (_movementController.s == SpriteEffects.FlipHorizontally)
                {
                    isLeft = false;
                }
                else
                {
                    isLeft = true;
                }
            }
        }

        public void Update(float delta, GameTime gameTime)
        {
            Move();
            if (_movementController.rDir.X == 0)
            {
                animation.Update(gameTime);
            }
            if (_movementController.rDir.X != 0)
            {
                animationRun.Update(gameTime);
            }
        }

        private void Move()
        {
            _movementController.Move(this);
        }
    }
}
