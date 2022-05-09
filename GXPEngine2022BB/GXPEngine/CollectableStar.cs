using TiledMapParser;

namespace GXPEngine
{
    public class Collectable : AnimationSprite
    {
        public Collectable(TiledObject obj = null) 
            : base(obj.GetStringProperty("fileName"), obj.GetIntProperty("cols"), obj.GetIntProperty("rows"))
        {
            Initialize(obj);
            
        }
        public Collectable() 
            : base("",1,1)
        {

        }
        private void Initialize(TiledObject obj)
        {
            SetCycle(0,8);
        }

        private void Update()
        {
            Animate(.6f);
        }


        public void Collect()
        {
            if (visible)
            {
                Level level = (Level)SceneManager.instance.activeScene;
                level.CollectableCollected();
                visible = false; 
            }

        }
    }
}