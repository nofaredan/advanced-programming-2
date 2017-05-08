using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public class Recive : EventArgs
    {
        public string type { get; set; }
        public string result { get; set; }

        public Recive(string type, string result)
        {
            this.type = type;
            this.result = result;
        }
    }
}
