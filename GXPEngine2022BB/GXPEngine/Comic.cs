using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Comic : AnimationSprite
    {
        private int lastFrame;
        public Comic(string fileName = "Comic.png", int cols = 3, int rows = 2) : base(fileName, cols, rows)
        {
            SetCycle(0, cols * rows);
            lastFrame = cols*rows - 1;
        }

        void Update()
        {
            Animate(0.0035f);
            if(currentFrame == lastFrame)
                LateDestroy();
        }
    }
}
