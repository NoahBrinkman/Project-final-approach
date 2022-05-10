namespace GXPEngine.Scenes
{
    public class LevelSelectScene : Scene
    {

        public LevelSelectScene() : base()
        {
            Sprite bg = new Sprite("Backgrounds/Menu_Background.png");
            AddChild(bg);
            Sprite selectLevelText = new Sprite("UI/SelectLevelText.png");
            selectLevelText.SetOrigin(selectLevelText.width/2,selectLevelText.height/2);
            selectLevelText.SetXY(game.width/2, 100);
            selectLevelText.scale = .7f;
            AddChild(selectLevelText);
            MyGame mg = (MyGame)game;
            Button backButton = new Button("UI/next_back_spritesheet.png", mg.ReloadGame);
            backButton.SetOrigin(backButton.width/2,backButton.height/2);
            backButton.SetXY(100,100);
            AddChild(backButton);
            backButton.Mirror(true,false);
            Button levelOne = new Button("UI/Level_One.png", LoadLevelOne,1,1,1);
            levelOne.SetXY(300,game.height/2);
            levelOne.SetOrigin(levelOne.width/2,levelOne.height/2);
            AddChild(levelOne);
            Button levelTwo = new Button("UI/Level_Two.png", LoadLevelTwo,1,1,1);
            levelTwo.SetOrigin(levelTwo.width/2,levelTwo.height/2);
            levelTwo.SetXY(500,game.height/2);
            AddChild(levelTwo);
            Button levelThree = new Button("UI/Level_Three.png", LoadLevelThree,1,1,1);
            levelThree.SetOrigin(levelThree.width/2,levelThree.height/2);
            levelThree.SetXY(700,game.height/2);
            AddChild(levelThree);
            Sprite lockedLevelOne = new Sprite("UI/Locked_Level.png");
            lockedLevelOne.SetXY(900,game.height/2);
            lockedLevelOne.SetOrigin(lockedLevelOne.width/2,lockedLevelOne.height/2);
            AddChild(lockedLevelOne);
            Sprite lockedLevelTwo = new Sprite("UI/Locked_Level.png");
            lockedLevelTwo.SetXY(1100,game.height/2);
            lockedLevelTwo.SetOrigin(lockedLevelTwo.width/2,lockedLevelTwo.height/2);
            AddChild(lockedLevelTwo);
        }
        
        private void LoadLevelOne()
        {
            SceneManager.instance.LoadScene(2);
        }

        private void LoadLevelTwo()
        {
            SceneManager.instance.LoadScene(3);
        }

        private void LoadLevelThree()
        {
            SceneManager.instance.LoadScene(4);
        }
        
    }
}