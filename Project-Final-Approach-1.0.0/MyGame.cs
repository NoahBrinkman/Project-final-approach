using System.Buffers.Text;

namespace GXPEngine;

public class MyGame : Game
{
    public MyGame() : base(800, 600, false)
    {
        
    }
    public static void Main(){
        new MyGame().Start();
    }
}

