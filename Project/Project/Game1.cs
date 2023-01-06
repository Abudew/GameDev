 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Controllers;
using Project.Interfaces;
using Project.Screens;

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
        public bool isSwitch { get; set; } = false;
        public ScreenType screenSelection { get; set; }
        private float timer = 3;
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
            var button = Content.Load<Texture2D>("Button");
            var buttonHover = Content.Load<Texture2D>("ButtonHover");
            var buttonFont = Content.Load<SpriteFont>("Font");

            Texture2D woodcutter = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_idle");
            Texture2D woodcutterWalk = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_walk");
            Texture2D woodcutterRun = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_run");
            Texture2D woodcutterJump = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_jump");
            Texture2D woodcutterDamage = Content.Load<Texture2D>("Characters/1 Woodcutter/Woodcutter_hurt");
            Texture2D background1 = Content.Load<Texture2D>("game_background_1");
            Texture2D background2 = Content.Load<Texture2D>("game_background_2");
            Texture2D background3 = Content.Load<Texture2D>("game_background_3. 2");
            Texture2D background4 = Content.Load<Texture2D>("game_background_4");
            

            woodcutters = new Texture2D[]{
                woodcutter,
                woodcutterWalk,
                woodcutterRun,
                woodcutterJump,
                woodcutterDamage
            };

            backGround = new Texture2D[]
            {
                background1,
                background2,
                background3,
                background4
            };

            //var soundEffect = Content.Load<SoundEffect>("burp");
            //soundEffect.Play();

            _screenController = new ScreenController(new IGameObject[]
            {
                new SplashScreen(splashImage),
                new MenuScreen(),
                new LoadScreen(),
                new GameScreen(woodcutters, this),
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