using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
	public MyGame() : base(800, 600, false)
	{

	}

	public void Update()
    {

    }

	static void Main()
	{
		new MyGame().Start();
	}
}