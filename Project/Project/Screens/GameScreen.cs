using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Characters;
using Project.Controllers;
using Project.Interfaces;
using Project.Levels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    internal class GameScreen : IGameObject
    {
        private Character _character;
        private Mob _mob;
        private Mob _mob2;
        private Mob _mob3;
        private Mob _mob4; 
        private Mob _mob5;
        private Mob _mob6;
        private Mob _mob7;
        private Game1 _game;
        private Level level;
        private MovementController movementController;
        private Texture2D blockTexture;
        private double HP;
        private double oneHp;

        private float timer = 1;
        private const float TIMER = 1;

        private double HPBoss;
        private double oneHpBoss;
        private int x;
        private bool hasRun = false;
        public ScreenType ScreenType => ScreenType.Game;

        public GameScreen(Texture2D[] characters, Texture2D[] mobs, Texture2D[] tiles, Game1 game)
        {
            movementController = new MovementController();
            _game = game;
            x = _game.GraphicsDevice.Viewport.Width / 4;
            level = new Level(game, tiles, movementController);
            _character = new Character(characters, new KeyboardController(), movementController, level.blocks, level, _game);
            _mob = new Mob(mobs, null, movementController, level.blocks, level, _game, _character);
            _mob2 = new Mob(mobs, null, movementController, level.blocks, level, _game, _character);
            _mob3 = new Mob(mobs, null, movementController, level.blocks, level, _game, _character);
            _mob4 = new Mob(mobs, null, movementController, level.blocks, level, _game, _character);
            _mob5 = new Mob(mobs, null, movementController, level.blocks, level, _game, _character);
            _mob6 = new Mob(mobs, null, movementController, level.blocks, level, _game, _character);
            _mob7 = new Mob(mobs, null, movementController, level.blocks, level, _game, _character);
            oneHp = ((_game.GraphicsDevice.Viewport.Width / 2) / 100) + 0.4;
            oneHpBoss = ((_game.GraphicsDevice.Viewport.Width / 2) / 200) + 0.2;
            HP = (_game.GraphicsDevice.Viewport.Width / 2);
            HPBoss = (_game.GraphicsDevice.Viewport.Width / 2);
            blockTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            blockTexture.SetData(new[] { Color.White });

            _mob3._mob = mobs[5];
            _mob3._mobAttack = mobs[7];
            _mob3._mobWalk = mobs[6];
            _mob3._mobHurt = mobs[8];
            _mob3._mobDead = mobs[9];

            _mob4._mob = mobs[5];
            _mob4._mobAttack = mobs[7];
            _mob4._mobWalk = mobs[6];
            _mob4._mobHurt = mobs[8];
            _mob4._mobDead = mobs[9];

            _mob5._mob = mobs[5];
            _mob5._mobAttack = mobs[7];
            _mob5._mobWalk = mobs[6];
            _mob5._mobHurt = mobs[8];
            _mob5._mobDead = mobs[9];

            _mob6._mob = mobs[5];
            _mob6._mobAttack = mobs[7];
            _mob6._mobWalk = mobs[6];
            _mob6._mobHurt = mobs[8];
            _mob6._mobDead = mobs[9];

            _mob7._mob = mobs[10];
            _mob7._mobAttack = mobs[12];
            _mob7._mobWalk = mobs[11];
            _mob7._mobHurt = mobs[13];
            _mob7._mobDead = mobs[14];

            _mob2.Position = new Vector2(900, game.GraphicsDevice.Viewport.Height - 48);
            _mob3.Position = new Vector2(900, game.GraphicsDevice.Viewport.Height - 48);
            _mob4.Position = new Vector2(1000, game.GraphicsDevice.Viewport.Height - 48);
            _mob5.Position = new Vector2(1200, game.GraphicsDevice.Viewport.Height - 48);
            _mob6.Position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height - 48);

            _mob7.damage = 50;
            _mob7.hp = 200;
            _mob7.Position = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height - 48);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_game.backGround[1], new Rectangle(0, 0, _game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height), Color.White);
            level.Draw(spriteBatch);
            _character.Draw(spriteBatch);
            if(level.level == 1)
            {
                _mob.Draw(spriteBatch);
                _mob2.Draw(spriteBatch);
            }
            if(level.level == 2)
            {
                _mob3.Draw(spriteBatch);
                _mob4.Draw(spriteBatch);
                _mob5.Draw(spriteBatch);
                _mob6.Draw(spriteBatch);
            }
            if (level.level == 3)
            {
                _mob7.Draw(spriteBatch);
            }

            spriteBatch.Draw(blockTexture, new Rectangle(_game.GraphicsDevice.Viewport.Width / 4, 0, (int)HP, 20), Color.Red);
            spriteBatch.Draw(blockTexture, new Rectangle(_game.GraphicsDevice.Viewport.Width / 4, 20, _game.GraphicsDevice.Viewport.Width / 2, 1), Color.Black);

            if(level.level == 3)
            {
                spriteBatch.Draw(blockTexture, new Rectangle(_game.GraphicsDevice.Viewport.Width / 4, _game.GraphicsDevice.Viewport.Height / 4, (int)HPBoss, 20), Color.Red);
                spriteBatch.Draw(blockTexture, new Rectangle(_game.GraphicsDevice.Viewport.Width / 4, (_game.GraphicsDevice.Viewport.Height / 4) + 20, _game.GraphicsDevice.Viewport.Width / 2, 1), Color.Black);
            }
            while (hasRun == false)
            {
                for (int i = 0; i < _character.lives; i++)
                {
                    spriteBatch.Draw(blockTexture, new Rectangle(x, 25, 10, 10), Color.Red);
                    x = x + 15;
                }
                hasRun = true;
            }
        }

        public void Update(float delta, GameTime gameTime) 
        {
            level.Update(delta, gameTime);
            _character.Update(delta, gameTime);
            gameOver();
            if (level.level == 1)
            {
                _mob.Update(delta, gameTime);
                _mob2.Update(delta, gameTime);
            }
            if (level.level == 2)
            {
                _mob3.Update(delta, gameTime);
                _mob4.Update(delta, gameTime);
                _mob5.Update(delta, gameTime);
                _mob6.Update(delta, gameTime);
            }
            if (level.level == 3)
            {
                _mob7.Update(delta, gameTime);

                if (_mob7.hasDied)
                {

                    var time = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000f);

                    timer -= time;
                    if (timer <= 0)
                    {
                        _character.gameOver = true;
                        level.level = 1;
                        level.hasRun = false;
                        timer = TIMER;
                    }
                }
            }
            x = _game.GraphicsDevice.Viewport.Width / 4;
            hasRun = false;
            HP = oneHp * _character.hp;
            HPBoss = oneHpBoss * _mob7.hp;
        }

        public void gameOver()
        {
            if (_character.gameOver)
            {
                _mob.hasDied = false;
                _mob.died = false;
                _mob.hp = 100;
                _mob.damage = 20;

                _mob2.hasDied = false;
                _mob2.died = false;
                _mob2.hp = 100;
                _mob2.damage = 20;

                _mob3.hasDied = false;
                _mob3.died = false;
                _mob3.hp = 100;
                _mob3.damage = 20;

                _mob4.hasDied = false;
                _mob4.died = false;
                _mob4.hp = 100;
                _mob4.damage = 20;

                _mob5.hasDied = false;
                _mob5.died = false;
                _mob5.hp = 100;
                _mob5.damage = 20;

                _mob6.hasDied = false;
                _mob6.died = false;
                _mob6.hp = 100;
                _mob6.damage = 20;

                _mob7.hasDied = false;
                _mob7.died = false;
                _mob7.hp = 200;
                _mob7.damage = 50;

                _character.gameOver = false;
            }
        }
    }
}
