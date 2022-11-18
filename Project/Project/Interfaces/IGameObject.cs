using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IGameObject
    {
        ScreenType ScreenType { get; }
        void Update(float delta, GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }

}
