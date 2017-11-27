using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine.EntityComponents
{
    public class PlatformerController : Physics
    {
        public float Acceleration;
        public float Deceleration;
        public float JumpStrength;
        public int CurrentJump = 0;
        public int MaxJumps = 2;

        BaseCollider passThru;

        public PlatformerController(BaseCollider collider)
        {
            Acceleration = 0.1f;
            Deceleration = 0.1f;
            JumpStrength = 5f;
            passThru = collider;
        }

        public override void Start()
        {
            this.Entity.AddComponent(passThru);
            this.Collider = passThru;
            passThru = null;
        }

        public override void Update()
        {
            if (CurrentJump < MaxJumps && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                MoveY = -JumpStrength;
                CurrentJump++;
            }

            if (IsGrounded)
                CurrentJump = 0;                

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                MoveX += -Acceleration;
            else if(MoveX < 0)
            {
                MoveX += Deceleration;
                if (MoveX >= -Deceleration) MoveX = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                MoveX += Acceleration;
            else if (MoveX > 0)
            {
                MoveX -= Deceleration;
                if (MoveX <= Deceleration) MoveX = 0;
            }

            base.Update();
        }
    }
}
