using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoXEngine.EntityComponents;
using Microsoft.Xna.Framework.Input;

namespace MonoXEngine.Entities
{
    public class Platformer : Entity
    {
        Sprite sprite;

        float moveX = 0;
        float moveY = 0;
        int jump = 0;
        int maxJumps = 1;

        public Platformer()
        {
            this.sprite = this.AddComponent<Sprite>();
            this.sprite.BuildRectangle(new Point(16, 16), Color.Purple);
        }

        public override void Update()
        {
            base.Update();

            // Input
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                moveX -= 0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                moveX += 0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if(jump < maxJumps)
                {
                    moveY -= 6;
                    jump += 1;
                }
            }
            
            // Gravity
            moveY += (moveY < 3) ? 0.5f : 0;

            // Deceleration
            

            // Max X
            if (moveX > 3) moveX = 3;
            if (moveX < -3) moveX = -3;

            for (int x = 0; x < Math.Abs(moveX); x++)
                this.Position.X += (moveX > 0) ? 1 : -1;
            for (int y = 0; y < Math.Abs(moveY); y++)
            {
                // Collide with floor
                if (this.Position.Y > 0 || this.Position.Y + moveY > 0)
                {
                    jump = 0;
                    moveY = 0;
                    break;
                }
                
                this.Position.Y += (moveY > 0) ? 1 : -1;
            }
        }
    }
}
