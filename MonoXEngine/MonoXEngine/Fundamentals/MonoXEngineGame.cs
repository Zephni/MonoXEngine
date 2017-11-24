using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

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
        /// MainSettings is a DataSet pulling from MainSettings.xml data
        /// </summary>
        public DataSet MainSettings = new DataSet();

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

        /// <summary>
        /// MonoXEngine constructor
        /// </summary>
        /// <param name="MainSettingsFile"></param>
        public MonoXEngineGame(string MainSettingsFile)
        {
            // Static instance
            MonoXEngineGame.Instance = this;

            // Pass MainSettings
            this.MainSettings = new DataSet();
            this.MainSettings.FromXML(XDocument.Load(@"MainSettings.xml"));

            // Set Global.Game
            Global.Game = this;

            // GraphicsDeviceManager
            this.Graphics = new GraphicsDeviceManager(this);

            // Content RootDirectory
            Content.RootDirectory = this.MainSettings.Get<string>(new string[] { "Directories", "Content" });

            // Window resizing
            if(this.MainSettings.Get<string>(new string[] { "Viewport", "AllowResizing" }).ToLower() == "true")
            {
                Window.AllowUserResizing = true;
                Window.ClientSizeChanged += delegate {
                    this.ViewportTexture.WindowSizeUpdate();
                };
            }

            // Full screen
            if (this.MainSettings.Get<string>(new string[] { "Viewport", "FullScreen" }).ToLower() == "true")
                Graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            Global.Cameras = new List<Camera>(){new Camera()};
            Global.Camera = Global.Cameras[0];
            Global.Resolution = new Point(
                this.MainSettings.Get<int>(new string[] { "Viewport", "ResolutionX" }),
                this.MainSettings.Get<int>(new string[] { "Viewport", "ResolutionY" })
            );

            this.SpriteBatchLayers = new Dictionary<string, SpriteBatchLayer>();
            this.SceneManager = new SceneManager();
            this.ViewportTexture = new ViewportTexture(Global.Resolution, this.MainSettings.Get<string>(new string[] { "Viewport", "ViewportArea" }));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach(string layerName in this.MainSettings.Get<string>(new string[] { "LayerNames" }).Split(','))
            {
                SpriteBatchLayer spriteBatchLayer = new SpriteBatchLayer(this.MainSettings.Get<string>(new string[] { "Layers", layerName.Trim() }));
                this.SpriteBatchLayers.Add(layerName.Trim(), spriteBatchLayer);
            }

            this.SceneManager.LoadScene(this.MainSettings.Get<string>(new string[] { "Initiation", "StartupScene" }));

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

            foreach (KeyValuePair<string, SpriteBatchLayer> SpriteBatchLayer in SpriteBatchLayers)
                SpriteBatchLayer.Value.Update();

            this.SceneManager.CurrentScene.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            this.ViewportTexture.CaptureAndRender(this, () => {
                GraphicsDevice.Clear(Color.White);
                foreach (KeyValuePair<string, SpriteBatchLayer> SpriteBatchLayer in SpriteBatchLayers)
                    SpriteBatchLayer.Value.Draw();
            });

            base.Draw(gameTime);
        }
    }
}
