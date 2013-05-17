using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsGame.Intefaces
{
    public interface IScoreBoard<T> : IEnumerable<T>, IEnumerator<T> where T : IComparable<T>
    {
        int Count { get; }
        void Add(T item);
    }
}
