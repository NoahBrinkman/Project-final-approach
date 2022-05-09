using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class LevelOverlay : GameObject
    {
        EasyDraw overlay;
        private bool hasWon;

        public LevelOverlay(bool hasWon)
        {
            this.hasWon = hasWon;
            overlay = new EasyDraw(game.width, game.height, false);
            AddChild(overlay);
        }

        void Update()
        {
            if(!hasWon)
                overlay.Clear(Color.Black);
            else
                overlay.Clear(Color.White);
        }
    }
}
