using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine.EntityComponents
{
    public class TileMapCollider : BaseCollider 
    {
        public TileMap tileMap;

        public TileMapCollider(TileMap _tileMap)
        {
            tileMap = _tileMap;
        }

        public override bool CollidingRect(Rectangle rectOffset)
        {
            Rectangle checkArea = new Rectangle(
                (this.Entity.Position.ToPoint() - (this.Entity.Size/2).ToPoint()) + rectOffset.Location,
                this.Entity.Size.ToPoint()
            );

            if (tileMap.IsRectOverlappingPixels(checkArea))
                return true;

            return false;
        }
    }
}
