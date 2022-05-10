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
    // Show amount of stars
        EasyDraw overlay;
        public bool hasWon;
        private LevelCamera cam;

        private LevelButton retryButton;
        private LevelButton nextButton;
        private LevelButton backButton;
        private Sprite wonMessage;
        private Sprite lossMessage;


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

            wonMessage = new Sprite("WonMessage.png", false, false);
            wonMessage.SetOrigin(wonMessage.width / 2, wonMessage.height / 2);
            wonMessage.SetXY(game.width / 2, wonMessage.height / 2);
            wonMessage.scale = 0.7f;
            AddChild(wonMessage);

            lossMessage = new Sprite("LostMessage.png", false, false);
            lossMessage.SetOrigin(lossMessage.width / 2, lossMessage.height / 2);
            lossMessage.SetXY(game.width / 2, lossMessage.height / 2);
            lossMessage.scale = 0.7f;
            AddChild(lossMessage);
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

            if (hasWon)
            {
                lossMessage.visible = false;
            }   
            else
            {
                wonMessage.visible = false;
                nextButton.visible = false;
            }
                
            
        }
        void Update()
        {
            if(!visible) return;
            
            if(!hasWon)
                overlay.Clear(0,0,0,200);
            else
                overlay.Clear(100,100,100,200);
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
