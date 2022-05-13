using System;
using TiledMapParser;

namespace GXPEngine
{
    public class Goal : AnimationSprite
    {
        public Action goalHit;

        public Goal(string fileName, int rows, int cols, TiledObject obj = null) : base(obj.GetStringProperty("fileName"), obj.GetIntProperty("cols"), obj.GetIntProperty("rows"))
        {
            SetOrigin(width / 2, height / 2);
            SetCycle(0,9);
        }

        public Goal() : base("colors.png", 1, 1)
        {

        }

        void OnCollision(GameObject other)
        {
            if(other is Player)
            {
                goalHit += GoalHit;
                goalHit.Invoke();
            }
        }

        private void Update()
        {
            Animate(.06f);
        }
        static void GoalHit()
        {
            Console.WriteLine("win");
        } 
    }
}