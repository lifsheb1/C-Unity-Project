namespace TrashMetadata
{
    public class PlasticCup : ITrashMetadata
    {
        public override string Name => "Plastic Cup";
        public override string Description =>
            "Most disposable plastic cups are made with PET (polyethylene terephthalate) which can take 450 years or more to decompose and as its break down harmful microplastics are released into the environment. Plastic is frequently consumed by wildlife, killing 1.1 million marine creatures per year.";
        public override string DegradeTime => "~450 years";
        public override bool Recyclable => true;
        public override bool Compostable => false;
        public override bool IsBiodegradable => false;
        public override bool IsReusable => true;
        public override int PointValue => 30;
    }
}