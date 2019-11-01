using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG2.Model
{
    public class contact
    {
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Ma
        {
            get
            {
                return Name.Substring(0, 1).ToUpper();
            }
        }
    }
}
