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

        List<Sprite> collected;
        List<Sprite> notCollected;

        public LevelOverlay(LevelCamera camera)
        {
            collected = new List<Sprite>();
            notCollected = new List<Sprite>();

            hasWon = false;
            cam = camera;
            overlay = new EasyDraw(game.width / 2, game.height, false);
            AddChild(overlay);
            retryButton = new LevelButton("UI/restart_spritesheet.png", OnRetryButton,cam);
            retryButton.SetOrigin(retryButton.width/2,retryButton.height/2);
            retryButton.SetXY(675,700);
            AddChild(retryButton);
            nextButton = new LevelButton("UI/next_back_spritesheet.png", onNextButton,cam);
            nextButton.SetOrigin(retryButton.width/2,retryButton.height/2);
            nextButton.SetXY(875,700);
            AddChild(nextButton);
            backButton = new LevelButton("UI/next_back_spritesheet.png", OnBackButton,cam); 
            backButton.SetOrigin(retryButton.width/2,retryButton.height/2);
            backButton.SetXY(475,700);
            AddChild(backButton);
            backButton.Mirror(true,false);
            overlay.SetXY(game.width / 2- overlay.width/2, 0);
            wonMessage = new Sprite("UI/WonMessage.png", false, false);
            wonMessage.SetOrigin(wonMessage.width / 2, wonMessage.height / 2);
            wonMessage.SetXY(game.width / 2, wonMessage.height / 2);
            wonMessage.scale = 0.7f;
            AddChild(wonMessage);
            lossMessage = new Sprite("UI/LostMessage.png", false, false);
            lossMessage.SetOrigin(lossMessage.width / 2, lossMessage.height / 2);
            lossMessage.SetXY(game.width / 2, lossMessage.height / 2);
            lossMessage.scale = 0.7f;
            AddChild(lossMessage);

            for (int i = 0; i < 3; i++)
            {
                
                Sprite star = new Sprite("UI/Star.png", false, false);
                float offset = star.width + 10;
                star.SetOrigin(star.width / 2, star.height / 2);
                star.SetXY(game.width / 2 + (offset * i) - offset, game.height / 2);
                AddChild(star);
                collected.Add(star);

                Sprite noStar = new Sprite("UI/NoStar.png", false, false);
                noStar.SetOrigin(noStar.width / 2, noStar.height / 2);
                noStar.SetXY(game.width / 2 + (offset * i) - offset, game.height / 2);
                AddChild(noStar);
                notCollected.Add(noStar);
            }
        }

        public void TurnVisibility(bool isVisible, int collectableAmount)
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
            collected.ForEach(x => x.visible = false);
            for (int i = 0; i < collectableAmount; i++)
            {
                Console.WriteLine("hi");
                collected[i].visible = true;
            }
        }

        void Update()
        {
            if(!visible) return;
            overlay.Clear(0,0,0,200);
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
