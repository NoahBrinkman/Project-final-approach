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
        public int frameTotal;
        public string threat;

        public ForceApplier(TiledObject obj=null) : 
            base(obj.GetStringProperty("fileName"), obj.GetIntProperty("cols"), obj.GetIntProperty("rows"))
		{

            Initialize(obj);
        }
        

        public virtual void Initialize(TiledObject obj)
		{
            SetOrigin(width / 2, height / 2);
            this.reachBottom = obj.GetFloatProperty("reachBottom");
            this.reachLeft = obj.GetFloatProperty("reachLeft");
            this.reachRight = obj.GetFloatProperty("reachRight");
            this.reachTop = obj.GetFloatProperty("reachTop");
            this.force = new Vector2(obj.GetFloatProperty("powerX"), obj.GetFloatProperty("powerY"));
            this.frameTotal = obj.GetIntProperty("frames");
            this.threat = obj.GetStringProperty("threat");
            SetCycle(0,frameCount);
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
