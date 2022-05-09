using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
	public class Button : AnimationSprite
	{
		public Action OnClicked;
		public Button(string fileName, Action action) : base(fileName,3,1)
		{
			OnClicked = action;
			SetCycle(0,3);
		}

		void Update()
		{
			Animate(0.01f);
			if(Input.GetMouseButtonDown(0) && (Input.mouseX > x - width / 2 && Input.mouseX < x + width / 2) && (Input.mouseY > y - height / 2 && Input.mouseY < y + height / 2))
			{
				if(OnClicked != null)
				{
					OnClicked.Invoke();
				}
			}
		}

	}
}
