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
        public override void Start()
        {
            base.Start();
        }

        public override bool CollidingRect(Rectangle rectOffset)
        {
            Rectangle CheckArea = new Rectangle(
                this.entity.Position.ToPoint() + rectOffset.Location,
                rectOffset.Size
            );

            if (CheckArea.Bottom > 0)
                return true;

            return false;
        }
    }
}
