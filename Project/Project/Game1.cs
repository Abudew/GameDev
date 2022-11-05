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

        public static int ScreenWidth;
        public static int ScreenHeight;

        public bool isSwitch { get; set; } = false;
        public ScreenType screenFromMain { get; set; }

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
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var splashImage = Content.Load<Texture2D>("Splash");
            var exitButton = Content.Load<Texture2D>("Exit");
            var exitButtonHover = Content.Load<Texture2D>("ExitHover");
            var newButton = Content.Load<Texture2D>("LoadGame");
            var newButtonHover = Content.Load<Texture2D>("LoadGameHover");
            var loadButton = Content.Load<Texture2D>("NewGame");
            var loadButtonHover = Content.Load<Texture2D>("NewGameHover");
            //var soundEffect = Content.Load<SoundEffect>("burp");

            //soundEffect.Play();

            _screenController = new ScreenController(new IScreen[]
            {
                new SplashScreen(splashImage),
                new MenuScreen(),
                new NewScreen(),
                new LoadScreen(),
                new GameScreen(),
                new MainMenuScreen(exitButton, exitButtonHover, newButton, newButtonHover, loadButton, loadButtonHover, this)
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
            _screenController.Update(delta);

            timer -= delta;
            if (timer <= 0)
            {
                _screenController.SwitchScreen(ScreenType.MainMenu);
            }

            if (isSwitch)
            {
                _screenController.SwitchScreen(screenFromMain);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _screenController.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}