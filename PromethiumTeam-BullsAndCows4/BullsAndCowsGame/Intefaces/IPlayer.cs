using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsGame.Intefaces
{
    public interface IPlayer : IComparable<IPlayer>
    {
        string Name { get; set; }
        int Attempts { get; set; }
    }
}
