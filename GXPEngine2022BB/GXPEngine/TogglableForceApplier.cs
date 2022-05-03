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

		public TogglableForceApplier(string fileName, int rows, int cols, TiledObject obj = null) : base(fileName,new Vector2(), 0, 0, 0, 0)
		{
			Initialize(obj);
			activatable = obj.GetBoolProperty("activatable");
			activated = obj.GetBoolProperty("activated");
		}
		public TogglableForceApplier(string fileName, Vector2 power, bool activatable, bool activated, float reachLeft = 0, float reachRight = 0, float reachTop = 0, float reachBottom = 0) : base(fileName, power, reachLeft,reachRight,reachTop,reachBottom)
		{
			this.activatable = activatable;
			shouldBeActivatable = activatable;
			this.activated = activated;
		}
		void Update()
		{
			if (activatable)
			{
				if(Input.GetMouseButtonDown(0) && (Input.mouseX > x - width/2 && Input.mouseX < x + width/2) && (Input.mouseY > y - height/2 && Input.mouseY < y + height/2))
				{
					OnClicked();
				}
			}

			//TEMP REMOVE LATER FOR SPRITE SWITCH
			if (activated)
				SetColor(0, 255, 0);
			else
				SetColor(255, 255, 255);
		}
		void OnClicked()
		{
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
