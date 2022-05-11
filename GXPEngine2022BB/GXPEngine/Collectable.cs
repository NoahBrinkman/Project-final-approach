using TiledMapParser;

namespace GXPEngine
{
    public class Collectable : AnimationSprite
    {
        Sound collectedSound;

        public Collectable(string fileName, int rows, int cols, TiledObject obj = null) 
            : base(obj.GetStringProperty("fileName"), obj.GetIntProperty("cols"), obj.GetIntProperty("rows"))
        {
            collectedSound = new Sound("Sound/ping.wav",false,false);
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
            Animate(.06f);
        }


        public void Collect()
        {
            if (visible)
            {
                collectedSound.Play();
                Level level = (Level)SceneManager.instance.activeScene;
                level.CollectableCollected();
                visible = false; 
            }

        }
    }
}