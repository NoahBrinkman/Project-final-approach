using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Scenes;

public class MyGame : Game
{



    public MyGame() : base(800, 600, false, false)
    {
        targetFps = 60;
        TestingScene scene = new TestingScene();
        SceneManager.instance.AddScene(scene);
        SceneManager.instance.LoadScene(scene);
    }

    public void Update()
    {
        HandleInput();
        Console.WriteLine(currentFps);
    }

    void HandleInput()
    {
        targetFps = Input.GetKey(Key.SPACE) ? 30 : 60;
    }

    static void Main()
    {
        new MyGame().Start();
    }
}