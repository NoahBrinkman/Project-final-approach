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
        private Square player;
        private Sprite background;
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
        /// 
        /// 
        /// 
        /// </summary>
        protected override void Start()
        {
            Console.WriteLine("Start");
            visible = true;
            if (fileName != "")
            {
                TiledLoader levelMap = new TiledLoader(fileName, this);
                levelMap.addColliders = false;
                levelMap.autoInstance = false;
                levelMap.LoadImageLayers();
                //levelMap.LoadTileLayers();
                levelMap.autoInstance = true;
                levelMap.addColliders = true;
                levelMap.LoadObjectGroups();

                List<GameObject> children = GetChildren();
				for (int i = 0; i < children.Count; i++)
				{
                    if(children[i] is ForceApplier)
					{
                        ForceApplier forceApplier = (ForceApplier)children[i];
                        forceAppliers.Add(forceApplier);
					}

                    if(children[i].name is "PH_BackgroundDouble.png")
                    {
                        background = (Sprite)children[i];
                    }
                    
				}
            }
            LevelCamera levelCamera = new LevelCamera(game.width/2, game.width * 1.5f);
            AddChild(levelCamera);
            levelCamera.SetXY(game.width/2,game.height/2);
            Square player = FindObjectOfType<Square>();
            if(player != null)
            {
                this.player = player;
                Console.WriteLine("Added playerDeath");
                player.death += OnPlayerDeath;
                this.player.cam = levelCamera;
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

        public void PlayerStartedMoving()
        {
            List<ForceApplier> tForceAppliers = forceAppliers.Where(f => f is TogglableForceApplier).ToList();
            foreach (ForceApplier tForceApplier in tForceAppliers)
            {
                TogglableForceApplier toggleForceApplier = (TogglableForceApplier)tForceApplier;
                toggleForceApplier.activatable = false;
            }
        }
        public void PlayerStoppedMoving()
        {
            List<ForceApplier> tForceAppliers = forceAppliers.Where(f => f is TogglableForceApplier).ToList();
            foreach (ForceApplier tForceApplier in tForceAppliers)
            {
                TogglableForceApplier toggleForceApplier = (TogglableForceApplier)tForceApplier;
                if(toggleForceApplier.shouldBeActivatable)
                    toggleForceApplier.activatable = true;
            }

            player.cam.canDrag = true;
        }
        
        public Vector2 GetBorders()
        {
            return(new Vector2(background.width, background.height));
        }

        public void OnPlayerDeath()
		{
            SceneManager.instance.LoadLastSceneInBuildIndex();
		}

        private void OnGoalHit()
		{
         SceneManager.instance.TryLoadNextScene();
        }
    }
}