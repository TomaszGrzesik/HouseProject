using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProject
{
    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot, string doorDescription) : base(name, hot)
        {
            this.DoorDescription = doorDescription;
        }

        public string DoorDescription { get; private set; }

        public Location DoorLoacation { get; set; }

        override public string Description {
            get
            {
               return base.Description + "You see now " + DoorDescription + "." ;
            }
        }
    }
}
