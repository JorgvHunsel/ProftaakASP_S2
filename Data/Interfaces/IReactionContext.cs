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
        //hier komen de methodes van reactioncontextsql
        void PostReaction(Reaction reaction);
        List<Reaction> GetAllCommentsWithQuestionID(int id);
    }
}
