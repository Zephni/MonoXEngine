using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    public struct Tile
    {
        public Point SourcePosition;
        public Point Position;
        public Dictionary<string, string> Options;

        public Tile(Point sourcePosition, Point position, string options = "")
        {
            this.SourcePosition = sourcePosition;
            this.Position = position;
            this.Options = new Dictionary<string, string>(); // Need to add options here
        }
    }
}
