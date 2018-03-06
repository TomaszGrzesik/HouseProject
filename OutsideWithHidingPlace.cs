namespace HouseProject
{
    class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public OutsideWithHidingPlace(string name, bool hot, string hidingPlace) : base(name, hot)
        {
            HidingPlace = hidingPlace;
        }

        public string HidingPlace { get; private set; }

        public override string Description
        {
            get
            {
                return base.Description + "Someone is hiding by the " + HidingPlace + ".";
            }
        }
    }
}
