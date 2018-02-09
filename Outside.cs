using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProject
{
    class Outside : Location
    {
        public Outside(string name, bool hot) : base(name)
        {
            this.hot = hot;
        }

        private bool hot;

        public override string Description
        {
            get
            {
                string NewDesription = base.Description;
                if (hot)
                    NewDesription += "Here is very hot !";
                return NewDesription; 
            }
        }


    }
}
