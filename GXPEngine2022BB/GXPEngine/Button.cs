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
		public Button(string fileName, Action action, int cols = 3, int rows = 1, int frames = 3) : base(fileName,cols,rows, frames, false,false)
		{
			OnClicked = action;
			SetCycle(0,frameCount);
		}

		protected virtual void Update()
		{
			if(frameCount != 1)
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
