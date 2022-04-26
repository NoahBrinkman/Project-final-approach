using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class ForceApplier : Sprite
    {
        public float reachLeft = 0;
        public float reachRight = 0;
        public float reachTop = 0;
        public float reachBottom = 0;
        public Vector2 force;
        public ForceApplier(string fileName, Vector2 power,float reachLeft = 0, float reachRight = 0, float reachTop = 0, float reachBottom = 0) : base(fileName)
        {
            SetOrigin(width/2,height/2);
            this.reachBottom = reachBottom;
            this.reachLeft = reachLeft; 
            this.reachRight = reachRight;   
            this.reachTop = reachTop;
            this.force = power; 
        }
        public bool IsInReach(GameObject other, float otherWidth, float otherHeight)
        {
            return (other.x + otherWidth / 2 > x - width / 2 - reachLeft) && (other.x - otherWidth / 2 < x + width / 2 + reachRight) ||
                (other.y + otherHeight / 2 < y - height / 2 - reachTop && other.y - otherHeight / 2 > y + height / 2 + reachBottom);
           
        }
    }
}
