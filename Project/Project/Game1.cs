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

        private ScreenController _screenController;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var splashImage = Content.Load<Texture2D>("Splash");
            //var soundEffect = Content.Load<SoundEffect>("");

            //soundEffect.Play();

            _screenController = new ScreenController(new IScreen[]
            {
                new SplashScreen(splashImage)
            });

            _screenController.SetScreen(ScreenType.Splash);
            _screenController.SwitchToNextScreen();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var delta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000f);
            _screenController.Update(delta);

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