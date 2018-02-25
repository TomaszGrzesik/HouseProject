namespace HouseProject
{
    class Room : Location
    {
        private string decoration;

        public Room(string name, string decoration) : base(name)
        {
            this.decoration = decoration;
        }

        public override string Description
        {
            get
            {
                return base.Description + "You can see here " + decoration +".";                
            }
        }
    }
}

