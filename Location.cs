namespace HouseProject
{
    abstract class Location
    {
        public Location(string name)
        {
           Name = name;
        }

        public string Name { get; set; }

        public Location[] Exits;
        
        public virtual string Description
        {
            get
            {
                string description = "Location: " + Name + ". Exterior doors: ";
                for (int i = 0; i < Exits.Length; i++)
                {
                    description += Exits[i].Name;
                    if (i != Exits.Length - 1)
                        description = ",";
                }
                description += ",";
                return description;
            }
        }       
    }
}
