using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
	public class TogglableForceApplier : ForceApplier
	{
		public bool shouldBeActivatable = true;
		public bool activatable = true;
		public bool activated = false;
		private LevelCamera camera;
		public TogglableForceApplier(string fileName, int rows, int cols, TiledObject obj = null) : base( obj)
		{
			Initialize(obj);
			Console.WriteLine(this._frames);
			activatable = obj.GetBoolProperty("activatable");
			shouldBeActivatable = obj.GetBoolProperty("activatable"); 
			activated = obj.GetBoolProperty("activated");
		}


		public void SetLevelCamera(LevelCamera cam)
		{
			camera = cam;
		}
		
		void Update()
		{
			if (activatable)
			{
				float mouseX = camera.ScreenPointToGlobal(Input.mouseX, Input.mouseY).x;
				if(Input.GetMouseButtonDown(0) && (mouseX  > (x ) - width/2 && mouseX < (x) + width/2) && (Input.mouseY > y - height/2 && Input.mouseY < y + height/2))
				{
					OnClicked();
				}
			}
			if(!shouldBeActivatable)
			{
				SetColor(.8f,.8f,.8f);
			}

			//TEMP REMOVE LATER FOR SPRITE SWITCH
			if (activated)
			{
				SetCycle(1, base.frameTotal -1);
				Animate(.1f);
			}
			else
			{
				SetCycle(0,1);
			}

		}
		void OnClicked()
		{
			Console.WriteLine(_frames);
			activated = !activated;
		}
		public override bool IsInHorizontalReach(GameObject other, float width, float height)
		{
			if (activated)
            {
				return base.IsInHorizontalReach(other, width, height);
			}	
			else
			{
				return false;
			}
		}
		public override bool IsInVerticalReach(GameObject other, float width, float height)
		{
			if (activated)
				return base.IsInVerticalReach(other, width, height);
			else
			{
				return false;
			}
		}
	}
}
