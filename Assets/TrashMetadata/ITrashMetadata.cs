namespace TrashMetadata
{
    public class ITrashMetadata
    {
        public virtual string Name { get; }
        public virtual string Description { get; }
        public virtual string DegradeTime { get; }
        public virtual bool Recyclable { get; }
        public virtual bool Compostable { get; }
        public virtual bool IsBiodegradable { get; }
        public virtual bool IsReusable { get; }
        public virtual int  PointValue { get; }
    }
}