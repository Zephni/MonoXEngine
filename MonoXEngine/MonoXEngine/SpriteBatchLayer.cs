using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    

    public class SpriteBatchLayer
    {
        private SpriteBatch SpriteBatch;
        public List<Entity> Entities;

        public Func<Matrix> MatrixUpdater;
        private Matrix transformMatrix;

        public static SpriteBatchLayer Get(string Key)
        {
            return MonoXEngineGame.Instance.SpriteBatchLayers[Key];
        }

        public SpriteBatchLayer(GraphicsDevice graphicsDevice, string layerOptions)
        {
            this.SpriteBatch = new SpriteBatch(graphicsDevice);
            this.Entities = new List<Entity>();
            this.ApplyOptions(layerOptions);
        }

        public void ApplyOptions(string layerOptions)
        {
            // Default transformMatrix to snapshot of camera transformation before it has any changes applied
            this.transformMatrix = Camera.Main.GetTransformation(MonoXEngineGame.Instance.RenderViewportTexture.Resolution);

            if (layerOptions.Trim().Length == 0)
                return;

            foreach (string option in layerOptions.Split(';'))
            {
                string[] optionKV = option.Split(':');

                if (optionKV.Length != 2)
                    continue;

                string key = optionKV[0].Trim().ToLower();
                string value = optionKV[1].Trim().ToLower();

                if(key == "matrix")
                {
                    if(value == "camera")
                    {
                        this.MatrixUpdater = delegate() {
                            return Camera.Main.GetTransformation(MonoXEngineGame.Instance.RenderViewportTexture.Resolution);
                        };
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if(this.MatrixUpdater != null)
                this.transformMatrix = this.MatrixUpdater();

            this.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                this.transformMatrix
            );

            foreach (Entity entity in Entities)
                entity.Draw(gameTime, this.SpriteBatch);

            this.SpriteBatch.End();
        }
    }
}
