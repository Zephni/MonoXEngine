using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoXEngine
{
    public struct Animation
    {
        /// <summary>
        /// Name of this animation
        /// </summary>
        public string Name;

        /// <summary>
        /// List of source rectangles
        /// </summary>
        public List<Rectangle> SourceRectangles;

        /// <summary>
        /// Time in seconds between frames
        /// </summary>
        public float FrameInterval;

        /// <summary>
        /// Animation construct
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sourceRectangles"></param>
        /// <param name="frameInterval"></param>
        public Animation(string name, List<Rectangle> sourceRectangles, float frameInterval)
        {
            this.Name = name;
            this.SourceRectangles = sourceRectangles;
            this.FrameInterval = frameInterval;
        }
    }
}