using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface ISearcher<T>
    {
        Solution<T> Search(ISearchable<T> searchable);
    }
}
