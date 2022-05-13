using System;

namespace GXPEngine
{
    public class LevelSelectButton : Button
    {
        public Action<int> onButtonClicked;
        private int buildIndex;
        public LevelSelectButton(string fileName, Action<int> intAction, int buildIndex , int rows, int cols, int frames) : base(fileName,null, cols,rows,frames)
        {
            onButtonClicked = intAction;
            this.buildIndex = buildIndex;
            SetCycle(0,3);
        }

        protected override void Update()
        {
            if(!visible) return;
            Animate(0.015f);
            float mouseX = Input.mouseX;
            if (Input.GetMouseButtonDown(0) && (mouseX > (x) - width / 2 && mouseX < (x) + width / 2) &&
                (Input.mouseY > y - height / 2 && Input.mouseY < y + height / 2))
            {
                Console.WriteLine("clicked");
                if(onButtonClicked != null)
                {
                    Console.WriteLine("invoking");
                    onButtonClicked.Invoke(buildIndex);
                }
            }
        }
        
        
    }
}