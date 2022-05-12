using System;
using GXPEngine;
using System.Collections.Generic;
using GXPEngine.Scenes;

public class MyGame : Game
{

    private List<GameObject> scenes = new List<GameObject>();
    private Sound music;
    private SoundChannel musicChannel;
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
        SceneManager.instance.AddScene(mainMenu);

		LevelSelectScene levelSelectScene = new LevelSelectScene();
		SceneManager.instance.AddScene(levelSelectScene);
		
        Level levelOne = new Level("Levels/Level1.tmx");
		SceneManager.instance.AddScene(levelOne);
		
		Level levelTwo = new Level("Levels/Level2.tmx");
		SceneManager.instance.AddScene(levelTwo);

		
        Level levelThree = new Level("Levels/Level3.tmx");
        SceneManager.instance.AddScene(levelThree);
        
        CongratulationsScene congratulationsScene = new CongratulationsScene();
        SceneManager.instance.AddScene(congratulationsScene);
        
    }
    public void ReloadGame()
	{
        SceneManager.instance.WipeScenes();
        LoadGame();
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