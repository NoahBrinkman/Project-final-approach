using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
	public MyGame() : base(800, 600, false)
	{
		Square square = new Square(new Vector2(100,400), new Vector2(1, 1));
		AddChild(square);
	}

	public void Update()
    {

    }

	static void Main()
	{
		new MyGame().Start();
	}
}