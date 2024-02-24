namespace TrashMetadata
{
    public class PlasticWaterBottle : ITrashMetadata
    {
        public override string Name => "Plastic Water Bottle";
        public override string Description =>
            "A disposable plastic water bottle made with PET (polyethylene terephthalate) can take 450 years or more to decompose and as they break down they release harmful microplastics into the enviornment. Also plastic is frequently consumed by wildlife killing 1.1 million marine creatures per year";
        public override string DegradeTime => "~450 years";
        public override bool Recyclable => true;
        public override bool Compostable => false;
        public override bool IsBiodegradable => false;
        public override bool IsReusable => false;
        public override int PointValue => 30;
    }
}