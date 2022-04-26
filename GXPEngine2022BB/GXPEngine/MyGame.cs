using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

    List<ForceApplier> forceAppliers;
    public int GetNumberOfAppliers()
    {
        return forceAppliers.Count;
    }
    public ForceApplier GetForceApplier(int index)
    {
        if (index >= 0 && index < forceAppliers.Count)
        {
            return forceAppliers[index];
        }
        else
        {
            return null;
        }
    }

    public MyGame() : base(800, 600, false, false)
    {
        Square square = new Square(new Vector2(100, 200), new Vector2(1, 1));
        AddChild(square);

        forceAppliers = new List<ForceApplier>();
        ForceApplier applier = new ForceApplier("triangle.png", new Vector2(0, -0.1f), 0, 0, 1000, 0);
        applier.SetXY(width / 2, height - applier.height / 2 - 200);
        AddChild(applier);
        forceAppliers.Add(applier);
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