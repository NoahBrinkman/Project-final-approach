using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Scenes
{
	public class MainMenuScene : GameObject
	{
		Button startButton;
		public MainMenuScene()
		{
			Sprite logo = new Sprite("logo.png");
			logo.SetOrigin(logo.width/2, logo.height/2);
			logo.SetXY(game.width / 2, game.height / 2 - 200);
			logo.scale = .7f;
			AddChild(logo);
			 startButton = new Button("cursed_StartButton.png",OnButtonClicked);
			startButton.SetOrigin(startButton.width/2, startButton.height/2);
			startButton.SetXY(game.width / 2, game.height / 2 + 200);
			AddChild(startButton);
			Console.WriteLine(GetChildCount());
		}
		private void OnButtonClicked()
		{

			MyGame mgame = (MyGame)game;
			mgame.LoadFirstLevel();
			mgame.DestroyScene(this);
		}
	}
}
