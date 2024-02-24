namespace TrashMetadata
{
    public class ToiletPaper : ITrashMetadata
    {
        public override string Name => "Toilet Paper";
        public override string Description =>
            "Since toilet paper is made from wood pulp and fibers it is biodegradable and will break down fairly quickly. However the environmental hazard posed by toilet paper is in its creation. Toilet paper manufacturers cut down 27,000 trees daily. Toilet paper from recycled materials can ease this problem.";
        public override string DegradeTime => "2 weeks to 1 month";
        public override bool Recyclable => false;
        public override bool Compostable => true; //if not used to clean anything unsavory
        public override bool IsBiodegradable => true;
        public override bool IsReusable => false;
        public override int PointValue => 5;
    }
}