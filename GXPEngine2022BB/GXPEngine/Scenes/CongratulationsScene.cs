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
			MyGame mg = (MyGame)game;
			
			Sprite bg = new Sprite("Backgrounds/ThankYou_Background.png");
			AddChild(bg);
			
			Button backButton = new Button("UI/next_back_spritesheet.png", mg.ReloadGame);
			backButton.SetOrigin(backButton.width/2,backButton.height/2);
			backButton.SetXY(100,100);
			backButton.Mirror(true,false);
			AddChild(backButton);
		}
	}
}
