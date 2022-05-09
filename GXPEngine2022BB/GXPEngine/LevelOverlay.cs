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

        private Button retryButton;
        private Button nextButton;
        private Button backButton;
        
        public LevelOverlay(LevelCamera camera)
        {
            hasWon = false;
            cam = camera;
            overlay = new EasyDraw(game.width / 2, game.height, false);
            retryButton = new Button("restart_spritesheet.png", OnRetryButton); 
            overlay.AddChild(retryButton);
            nextButton = new Button("next_back_spritesheet.png", onNextButton);  
            overlay.AddChild(nextButton);
            backButton = new Button("next_back_spritesheet.png", OnBackButton); 
            overlay.AddChild(backButton);
            backButton.Mirror(true,false);
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
