using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Scenes;

public class MyGame : Game
{



    public MyGame() : base(1344, 768, false)
    {
        /*TestingScene scene = new TestingScene();
        SceneManager.instance.AddScene(scene);
        SeneManager.instance.LoadScene(scene);*/
        Level level = new Level("TestLevel.tmx");
        SceneManager.instance.AddScene(level);
        SceneManager.instance.LoadScene(level);
    }

    public void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
    }

    static void Main()
    {
        new MyGame().Start();
    }
}