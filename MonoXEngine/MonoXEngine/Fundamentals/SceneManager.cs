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
            if(this.CurrentScene != null)
                this.CurrentScene.Destroy();

            Coroutines.routines.Clear();

            this.CurrentScene = scene;
            this.CurrentScene.Initialise();
        }

        public void LoadScene(string sceneName)
        {
            this.LoadScene(Activator.CreateInstance(Type.GetType("MonoXEngine.Scenes." + sceneName)) as Scene);
        }
    }
}
