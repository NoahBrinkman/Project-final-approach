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
        public bool hasWon;
        private LevelCamera cam;
        public LevelOverlay(LevelCamera camera)
        {
            hasWon = false;
            cam = camera;
            overlay = new EasyDraw(game.width / 2, game.height, false);
           
            AddChild(overlay);
        }

        public void TurnVisibility(bool isVisible)
        {
            for (int i = 0; i < GetChildCount(); i++)
            {
                GetChildren()[i].visible = isVisible;
            }
            Vector2 center = cam.ScreenPointToGlobal(game.width / 2, game.height / 2);
            overlay.x = center.x - overlay.width/2;
            visible = isVisible;

        }
        void Update()
        {

            if(!visible) return;
            
            if(!hasWon)
                overlay.Clear(0,0,0,200);
            else
                overlay.Clear(255,255,255,200);
        }
    }
}
