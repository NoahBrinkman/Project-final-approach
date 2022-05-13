using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GXPEngine
{
    /// <summary>
    /// A singleton class made to handle the loading unloading and switching of scenes.
    /// </summary>
    public class SceneManager : GameObject
    {
        private List<Scene> scenes;

        private static SceneManager _instance = null;
        public Scene activeScene { get; private set; }

        private int firstLevelIndex = -1;
        public static SceneManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManager();
                    Console.WriteLine("making new Scene manager");
                }

                return _instance;
            }
        }

        public SceneManager()
        {
            if (!game.GetChildren().Contains(this))
            {
                game.AddChild(this);
            }
        }

        public void AddScene(Scene scene)
        {
            bool firstAdded = false;
            if (scenes == null || scenes.Count == 0)
            {
                scenes = new List<Scene>();
                firstAdded = true;
            }


            scenes.Add(scene);
            if (scene is Level && firstLevelIndex == -1)
            {
                firstLevelIndex = scenes.IndexOf(scene);
            }
            if (firstAdded)
            {
                activeScene = scene;
                AddChild(activeScene);
            }
            
        }

        public void LoadScene(int buildIndex)
        {
            if (buildIndex > 0 && buildIndex < scenes.Count)
            {
                activeScene.LateDestroy();
                activeScene = scenes[buildIndex];
                activeScene.LoadScene();
                LateAddChild(activeScene);
            }

            if(buildIndex == firstLevelIndex)
            {
                Comic comic = new Comic();
                LateAddChild(comic);
            }

            if (buildIndex == scenes.Count - 1)
            {
                Comic endComic = new Comic("End_Comic.png", 4, 1);
                LateAddChild(endComic);
            }
        }
        public void ReloadActiveScene()
        {
            activeScene.Reload();
        }
        public void LoadLastSceneInBuildIndex()
        {
            LoadScene(scenes.Count-1);
        }

        public void TryLoadNextScene()
        {
            LoadScene(scenes.IndexOf(activeScene) + 1);
        }

        public void WipeScenes()
        {
            scenes.Clear();
            activeScene.LateDestroy();
            firstLevelIndex = -1;
            scenes = null;
        }
    }
}
