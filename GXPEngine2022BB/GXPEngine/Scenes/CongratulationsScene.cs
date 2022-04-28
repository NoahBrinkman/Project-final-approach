using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Scenes
{
	public class CongratulationsScene : Scene
	{
		public CongratulationsScene()
		{
			Sprite logo = new Sprite("cursed_GoodJob.png");
			logo.SetOrigin(logo.width / 2, logo.height / 2);
			logo.SetXY(game.width / 2, game.height / 2 - 200);
			logo.scale = .7f;
			AddChild(logo);
			Button restartButton = new Button("cursed_Next.png", OnButtonClicked);
			restartButton.SetOrigin(restartButton.width / 2, restartButton.height / 2);
			restartButton.SetXY(game.width / 2, game.height / 2 + 200);
			AddChild(restartButton);
		}

		private void OnButtonClicked()
		{
			MyGame mg = (MyGame)game;
			mg.ReloadGame();
		}
	}
}
