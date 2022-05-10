using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Scenes
{
	internal class TestingScene : Scene
	{
        List<ForceApplier> forceAppliers;
        public int GetNumberOfAppliers()
        {
            return forceAppliers.Count;
        }
        public ForceApplier GetForceApplier(int index)
        {
            if (index >= 0 && index < forceAppliers.Count)
            {
                return forceAppliers[index];
            }
            else
            {
                return null;
            }
        }
        public TestingScene()
		{
			Player player = new Player(new Vector2(0, 0), new Vector2( .5f, .5f));
			AddChild(player);

			forceAppliers = new List<ForceApplier>();
			//TogglableForceApplier applier = new TogglableForceApplier("triangle.png", new Vector2(0, -0.1f), true, false, 0, 0, 1000, 0);
			//applier.SetXY(game.width / 2, game.height - applier.height / 2 - 200);
			//AddChild(applier);
			//forceAppliers.Add(applier);
		}
	}
}
