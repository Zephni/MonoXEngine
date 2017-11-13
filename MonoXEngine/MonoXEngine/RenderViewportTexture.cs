using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    public class RenderViewportTexture
    {
        public RenderTarget2D RenderTarget;

        private SpriteBatch spriteBatch;

        public RenderViewportTexture(GraphicsDevice graphicsDevice, int XResolution, int YResolution)
        {
            this.RenderTarget = new RenderTarget2D(
               graphicsDevice,
               XResolution,
               YResolution,
               false,
               graphicsDevice.PresentationParameters.BackBufferFormat,
               DepthFormat.Depth24
            );

            this.spriteBatch = new SpriteBatch(graphicsDevice);
        }

        private void BeginCapture(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(this.RenderTarget);
            graphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
        }

        private void EndCapture(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(null);
        }

        private void RenderToViewport(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
               SamplerState.LinearClamp, DepthStencilState.Default,
               RasterizerState.CullNone);

            spriteBatch.Draw(this.RenderTarget, new Rectangle(0, 0, graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight), Color.White);

            spriteBatch.End();
        }

        public void CaptureAndRender(GraphicsDevice graphicsDevice, Dictionary<string, SpriteBatchLayer> SpriteBatchLayers, Action drawCalls)
        {
            this.BeginCapture(graphicsDevice);
            drawCalls();
            this.EndCapture(graphicsDevice);
            this.RenderToViewport(graphicsDevice);
        }
    }
}