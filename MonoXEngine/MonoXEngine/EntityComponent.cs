namespace MonoXEngine
{
    public class EntityComponent
    {
        private Entity _entity;
        public Entity entity
        {
            get { return _entity; }
            set { _entity = value; this.Start(); }
        }

        public virtual void Start()
        {

        }
    }
}
