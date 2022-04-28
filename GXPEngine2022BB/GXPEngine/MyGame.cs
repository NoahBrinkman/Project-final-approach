using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Scenes;

public class MyGame : Game
{

    private List<GameObject> scenes = new List<GameObject>();

    public MyGame() : base(1344, 768, false)
    {
        /*TestingScene scene = new TestingScene();
        SceneManager.instance.AddScene(scene);
        SeneManager.instance.LoadScene(scene);*/

        LoadGame();
    }
    void LoadGame()
	{
        MainMenuScene mainMenu = new MainMenuScene();
        scenes.Add(mainMenu);

        Level level = new Level("TestLevel.tmx");
        scenes.Add(level);

        CongratulationsScene congratulationsScene = new CongratulationsScene();
        scenes.Add(congratulationsScene);

        GameOverScene gameOverScene = new GameOverScene();
        scenes.Add(gameOverScene);

        MainMenuScene loadedMainMenu = (MainMenuScene)scenes[0];
        AddChild(loadedMainMenu);
    }
    public void LoadFirstLevel()
	{
        for (int i = 0; i < scenes.Count; i++)
		{
            if(scenes[i] is Level)
			{
                AddChild(scenes[i]);
                Level level = (Level)scenes[i];
                level.Start();
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
        scenes.Clear();
        GetChildren().ForEach(x => x.LateDestroy());
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
            Console.WriteLine(GetChildCount());
		}
    }

    static void Main()
    {
        new MyGame().Start();

    }
}