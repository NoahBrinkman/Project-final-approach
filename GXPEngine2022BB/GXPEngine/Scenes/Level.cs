using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Xsl;
using TiledMapParser;

namespace GXPEngine
{
    // Add canvas for overlay loss and win
    // Only update when !isVisible
    // OnGoal and OnDeath trigger right overlay                          
    // go back to level selecter/go back to main menu/restart(loss) || next level(win)
    // Make camera only follow target if it's not == to null

    /// <summary>
    /// A level withing the game based on an enemy map provided by tiled.
    /// Will be changed to something we can actually use
    /// </summary>
    public class Level : Scene
    {

        public Action onLevelComplete;
        private string fileName;
        private Player player;
        private Sprite background;
        public LevelCamera levelCamera;
        List<ForceApplier> forceAppliers;
        private Sound wonSound;
        private Sound lostSound;
        private float timer;
        private bool startTimer;
        public int collectablesCollected { get; private set; }
        private LevelOverlay overlay;
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
        public Level(string fileName = "") : base(true)
        {
            this.fileName = fileName;
            forceAppliers = new List<ForceApplier>();
        }

        protected override void Start()
        {
            lostSound = new Sound("Sound/LevelLost.wav", false, false);
            wonSound = new Sound("Sound/LevelWon.wav", false, false);
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
                    if (children[i] is ForceApplier)
                    {
                        ForceApplier forceApplier = (ForceApplier)children[i];
                        forceAppliers.Add(forceApplier);
                    }

                    if (children[i].name.Contains("Background"))
                    {
                        background = (Sprite)children[i];
                    }

                }
            }


            Player player = FindObjectOfType<Player>();
            LevelCamera levelCamera = new LevelCamera(game.width / 2, background.width - game.width / 2, player);
            levelCamera.SetXY(game.width / 2, game.height / 2);
            AddChild(levelCamera);
            if (player != null)
            {
                this.player = player;
                player.death += OnPlayerDeath;
                this.player.cam = levelCamera;
            }
            Goal goal = FindObjectOfType<Goal>();
            if (goal != null)
            {
                goal.goalHit += OnGoalHit;
            }

            overlay = new LevelOverlay(levelCamera);
            AddChild(overlay);
            overlay.TurnVisibility(false, 0);

            List<ForceApplier> tForceAppliers = forceAppliers.Where(f => f is TogglableForceApplier).ToList();
            foreach (ForceApplier tForceApplier in tForceAppliers)
            {
                TogglableForceApplier toggleForceApplier = (TogglableForceApplier)tForceApplier;
                toggleForceApplier.SetLevelCamera(levelCamera);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(Key.D))
            {
                Console.WriteLine(GetChildCount());
            }

            if (startTimer)
            {
                timer -= (float)Time.deltaTime / 1000;
                if (timer < 0)
                {
                    EndLevel();
                }
            }

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
                if (toggleForceApplier.shouldBeActivatable)
                    toggleForceApplier.activatable = true;
            }

            foreach (GameObject obj in GetChildren())
            {
                if (obj is Collectable)
                {
                    obj.visible = true;
                    collectablesCollected = 0;
                }
            }
            player.cam.canDrag = true;
        }

        public Vector2 GetBorders()
        {
            return (new Vector2(background.width, background.height));
        }

        public void OnPlayerDeath()
        {
            player.StopSimulating();
            lostSound.Play();
            Timer(1.5f, true);
        }

        private void OnGoalHit()
        {
            EndLevel();
            wonSound.Play();
            overlay.hasWon = true;
        }

        private void EndLevel()
        {
            Timer(0, false);
            overlay.TurnVisibility(true, collectablesCollected);
            player.LateDestroy();
        }

        private void Timer(float amount, bool state)
        {
            timer = amount;
            startTimer = state;
        }

        public override void Reload()
        {
            forceAppliers = new List<ForceApplier>();
            collectablesCollected = 0;
            base.Reload();
        }


        public void CollectableCollected()
        {
            collectablesCollected++;
        }

    }
}