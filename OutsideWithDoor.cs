namespace HouseProject
{
    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot, string doorDescription) : base(name, hot)
        {
            this.DoorDescription = doorDescription;
        }

        public string DoorDescription { get; private set; }

        public Location DoorLocation { get; set; }

        override public string Description {
            get
            {
               return base.Description + "You see now " + DoorDescription + "." ;
            }
        }
    }
}
