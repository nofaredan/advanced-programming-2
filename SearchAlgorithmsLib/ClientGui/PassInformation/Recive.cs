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

        /// <summary>
        /// Initializes a new instance of the <see cref="Recive"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="result">The result.</param>
        public Recive(string type, string result)
        {
            this.type = type;
            this.result = result;
        }
    }
}
