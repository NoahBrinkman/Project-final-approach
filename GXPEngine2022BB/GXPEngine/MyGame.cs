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

	public MyGame() : base(800, 600, false)
	{
		forceAppliers = new List<ForceApplier>();

		Square square = new Square(new Vector2(100, 200), new Vector2(1, 1));
		AddChild(square);
		ForceApplier applier = new ForceApplier("triangle.png", new Vector2(0, -0.1f), 0, 0, 1000, 0);
		applier.SetXY(width / 2, height - applier.height / 2 - 200);
		AddChild(applier);
		forceAppliers.Add(applier);
	}

	public void Update()
    {
		 if(Input.GetKey(Key.SPACE)){
			Console.WriteLine("hi");
			targetFps = 5;
        }else
        {
			targetFps = 60;
        }
    }

	static void Main()
	{
		new MyGame().Start();
	}
}