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

        private LevelButton retryButton;
        private LevelButton nextButton;
        private LevelButton backButton;
        
        public LevelOverlay(LevelCamera camera)
        {
            hasWon = false;
            cam = camera;
            overlay = new EasyDraw(game.width / 2, game.height, false);
            AddChild(overlay);
            retryButton = new LevelButton("restart_spritesheet.png", OnRetryButton,cam);
            retryButton.SetOrigin(retryButton.width/2,retryButton.height/2);
            retryButton.SetXY(675,700);
            AddChild(retryButton);
            nextButton = new LevelButton("next_back_spritesheet.png", onNextButton,cam);
            nextButton.SetOrigin(retryButton.width/2,retryButton.height/2);
            nextButton.SetXY(875,700);
            AddChild(nextButton);
            backButton = new LevelButton("next_back_spritesheet.png", OnBackButton,cam); 
            backButton.SetOrigin(retryButton.width/2,retryButton.height/2);
            backButton.SetXY(475,700);
            AddChild(backButton);
            backButton.Mirror(true,false);
            overlay.SetXY(game.width / 2- overlay.width/2, 0);
        }

        public void TurnVisibility(bool isVisible)
        {
            for (int i = 0; i < GetChildCount(); i++)
            {
                GetChildren()[i].visible = isVisible;
                Vector2 position = cam.ScreenPointToGlobal((int)GetChildren()[i].x, (int)GetChildren()[i].y);
                GetChildren()[i].SetXY(position.x,position.y);
            }

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

        private void OnRetryButton()
        {
            SceneManager.instance.ReloadActiveScene();
        }

        private void onNextButton()
        {
            SceneManager.instance.TryLoadNextScene();
        }

        private void OnBackButton()
        {
            MyGame g = (MyGame)game;
            g.ReloadGame();
        }
    }
}
