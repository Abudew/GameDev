using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Screens
{
    internal class GameScreen : IScreen
    {
        private Texture2D texture;
        public ScreenType ScreenType => ScreenType.Game;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture,
                new Vector2(0, 0),
                Color.White
            );
            spriteBatch.End();
        }

        public void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
