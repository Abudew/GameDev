using Microsoft.Xna.Framework.Graphics;
using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interfaces
{
    public interface IScreen
    {
        ScreenType ScreenType { get; }
        void Update(float delta);

        void Draw(SpriteBatch spriteBatch);
    }

}
