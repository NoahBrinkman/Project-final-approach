using System;
using TiledMapParser;

namespace GXPEngine
{
    public class Goal : AnimationSprite
    {
        public Action goalHit;

        public Goal(string fileName, int rows, int cols, TiledObject obj = null) : base(obj.GetStringProperty("fileName"), 1, 1)
        {
            SetOrigin(width / 2, height / 2);
        }

        public Goal() : base("colors.png", 1, 1)
        {

        }

        void OnCollision(GameObject other)
        {
            if(other is Square)
            {
                goalHit += GoalHit;
                goalHit.Invoke();
            }
        }

        static void GoalHit() => Console.WriteLine("win");
    }
}