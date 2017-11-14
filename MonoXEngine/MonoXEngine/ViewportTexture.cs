using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonoXEngine
{
    public class ViewportTexture
    {
        public RenderTarget2D RenderTarget;

        private SpriteBatch spriteBatch;

        private Point WindowSize;
        private Point Resolution;
        private Rectangle TextureRect;

        public ViewportTexture(GraphicsDevice graphicsDevice, Point resolution, string viewportArea = "FIT_Y")
        {
            this.Resolution = resolution;
            this.WindowSize = new Point(graphicsDevice.PresentationParameters.BackBufferWidth, graphicsDevice.PresentationParameters.BackBufferHeight);
            this.TextureRect = this.GetRect(graphicsDevice, viewportArea);

            this.RenderTarget = new RenderTarget2D(
               graphicsDevice,
               this.Resolution.X,
               this.Resolution.Y,
               false,
               graphicsDevice.PresentationParameters.BackBufferFormat,
               DepthFormat.Depth24
            );

            this.spriteBatch = new SpriteBatch(graphicsDevice);
        }

        private Rectangle GetRect(GraphicsDevice graphicsDevice, string fitType)
        {
            if (fitType == "FIT_Y")
            {
                float otherRatio = (this.WindowSize.Y - this.Resolution.Y) / (float)this.Resolution.Y;
                Point size = new Point(this.Resolution.X + (int)(this.Resolution.X * otherRatio), this.Resolution.Y + (int)(this.Resolution.Y * otherRatio));

                return new Rectangle(
                    this.WindowSize.X / 2 - size.X / 2,
                    this.WindowSize.Y / 2 - size.Y / 2,
                    size.X,
                    size.Y
                );
            }
            else if (fitType == "FIT_X")
            {
                float otherRatio = (this.WindowSize.X - this.Resolution.X) / (float)this.Resolution.X;
                Point size = new Point(this.Resolution.X + (int)(this.Resolution.X * otherRatio), this.Resolution.Y + (int)(this.Resolution.Y * otherRatio));

                return new Rectangle(
                    this.WindowSize.X / 2 - size.X / 2,
                    this.WindowSize.Y / 2 - size.Y / 2,
                    size.X,
                    size.Y
                );
            }
            else if (fitType == "STRETCH")
            {
                return new Rectangle(0, 0, this.WindowSize.X, this.WindowSize.Y);
            }
            else if (fitType == "FIT_BEST")
            {
                float scale = Math.Min((float)this.WindowSize.X / (float)this.Resolution.X, (float)this.WindowSize.Y / (float)this.Resolution.Y);

                Point size = new Point((int)((float)this.Resolution.X * scale), (int)((float)this.Resolution.Y * scale));

                return new Rectangle(
                    this.WindowSize.X / 2 - size.X / 2,
                    this.WindowSize.Y / 2 - size.Y / 2,
                    size.X,
                    size.Y
                );

            }
            else if (fitType.Substring(0, 7) == "CUSTOM ")
            {
                string[] parts = new string[4];
                parts = fitType.Substring(7).Split(' ');

                int[] rectParts = new int[4];

                for(int I = 0; I < parts.Length; I++)
                    rectParts[I] = Convert.ToInt16(parts[I]);

                return new Rectangle(rectParts[0], rectParts[1], rectParts[2], rectParts[3]);
            }

            return this.GetRect(graphicsDevice, "FIT_BEST");
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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
               SamplerState.LinearClamp, DepthStencilState.Default,
               RasterizerState.CullNone);

            spriteBatch.Draw(this.RenderTarget, this.TextureRect, Color.White);

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