using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Scenes
{
	public class MainMenuScene : Scene
	{
		Button startButton;
		public MainMenuScene()
		{
			Sprite bg = new Sprite("Menu_Background.png");
			AddChild(bg);
			Sprite logo = new Sprite("logo.png");
			logo.SetOrigin(logo.width/2, logo.height/2);
			logo.SetXY(game.width / 2, game.height / 2 - 200);
			logo.scale = .5f;
			AddChild(logo);
			 startButton = new Button("start_button_spritesheet.png",OnButtonClicked);
			startButton.SetOrigin(startButton.width/2, startButton.height/2);
			startButton.SetXY(game.width / 2, game.height / 2 + 200);

			AddChild(startButton);
		}
		private void OnButtonClicked()
		{

			/*MyGame mgame = (MyGame)game;
			mgame.LoadFirstLevel();
			mgame.DestroyScene(this);*/
			softUnload = false;
			SceneManager.instance.TryLoadNextScene();
		}
	}
}
