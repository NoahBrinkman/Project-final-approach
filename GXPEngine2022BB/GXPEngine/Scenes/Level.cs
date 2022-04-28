using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Xsl;
using TiledMapParser;

namespace GXPEngine
{
    /// <summary>
    /// A level withing the game based on an enemy map provided by tiled.
    /// Will be changed to something we can actually use
    /// </summary>
    public class Level : Scene
    {
  
        public Action onLevelComplete;
        private string fileName;
        
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
        public Level( string fileName = "") : base(true)
        {
            this.fileName = fileName;
            forceAppliers = new List<ForceApplier>();
        }
        
        
        /// <summary>
        ///Set up the level.
        /// Start the player in it's position
        /// Spawn all enemies
        /// Count how many there are and set up enemies left
        /// </summary>
        protected override void Start()
        {
            isActive = true;
            visible = true;
            if (fileName != "")
            {
                TiledLoader levelMap = new TiledLoader(fileName, this);
                levelMap.autoInstance = true;
                levelMap.LoadImageLayers();
                //levelMap.LoadTileLayers();
                levelMap.LoadObjectGroups();
            }
			foreach (ForceApplier item in FindObjectsOfType<ForceApplier>())
			{
                if(item.parent == this)
				{
                Console.WriteLine("Have been added");
                    forceAppliers.Add(item);
				}
                    
			}
        }

        /// <summary>
        /// Gets called every frame
        /// When all enemies are out of the game. Complete the level
        /// When hte level is considered complete. Start a timer. When this timer is complete go to the next level
        /// When you are out of health. Tell the game to end it.
        /// </summary>
        protected override void Update()
        {
            if (!base.isActive)
            {
                return;
            }

        }


    }
}