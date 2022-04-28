using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class ForceApplier : AnimationSprite
    {
        public float reachLeft = 0;
        public float reachRight = 0;
        public float reachTop = 0;
        public float reachBottom = 0;
        public Vector2 force;

        public ForceApplier(string fileName, int rows, int cols, TiledObject obj=null) : base("triangle.png", 1, 1)
		{
            Initialize(obj);
		}

        public ForceApplier(string fileName, Vector2 power,float reachLeft = 0, float reachRight = 0, float reachTop = 0, float reachBottom = 0, TiledObject obj = null) : base(fileName,1,1)
        {
            SetOrigin(width/2,height/2);
            this.reachBottom = reachBottom;
            this.reachLeft = reachLeft; 
            this.reachRight = reachRight;   
            this.reachTop = reachTop;
            this.force = power; 
        }

        public virtual void Initialize(TiledObject obj)
		{
            SetOrigin(width / 2, height / 2);
            this.reachBottom = obj.GetFloatProperty("reachBottom");
            this.reachLeft = obj.GetFloatProperty("reachLeft");
            this.reachRight = obj.GetFloatProperty("reachRight");
            this.reachTop = obj.GetFloatProperty("reachTop");
            this.force = new Vector2(obj.GetFloatProperty("powerX"), obj.GetFloatProperty("powerY"));
        }

        public  virtual bool IsInHorizontalReach(GameObject other, float otherWidth, float otherHeight)
        {
            return  ((other.x < x && other.x > x - width / 2 - reachLeft && other.y > y - height / 2 && other.y < y + height / 2) && reachLeft != 0) ||
                    (other.x > x && other.x < x + width / 2 + reachRight && other.y > y - height / 2 && other.y < y + height / 2 && reachRight != 0);
        }
        public virtual bool IsInVerticalReach(GameObject other, float otherWidth, float otherHeight)
        {
            return ((other.y < y && other.y > y - height / 2 - reachTop && other.x > x - width / 2 && other.x < x + width / 2 && reachTop != 0) ||
                    (other.y > y && other.y < y + height / 2 + reachBottom && other.x > x - width / 2 && other.x < x + width / 2 && reachBottom != 0));
        }
    }
}
