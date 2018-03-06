namespace HouseProject
{
    class RoomWithHidingPlace : Room, IHidingPlace
    {
        public RoomWithHidingPlace(string name, string decoration, string hidingPlace) : base(name, decoration)
        {
            HidingPlace = hidingPlace;
        }

        public string HidingPlace { get; private set; }
        public override string Description
        {
            get
            {
                return base.Description + " Someone is hiding by the" + HidingPlace + ".";
            }
        }
    }
}
