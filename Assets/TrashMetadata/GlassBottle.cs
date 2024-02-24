namespace TrashMetadata
{
    public class GlassBottle : ITrashMetadata
    {
        public override string Name => "Glass Bottle";
        public override string Description =>
            "Despite being made from natural materials like silica, glass is not biodegradable and can remain in the environment in some form for thousands and thousands of years. However, glass does not release harmful chemicals or materials somewhat lessening its environmental impact. Also, glass is infinitely recyclable.";
        public override string DegradeTime => "4,000+ years";
        public override bool Recyclable => true;
        public override bool Compostable => false;
        public override bool IsBiodegradable => false;
        public override bool IsReusable => true;
        public override int PointValue => 10;
    }
}