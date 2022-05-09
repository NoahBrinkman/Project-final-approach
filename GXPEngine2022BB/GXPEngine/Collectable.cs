using TiledMapParser;

namespace GXPEngine
{
    public class Collectable : AnimationSprite
    {
        public Collectable(string fileName, int cols,int rows) : base("", 1, 1)
        {
            
        }
        public Collectable(TiledObject obj = null) : base("", 1, 1)
        {
            
        }

        private void Initialize(TiledObject obj)
        {
            
        }



        public void Collect()
        {
            Level level = (Level)SceneManager.instance.activeScene;
            level.CollectableCollected();
        }
    }
}