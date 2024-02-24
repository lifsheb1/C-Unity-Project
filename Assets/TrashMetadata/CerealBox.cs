namespace TrashMetadata
{
    public class CerealBox : ITrashMetadata
    {
        public override string Name => "Cereal Boxes";
        public override string Description =>
            "Cardboard ceral boxes typically take around 2 months to decompose naturally but can be quicker if composted properly. However the plastic lining that stores the cereal can take hundreds to thousands of years to decompose"; 
        public override string DegradeTime => "2 months to 1,000 years";
        //For these metrics not sure if should use the box itself or plastic lining
        public override bool Recyclable => true;
        public override bool Compostable => true;
        public override bool IsBiodegradable => true;
        public override bool IsReusable => true;
        public override int PointValue => 5;
    }
}