using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Data.Interfaces
{
    public interface IReactionContext
    {
        void PostReaction(Reaction reaction);
        List<Reaction> GetAllReactions(int questionId);
    }
}
