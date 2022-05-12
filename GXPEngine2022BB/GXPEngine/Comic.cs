using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Comic : AnimationSprite
    {
        public Comic(string fileName = "Comic.png", int cols = 3, int rows = 2) : base(fileName, cols, rows)
        {
            SetCycle(0, 6);
        }

        void Update()
        {
            Animate(0.0035f);
            if(currentFrame == 5)
                LateDestroy();
        }
    }
}
