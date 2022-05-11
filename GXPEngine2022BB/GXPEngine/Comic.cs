using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Comic : AnimationSprite
    {
        public Comic() : base("Comic.png", 3, 2)
        {
            SetCycle(0, 6);
        }

        void Update()
        {
            Animate(0.0025f);
            if(currentFrame == 5)
                LateDestroy();
        }
    }
}
