using System;

namespace MonoXEngine
{
    public class SceneManager
    {
        public Scene CurrentScene { get; set; }

        public SceneManager()
        {

        }

        public void LoadScene(Scene scene)
        {
            this.CurrentScene = scene;
        }

        public void LoadScene(string sceneName)
        {
            this.CurrentScene = Activator.CreateInstance(Type.GetType("MonoXEngine.Scenes." + sceneName)) as Scene;
        }
    }
}
