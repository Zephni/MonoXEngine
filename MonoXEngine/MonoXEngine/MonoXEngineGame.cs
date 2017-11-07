using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace MonoXEngine
{
    public class MonoXEngineGame : Game
    {
        /// <summary>
        /// Static instance
        /// </summary>
        public static MonoXEngineGame Instance;

        /// <summary>
        /// GraphicsDeviceManager must be defined in construct
        /// </summary>
        private GraphicsDeviceManager Graphics;

        /// <summary>
        /// SpriteBatch
        /// </summary>
        private SpriteBatch SpriteBatch;

        /// <summary>
        /// Two level string dictionary that represents MainSettings.xml data
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> MainSettings { get; private set; }

        /// <summary>
        /// SceneManager
        /// </summary>
        public SceneManager SceneManager;

        /// <summary>
        /// RenderViewportTexture
        /// </summary>
        public RenderViewportTexture RenderViewportTexture;

        /// <summary>
        /// Get content manager
        /// </summary>
        public static ContentManager ContentManager{
            get {return MonoXEngineGame.Instance.Content;}
        }

        /// <summary>
        /// MonoXEngine constructor
        /// </summary>
        /// <param name="MainSettingsFile"></param>
        public MonoXEngineGame(string MainSettingsFile)
        {
            // Static instance
            MonoXEngineGame.Instance = this;

            // Pass MainSettings
            this.MainSettings = new Dictionary<string, Dictionary<string, string>>();

            XmlDocument mainSettingsDocument = new XmlDocument();
            mainSettingsDocument.Load(@"MainSettings.xml");

            foreach(XmlNode node in mainSettingsDocument.SelectSingleNode("MainSettings").ChildNodes)
            {
                this.MainSettings.Add(node.Name, new Dictionary<string, string>());

                foreach(XmlNode node2 in node.ChildNodes)
                    this.MainSettings[node.Name].Add(node2.Name, node2.InnerText);
            }

            // GraphicsDeviceManager
            this.Graphics = new GraphicsDeviceManager(this);

            // Content RootDirectory
            Content.RootDirectory = this.MainSettings["Directories"]["Content"];
        }

        protected override void Initialize()
        {
            this.SceneManager = new SceneManager();
            this.RenderViewportTexture = new RenderViewportTexture(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.SceneManager.LoadScene(this.MainSettings["Initiation"]["StartupScene"]);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.RenderViewportTexture.CaptureAndRender(GraphicsDevice, SpriteBatch, () => {
                this.Graphics.GraphicsDevice.Clear(Color.White);
                this.SpriteBatch.Begin();
                this.SceneManager.CurrentScene.Draw(gameTime, this.SpriteBatch);
                this.SpriteBatch.End();
            });

            base.Draw(gameTime);
        }
    }
}
