﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Controllers
{
    class KeyboardController : IInputReader
    {

        public Vector2 ReadInput()
        {
            Vector2 direction = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A))
            {
                direction.X = -1;
            }
            else if (state.IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }
            if (state.IsKeyDown(Keys.LeftShift) && state.IsKeyDown(Keys.A))
            {
                direction.X = -2;
            }
            if (state.IsKeyDown(Keys.LeftShift) && state.IsKeyDown(Keys.D))
            {
                direction.X = 2;
            }
            if (state.IsKeyDown(Keys.W))
            {
                direction.Y = -1;
            }
            return direction;
        }
    }
}
