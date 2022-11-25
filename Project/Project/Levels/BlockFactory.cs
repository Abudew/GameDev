using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Levels
{
    class BlockFactory
    {
        public Block CreateBlock(string type, int x, int y, GraphicsDevice graphics)
        {
            Block newBlock = null;
            type = type.ToUpper();
            if (type == "NORMAL")
            {
                newBlock = new Block(x, y, graphics);
            }
            if (type == "EXIT")
            {
                newBlock = new Exit(x, y, graphics);
            }
            if (type == "TRAP")
            {
                newBlock = new Trap(x, y, graphics);
            }
            if (type == "SLOW")
            {
                newBlock = new Slow(x, y, graphics);
            }
            return newBlock;
        }

    }
}
