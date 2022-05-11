using System;
using GXPEngine;
using System.Collections.Generic;
using GXPEngine.Scenes;

public class MyGame : Game
{

    private List<GameObject> scenes = new List<GameObject>();
    Sound music;
    SoundChannel musicChannel;
    public MyGame() : base(1366, 768, false)
    {
        /*TestingScene scene = new TestingScene();
        SceneManager.instance.AddScene(scene);
        SeneManager.instance.LoadScene(scene);*/
        music = new Sound("Sound/BackgroundMusic.mp3", true, true);
        musicChannel = music.Play();
        musicChannel.Volume = 0.3f;

        LoadGame();
    }
    void LoadGame()
    {
        MainMenuScene mainMenu = new MainMenuScene();
       // scenes.Add(mainMenu);
		SceneManager.instance.AddScene(mainMenu);

		LevelSelectScene levelSelectScene = new LevelSelectScene();
		SceneManager.instance.AddScene(levelSelectScene);
		
        Level levelOne = new Level("Levels/Level1.tmx");
		SceneManager.instance.AddScene(levelOne);
		
		Level levelTwo = new Level("Levels/Level2.tmx");
		SceneManager.instance.AddScene(levelTwo);
        //scenes.Add(level);
        Level levelThree = new Level("Levels/Level3.tmx");
        SceneManager.instance.AddScene(levelThree);
        CongratulationsScene congratulationsScene = new CongratulationsScene();
        SceneManager.instance.AddScene(congratulationsScene);
       // scenes.Add(congratulationsScene);

        GameOverScene gameOverScene = new GameOverScene();
        SceneManager.instance.AddScene(gameOverScene);
       // SceneManager.instance.LoadScene(0);
       // scenes.Add(gameOverScene);

        
        //AddChild(loadedMainMenu);
    }
    public void LoadFirstLevel()
	{
        for (int i = 0; i < scenes.Count; i++)
		{
            if(scenes[i] is Level)
			{
                AddChild(scenes[i]);
                Level level = (Level)scenes[i];
	                //level.Start();
                return;
			}
		}
	}
    public Level GetCurrentLevel()
	{
        List<GameObject> gobjs = GetChildren();
		for (int i = 0; i < gobjs.Count; i++)
		{
            if(gobjs[i] is Level)
			{
                return (Level)gobjs[i];
			}
		}
        return null;
	}
    public void DestroyScene(GameObject scene)
	{
		if (scenes.Contains(scene))
		{
            scene.LateDestroy();

		}
	}

    public void LoadGameOverScene()
	{
		for (int i = 0; i < scenes.Count; i++)
		{
            if(scenes[i] is GameOverScene)
			{
                LateAddChild(scenes[i]);
			}
		}
	}
    public void LoadCongratulationsScene()
	{
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i] is CongratulationsScene)
            {
                LateAddChild(scenes[i]);
            }
        }
    }
    public void ReloadGame()
	{
        SceneManager.instance.WipeScenes();
        LoadGame();
        List<GameObject> Children = new List<GameObject>();
	}

    public void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
		if (Input.GetKeyDown(Key.D))
		{
            Console.WriteLine(GetChildren().Count);
		}
    }

    static void Main()
    {
        new MyGame().Start();

    }
}