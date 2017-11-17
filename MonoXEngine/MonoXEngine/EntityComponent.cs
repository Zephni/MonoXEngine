using System;
using System.Collections;

namespace MonoXEngine
{
    public class EntityComponent
    {
        private Action<EntityComponent> CallBack;

        private Entity _entity;
        public Entity entity
        {
            get { return _entity; }
            set {
                _entity = value;
                this.Start();
                if(this.CallBack != null)
                {
                    this.CallBack(this);
                    this.CallBack = null;
                }
            }
        }

        public EntityComponent(Action<EntityComponent> callBack = null)
        {
            this.CallBack = callBack;
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }
    }
}
