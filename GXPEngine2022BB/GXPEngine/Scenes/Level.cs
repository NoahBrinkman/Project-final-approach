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
      /*  public int lives
        {
            get;
            private set;
        }

        public int enemiesLeft
        {
            get;
            private set;
        }

        public SoundEffectGameObject sfxHandler;
        
        public Action onLevelComplete;
        private string enemyMapFileName;
        
        private bool levelCompleted = false;
        private float timeBetweenLevelTimer;
        public Level(int lives, float timeBetweenLevelShift, string fileName = "") : base(true)
        {
            this.lives = lives;
            enemyMapFileName = fileName;
            timeBetweenLevelTimer = timeBetweenLevelShift;
            sfxHandler = new SoundEffectGameObject();
            AddChild(sfxHandler);
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
            Emitter emitter = new Emitter("StarParticle.png", 1, 1, 1, 100, .9f,BlendMode.NORMAL);
            emitter.SetVelocity(90,90,15, 25);
            emitter.SetScale(.7f, 1);
            emitter.SetSpawnPosition(0,0, game.width, game.width , 0, game.height);
            AddChild(emitter);
            if (enemyMapFileName != "")
            {
                TiledLoader levelMap = new TiledLoader(enemyMapFileName, this);
                levelMap.autoInstance = true;
                levelMap.LoadObjectGroups(0);
            }

            List<Enemy> enemies = FindObjectsOfType<Enemy>().ToList();
            
            PlayerController player = new PlayerController("Player_Triangle_Sheet.png",3,4,1, 4);
            player.SetOrigin(player.width / 2, player.height / 2);
            player.x = 100;
            player.y = game.height / 2 - player.height;
            player.onBlasterShot += sfxHandler.PlaySoundEffect;
            
            Pivot finishLinePivot = new Pivot();
            finishLinePivot.x = player.x;
            
            AddChild(finishLinePivot);
            AddChild(player);
            
            
            foreach (Enemy enemy in enemies)
            {
                enemy.SetTargetPosition(finishLinePivot);
                AddChild(enemy);
                enemy.onEnemyDestroyed += RemoveFromEnemiesLeft;
                enemy.onEnemyFinishedDestoying  += sfxHandler.PlaySoundEffect;
                enemy.isActive = true;
            }
            Console.WriteLine(enemies.Count);
            List<GameObject> children = GetChildren();
            children.ForEach(x => x.visible = true);
            enemiesLeft = enemies.Count;
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
           // Console.WriteLine(enemiesLeft);
            if (enemiesLeft <= 0 && !levelCompleted)
            {
                levelCompleted = true;
            }
            if (levelCompleted) timeBetweenLevelTimer -= (float)Time.deltaTime / 1000;
            if (timeBetweenLevelTimer <= 0)
            {
                sfxHandler.PlaySoundEffect(SoundEffectType.LevelComplete);
                SceneManager.instance.TryLoadNextScene();
                PlayerController player =  FindObjectOfType<PlayerController>();
                if (player != null)
                {
                    player.LateRemove();
                    player.LateDestroy();
                    base.isActive = false;
                }
            }

            if (lives <= 0)
            {
                SceneManager.instance.LoadLastScene();
            }
        }
        
        /// <summary>
        /// Remove an enemy and possibly lives
        /// </summary>
        /// <param name="enemyToRemove"></param>
        public void RemoveFromEnemiesLeft(bool enemyReachedFinish)
        {
            if (enemyReachedFinish) lives--;
            enemiesLeft--;
            if (enemiesLeft <= -1)
            {
                enemiesLeft = 0;
            }
        }*/

    }
}