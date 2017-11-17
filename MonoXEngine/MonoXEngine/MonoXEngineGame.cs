using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        /// Two level string dictionary that represents MainSettings.xml data
        /// </summary>
        public Dictionary<string, Dictionary<string, object>> MainSettings { get; private set; }

        /// <summary>
        /// SceneManager
        /// </summary>
        public SceneManager SceneManager;

        /// <summary>
        /// ViewportTexture
        /// </summary>
        public ViewportTexture ViewportTexture;

        /// <summary>
        /// SpriteBatchLayers
        /// </summary>
        public Dictionary<string, SpriteBatchLayer> SpriteBatchLayers;

        public T GetSetting<T>(string Key, string Key2)
        {
            return (T)Convert.ChangeType(this.MainSettings[Key][Key2], typeof(T));
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
            this.MainSettings = new Dictionary<string, Dictionary<string, object>>();

            XmlDocument mainSettingsDocument = new XmlDocument();
            mainSettingsDocument.Load(@"MainSettings.xml");

            foreach(XmlNode node in mainSettingsDocument.SelectSingleNode("MainSettings").ChildNodes)
            {
                this.MainSettings.Add(node.Name, new Dictionary<string, object>());

                foreach(XmlNode node2 in node.ChildNodes)
                    this.MainSettings[node.Name].Add(node2.Name, node2.InnerText);
            }

            // Set Global.Game
            Global.Game = this;

            // GraphicsDeviceManager
            this.Graphics = new GraphicsDeviceManager(this);

            // Content RootDirectory
            Content.RootDirectory = this.GetSetting<string>("Directories", "Content");

            // Window resizing
            if(this.GetSetting<string>("Viewport", "AllowResizing").ToLower() == "true")
            {
                Window.AllowUserResizing = true;
                Window.ClientSizeChanged += delegate {
                    this.ViewportTexture.WindowSizeUpdate();
                };
            }
        }

        protected override void Initialize()
        {
            Global.Cameras = new List<Camera>(){new Camera()};
            Global.Camera = Global.Cameras[0];
            Global.Resolution = new Point(
                this.GetSetting<int>("Viewport", "ResolutionX"),
                this.GetSetting<int>("Viewport", "ResolutionY")
            );

            this.SpriteBatchLayers = new Dictionary<string, SpriteBatchLayer>();
            this.SceneManager = new SceneManager();
            this.ViewportTexture = new ViewportTexture(Global.Resolution, this.GetSetting<string>("Viewport", "ViewportArea"));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach(KeyValuePair<string, object> Layer in this.MainSettings["Layers"])
            {
                SpriteBatchLayer spriteBatchLayer = new SpriteBatchLayer(Layer.Value.ToString());
                this.SpriteBatchLayers.Add(Layer.Key, spriteBatchLayer);
            }
            
            this.SceneManager.LoadScene(this.GetSetting<string>("Initiation", "StartupScene"));
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            Global.GameTime = gameTime;
            Global.DeltaTime = (float)Global.GameTime.ElapsedGameTime.TotalSeconds;
            Coroutines.Update();
            this.SceneManager.CurrentScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            this.ViewportTexture.CaptureAndRender(this, () => {
                GraphicsDevice.Clear(Color.White);
                foreach (KeyValuePair<string, SpriteBatchLayer> SpriteBatchLayer in SpriteBatchLayers)
                    SpriteBatchLayer.Value.Draw(gameTime);
            });

            base.Draw(gameTime);
        }
    }
}
