using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGui
{
    public class RecieveInfo
    {
        public string result { get; set; }
        public bool connectionAlive { get; set; }
        public string typeCommand { get; set; }

        public RecieveInfo(string result, bool connectionAlive, string typeCommand)
        {
            this.result = result;
            this.connectionAlive = connectionAlive;
            this.typeCommand = typeCommand;
        }
    }
}
