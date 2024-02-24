namespace TrashMetadata
{
    public class Battery : ITrashMetadata
    {
        public override string Name => "Battery";
        public override string Description =>
            "Batteries take around 100 years to decompose and as they do so, they release harmful chemicals and heavy metals to the environment.";
        public override string DegradeTime => "100 years";
        public override bool Recyclable => false; //Technically batteries can be recycled but you have to get a battery recycling kit or give them to a specific company
        public override bool Compostable => false;
        public override bool IsBiodegradable => false;
        public override bool IsReusable => false;
        public override int PointValue => 20;
    }
}