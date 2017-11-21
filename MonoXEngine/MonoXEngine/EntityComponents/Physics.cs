using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine.EntityComponents
{
    public class Physics : EntityComponent
    {
        public bool Kinetic;
        public float Gravity;
        public float MaxX;
        public float MaxY;
        public float MoveX;
        public float MoveY;
        public bool IsGrounded;
        public BaseCollider Collider;

        public override void Start()
        {
            base.Start();

            this.Kinetic = false;
            this.Gravity = 0.3f;
            this.MaxX = 3;
            this.MaxY = 7;
            this.MoveX = 0;
            this.MoveY = 0;
            this.IsGrounded = false;
        }

        public override void Update()
        {
            base.Update();

            if (Collider == null)
                return;
            
            if(!this.Kinetic)
            {
                // Apply gravity if not grounded
                if (!this.IsGrounded)
                    this.MoveY += this.Gravity;

                // Clamp values
                this.MoveX = Math.Min(Math.Max(this.MoveX, -this.MaxX), this.MaxX);
                this.MoveY = Math.Min(Math.Max(this.MoveY, -this.MaxY), this.MaxY);

                // X move
                for (int X = 0; X < Math.Abs(this.MoveX); X++)
                {
                    if (Collider.CollidingRect(new Rectangle(new Point(0, 0), new Point(0, 0))))
                        this.MoveX = 0;

                    if (this.MoveX != 0)
                        this.entity.Position.X += (this.MoveX > 0) ? 1 : -1;
                }

                // Y move
                for (int Y = 0; Y < Math.Abs(this.MoveY); Y++)
                {
                    if (Collider.CollidingRect(new Rectangle(new Point(0, (MoveY > 0) ? 1 : -1), new Point(0, 0))))
                        this.MoveY = 0;

                    if (this.MoveY != 0)
                        this.entity.Position.Y += (this.MoveY > 0) ? 1 : -1;
                }

                // Check if grounded or too deep in ground
                this.IsGrounded = false;
                if (Collider.CollidingRect(new Rectangle(new Point(0, 1), new Point(0, 0))))
                    this.IsGrounded = true;
            }
        }


    }
}
