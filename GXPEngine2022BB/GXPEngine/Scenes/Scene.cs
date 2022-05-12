
using System;

namespace GXPEngine
{
    /// <summary>
    /// This is a scene withing the game.
    /// A massive parent object that gets activated and deactivated by the scenemanager.
    /// </summary>
    public class Scene : GameObject
    {
        public Scene(bool displayHUD = false)
        {
        }
        
        /// <summary>
        /// call the protected start method. 
        /// </summary>
        public virtual void LoadScene() 
        {
            Console.WriteLine("LoadScene started");
            Start();
        }

        public virtual void Reload()
        {
            UnLoadScene();
            LoadScene();
        }
        /// <summary>
        /// Remove all objects from this scene (softUnload means that objects will only be disabled)
        /// </summary>
        public virtual void UnLoadScene()
        {
            foreach (GameObject gameObject in GetChildren())
                {
                    if(gameObject != SceneManager.instance)
                    gameObject.Destroy();
                }
        }
        
        protected virtual void Update()
        {
           
        }
        
        protected virtual void Start()
        {
            visible = true;
            GetChildren().ForEach(x => x.visible = true);
        }
    }
}