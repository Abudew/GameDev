 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Controllers;
using Project.Interfaces;
using Project.Screens;
using System;
using System.Threading;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public Texture2D[] woodcutters { get; set; }
        public Texture2D[] backGround { get; set; }
        public Texture2D[] mobs { get; set; }
        public Texture2D[] tiles { get; set; }
        public bool isSwitch { get; set; } = false;
        public ScreenType screenSelection { get; set; }
        private float timer = 3;
        private const float TIMER = 3;
        private ScreenController _screenController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            //_graphics.IsFullScreen = true;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var splashImage = Content.Load<Texture2D>("Splash");
            var GameOverImage = Content.Load<Texture2D>("Gameover");
            var button = Content.Load<Texture2D>("Button");
            var buttonHover = Content.Load<Texture2D>("ButtonHover");
            var buttonFont = Content.Load<SpriteFont>("Font");

            var tile = Content.Load<Texture2D>("Tile");
            var spikes = Content.Load<Texture2D>("spikes");
            var exit = Content.Load<Texture2D>("Exit");

            Texture2D woodcutter = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_idle");
            Texture2D woodcutterWalk = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_walk");
            Texture2D woodcutterRun = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_run");
            Texture2D woodcutterJump = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_jump");
            Texture2D woodcutterDamage = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_hurt");
            Texture2D woodcutterLight = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_attack2");
            Texture2D woodcutterHeavy = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_attack1");

            Texture2D background1 = Content.Load<Texture2D>("game_background_1");
            Texture2D background2 = Content.Load<Texture2D>("game_background_2");
            Texture2D background3 = Content.Load<Texture2D>("game_background_3. 2");
            Texture2D background4 = Content.Load<Texture2D>("game_background_4");

            Texture2D Mob1 = Content.Load<Texture2D>("Golems/Golem_01/PNG Sequences/Idle/css_sprites");
            Texture2D Mob1Walk = Content.Load<Texture2D>("Golems/Golem_01/PNG Sequences/Walking/css_sprites(4)");
            Texture2D Mob1Attack = Content.Load<Texture2D>("Golems/Golem_01/PNG Sequences/Attacking/css_sprites(1)");
            Texture2D Mob1Hurt = Content.Load<Texture2D>("Golems/Golem_01/PNG Sequences/Hurt/css_sprites(3)");
            Texture2D Mob1Dead = Content.Load<Texture2D>("Golems/Golem_01/PNG Sequences/Dying/css_sprites(2)");

            Texture2D Mob2 = Content.Load<Texture2D>("Golems/Golem_02/PNG Sequences/Idle/css_sprites(8)");
            Texture2D Mob2Walk = Content.Load<Texture2D>("Golems/Golem_02/PNG Sequences/Walking/css_sprites(9)");
            Texture2D Mob2Attack = Content.Load<Texture2D>("Golems/Golem_02/PNG Sequences/Attacking/css_sprites(5)");
            Texture2D Mob2Hurt = Content.Load<Texture2D>("Golems/Golem_02/PNG Sequences/Hurt/css_sprites(7)");
            Texture2D Mob2Dead = Content.Load<Texture2D>("Golems/Golem_02/PNG Sequences/Dying/css_sprites(6)");

            Texture2D Mob3 = Content.Load<Texture2D>("Golems/Golem_03/PNG Sequences/Idle/css_sprites(13)");
            Texture2D Mob3Walk = Content.Load<Texture2D>("Golems/Golem_03/PNG Sequences/Walking/css_sprites(14)");
            Texture2D Mob3Attack = Content.Load<Texture2D>("Golems/Golem_03/PNG Sequences/Attacking/css_sprites(10)");
            Texture2D Mob3Hurt = Content.Load<Texture2D>("Golems/Golem_03/PNG Sequences/Hurt/css_sprites(12)");
            Texture2D Mob3Dead = Content.Load<Texture2D>("Golems/Golem_03/PNG Sequences/Dying/css_sprites(11)");

            mobs = new Texture2D[]
            {
                Mob1,
                Mob1Walk,
                Mob1Attack,
                Mob1Hurt,
                Mob1Dead,
                Mob2,
                Mob2Walk,
                Mob2Attack,
                Mob2Hurt,
                Mob2Dead,
                Mob3,
                Mob3Walk,
                Mob3Attack,
                Mob3Hurt,
                Mob3Dead
            };

            woodcutters = new Texture2D[]{
                woodcutter,
                woodcutterWalk,
                woodcutterRun,
                woodcutterJump,
                woodcutterDamage,
                woodcutterLight,
                woodcutterHeavy
            };

            backGround = new Texture2D[]
            {
                background1,
                background2,
                background3,
                background4
            };

            tiles = new Texture2D[]
            {
                tile,
                spikes,
                exit
            };



            //var soundEffect = Content.Load<SoundEffect>("burp");
            //soundEffect.Play();

            _screenController = new ScreenController(new IGameObject[]
            {
                new SplashScreen(splashImage),
                new MenuScreen(),
                new LoadScreen(),
                new GameScreen(woodcutters, mobs, tiles, this),
                new GameOverScreen(GameOverImage, this),
                new MainMenuScreen(button, buttonHover, buttonFont,this)
            });

            _screenController.SwitchScreen(ScreenType.Splash);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (_screenController.SwitchToNextScreen() == ScreenType.MainMenu)
                {
                    return;
                }
                Exit();
            };

            var delta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000f);

            timer -= delta;
            if (timer <= 0)
            {
                _screenController.SwitchScreen(ScreenType.MainMenu);
                timer = TIMER;
            }

            if (isSwitch)
            {
                _screenController.SwitchScreen(screenSelection);
            }

            _screenController.Update(delta, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(backGround[3], new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);
            _screenController.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}