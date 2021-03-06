﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticCoroutines;

namespace MonoXEngine.EntityComponents
{
    public class Sprite : Drawable
    {
        protected Dictionary<string, Animation> Animations;

        private int CurrentAnimationFrame;

        public string CurrentAnimation;

        public Sprite()
        {
            this.Animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(Animation animation)
        {
            this.Animations.Add(animation.Name, animation);
        }

        public Animation GetAnimation(string name)
        {
            return this.Animations[name];
        }

        /// <summary>
        /// RunAnimation
        /// </summary>
        /// <param name="name">Name of an animation that has already been added to this sprite</param>
        public void RunAnimation(string name)
        {
            if(CurrentAnimation != name)
            {
                CurrentAnimation = name;
                this.SourceRectangle = this.GetAnimation(CurrentAnimation).SourceRectangles[0];
                CurrentAnimationFrame = 0;
                AnimTimer = 0;
            }
        }

        private float AnimTimer = 0;

        public override void Update()
        {
            if(CurrentAnimation != null)
            {
                AnimTimer += Global.DeltaTime;

                if(AnimTimer >= this.GetAnimation(CurrentAnimation).FrameInterval)
                {
                    AnimTimer = 0;

                    CurrentAnimationFrame++;

                    if (CurrentAnimationFrame >= this.GetAnimation(CurrentAnimation).SourceRectangles.Count)
                        CurrentAnimationFrame = 0;

                    this.SourceRectangle = this.GetAnimation(CurrentAnimation).SourceRectangles[CurrentAnimationFrame];
                }
            }
        }
    }
}
