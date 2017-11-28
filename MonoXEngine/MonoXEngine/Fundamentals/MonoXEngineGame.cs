﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

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
        /// SceneManager
        /// </summary>
        public SceneManager SceneManager;

        /// <summary>
        /// ViewportTexture
        /// </summary>
        public ViewportTexture ViewportTexture;

        /// <summary>
        /// MonoXEngine constructor
        /// </summary>
        /// <param name="MainSettingsFile"></param>
        public MonoXEngineGame(string MainSettingsFile)
        {
            // Static instance
            MonoXEngineGame.Instance = this;

            // Pass MainSettings
            Global.MainSettings = new DataSet();
            Global.MainSettings.FromXML(XDocument.Load(@"MainSettings.xml"));

            // Set Global.Game
            Global.Game = this;

            // GraphicsDeviceManager
            this.Graphics = new GraphicsDeviceManager(this);

            // Content RootDirectory
            Content.RootDirectory = Global.MainSettings.Get<string>(new string[] { "Directories", "Content" });

            // Window resizing
            if (Global.MainSettings.Get<string>("Viewport", "AllowResizing").ToLower() == "true")
            {
                Window.AllowUserResizing = true;
                Window.ClientSizeChanged += delegate {
                    this.ViewportTexture.WindowSizeUpdate();
                };
            }

            // Full screen
            if (Global.MainSettings.Get<string>("Viewport", "FullScreen").ToLower() == "true")
                Graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            Global.Cameras = new List<Camera>(){new Camera()};
            Global.Camera = Global.Cameras[0];
            Global.SpriteBatchLayers = new Dictionary<string, SpriteBatchLayer>();
            Global.Resolution = new Point(
                Global.MainSettings.Get<int>(new string[] { "Viewport", "ResolutionX" }),
                Global.MainSettings.Get<int>(new string[] { "Viewport", "ResolutionY" })
            );
            
            this.SceneManager = new SceneManager();
            this.ViewportTexture = new ViewportTexture(Global.Resolution, Global.MainSettings.Get<string>(new string[] { "Viewport", "ViewportArea" }));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach(KeyValuePair<string, object> layer in Global.MainSettings.GetGroup("Layers"))
            {
                SpriteBatchLayer spriteBatchLayer = new SpriteBatchLayer(Global.MainSettings.GetGroup("Layers/" + layer.Key));
                Global.SpriteBatchLayers.Add(layer.Key, spriteBatchLayer);
            }

            this.SceneManager.LoadScene(Global.MainSettings.Get<string>(new string[] { "Initiation", "StartupScene" }));

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

            foreach (KeyValuePair<string, SpriteBatchLayer> SpriteBatchLayer in Global.SpriteBatchLayers)
                SpriteBatchLayer.Value.Update();

            this.SceneManager.CurrentScene.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            this.ViewportTexture.CaptureAndRender(this, () => {
                GraphicsDevice.Clear(Color.White);
                foreach (KeyValuePair<string, SpriteBatchLayer> SpriteBatchLayer in Global.SpriteBatchLayers)
                    SpriteBatchLayer.Value.Draw();
            });

            base.Draw(gameTime);
        }
    }
}
