using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientGui
{
    class ViewSolveMode : IUpdateCommand
    {
        /// <summary>
        /// Executes the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="current">The current.</param>
        public void Execute(string result, Window current)
        {
            ((SinglePlayerWindow)(current)).SolveMaze(result);
        }
    }
}
