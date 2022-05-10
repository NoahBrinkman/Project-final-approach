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

        private Sprite collected1;
        private Sprite collected2;
        private Sprite collected3;

        List<Sprite> collected;

        private Sprite notCollected1;
        private Sprite notCollected2;
        private Sprite notCollected3;

        List<Sprite> notCollected;

        public LevelOverlay(LevelCamera camera)
        {
            collected = new List<Sprite>();
            notCollected = new List<Sprite>();

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

            collected1 = new Sprite("Star.png", false, false);
            collected1.SetOrigin(collected1.width / 2, collected1.height / 2);
            collected1.SetXY(game.width / 2 - collected1.width, game.height / 2);
            AddChild(collected1);

            collected2 = new Sprite("Star.png", false, false);
            collected2.SetOrigin(collected2.width / 2, collected2.height / 2);
            collected2.SetXY(game.width / 2, game.height / 2);
            AddChild(collected2);

            collected3 = new Sprite("Star.png", false, false);
            collected3.SetOrigin(collected3.width / 2, collected3.height / 2);
            collected3.SetXY(game.width / 2 + collected3.width, game.height / 2);
            AddChild(collected3);

            collected.Add(collected1);
            collected.Add(collected2);
            collected.Add(collected3);

            notCollected1 = new Sprite("NoStar.png", false, false);
            notCollected1.SetOrigin(notCollected1.width / 2, notCollected1.height / 2);
            notCollected1.SetXY(game.width / 2 - notCollected1.width, game.height / 2);
            AddChild(notCollected1);

            notCollected2 = new Sprite("NoStar.png", false, false);
            notCollected2.SetOrigin(notCollected2.width / 2, notCollected2.height / 2);
            notCollected2.SetXY(game.width / 2, game.height / 2);
            AddChild(notCollected2);

            notCollected3 = new Sprite("NoStar.png", false, false);
            notCollected3.SetOrigin(notCollected3.width / 2, notCollected3.height / 2);
            notCollected3.SetXY(game.width / 2 + notCollected3.width, game.height / 2);
            AddChild(notCollected3);

            notCollected.Add(notCollected1);
            notCollected.Add(notCollected2);
            notCollected.Add(notCollected3);
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
