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

        /// <summary>
        /// Initializes a new instance of the <see cref="RecieveInfo"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="connectionAlive">if set to <c>true</c> [connection alive].</param>
        /// <param name="typeCommand">The type command.</param>
        public RecieveInfo(string result, bool connectionAlive, string typeCommand)
        {
            this.result = result;
            this.connectionAlive = connectionAlive;
            this.typeCommand = typeCommand;
        }
    }
}
