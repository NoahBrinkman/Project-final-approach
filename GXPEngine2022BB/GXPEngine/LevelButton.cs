using System;

namespace GXPEngine
{
    public class LevelButton : Button
    {
        private LevelCamera cam;
        public LevelButton(string fileName, Action action, LevelCamera cam) : base(fileName,action)
        {
            this.cam = cam;
            SetCycle(0,3);
        }

        protected override void Update()
        {
            if(!visible) return;
            Animate(0.015f);
            float mouseX = cam.ScreenPointToGlobal(Input.mouseX, Input.mouseY).x;
            if (Input.GetMouseButtonDown(0) && (mouseX > (x) - width / 2 && mouseX < (x) + width / 2) &&
                (Input.mouseY > y - height / 2 && Input.mouseY < y + height / 2))
            {
                if(OnClicked != null)
                {
                    OnClicked.Invoke();
                }
            }
        }
        
        
    }
}