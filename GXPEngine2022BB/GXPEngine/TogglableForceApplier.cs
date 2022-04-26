﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	public class TogglableForceApplier : ForceApplier
	{
		public bool activatable = true;
		public bool activated = false;


		public TogglableForceApplier(string fileName, Vector2 power, bool activatable, bool activated, float reachLeft = 0, float reachRight = 0, float reachTop = 0, float reachBottom = 0) : base(fileName, power, reachLeft,reachRight,reachTop,reachBottom)
		{
			this.activatable = activatable;
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
		}
		void OnClicked()
		{
			activated = !activated;
		}
		public override bool IsInHorizontalReach(GameObject other, float width, float height)
		{
			if (activated)
				return base.IsInHorizontalReach(other,width,height);
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
