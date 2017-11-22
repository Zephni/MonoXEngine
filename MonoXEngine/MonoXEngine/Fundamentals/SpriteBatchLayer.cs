using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    public class SpriteBatchLayer
    {
        private SpriteBatch SpriteBatch;
        public List<Entity> Entities;

        public SamplerState SamplerState;

        public Func<Matrix> MatrixUpdater;
        private Matrix TransformMatrix;

        public static SpriteBatchLayer Get(string Key)
        {
            return MonoXEngineGame.Instance.SpriteBatchLayers[Key];
        }

        public SpriteBatchLayer(string layerOptions)
        {
            this.SpriteBatch = new SpriteBatch(Global.GraphicsDevice);
            this.Entities = new List<Entity>();
            this.ApplyOptions(layerOptions);
        }

        public void ApplyOptions(string layerOptions)
        {
            // Defaults
            this.TransformMatrix = Global.Camera.GetTransformation();
            this.SamplerState = SamplerState.PointClamp;

            if (layerOptions.Trim().Length == 0)
                return;

            foreach (string option in layerOptions.Split(';'))
            {
                string[] optionKV = option.Split(':');

                if (optionKV.Length != 2)
                    continue;

                string key = optionKV[0].Trim().ToLower();
                string value = optionKV[1].Trim();

                if(key == "matrix")
                {
                    if(value.ToLower() == "camera")
                    {
                        this.MatrixUpdater = delegate() {
                            return Global.Camera.GetTransformation();
                        };
                    }
                }
                else if(key == "samplerstate")
                {
                    var field = typeof(SamplerState).GetField(value, BindingFlags.Public | BindingFlags.Static);
                    var samplerstate = (SamplerState)field.GetValue(null);
                    this.SamplerState = samplerstate;
                }
            }
        }

        public void Update()
        {
            foreach (Entity entity in Entities)
                entity.Update();
        }

        public void Draw()
        {
            if(this.MatrixUpdater != null)
                this.TransformMatrix = this.MatrixUpdater();

            this.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                this.SamplerState,
                null,
                null,
                null,
                this.TransformMatrix
            );

            foreach (Entity entity in Entities)
                entity.Draw(this.SpriteBatch);

            this.SpriteBatch.End();
        }
    }
}
