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
    public class Level : GameObject
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
            Console.WriteLine("Level Initialized");
        }
        
        
        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Start");
            visible = true;
            if (fileName != "")
            {
                TiledLoader levelMap = new TiledLoader(fileName, this);
                levelMap.autoInstance = true;
                levelMap.LoadImageLayers();
                //levelMap.LoadTileLayers();
                levelMap.LoadObjectGroups();
            }

            Square player = FindObjectOfType<Square>();
            if(player != null)
			{
                Console.WriteLine("Added playerDeath");
                player.death += OnPlayerDeath;
            }
            Goal goal = FindObjectOfType<Goal>(); 
            if(goal != null)
			{
                goal.goalHit += OnGoalHit;

            }
        }

        /// <summary>
        /// Gets called every frame
        /// When all enemies are out of the game. Complete the level
        /// When hte level is considered complete. Start a timer. When this timer is complete go to the next level
        /// When you are out of health. Tell the game to end it.
        /// </summary>
         void Update()
        {

        }

        public void OnPlayerDeath()
		{
            Console.WriteLine("Trying to load last scene");
            MyGame mg = (MyGame)game;
            mg.LoadGameOverScene();
            mg.DestroyScene(this);
		}

        private void OnGoalHit()
		{
            MyGame mg = (MyGame)game;
            mg.LoadCongratulationsScene();
            mg.DestroyScene(this);
        }
    }
}