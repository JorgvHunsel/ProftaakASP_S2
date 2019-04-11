using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fontys.PTS2.Prototype.Classes;

namespace Fontys.PTS2.Prototype.Data.Contexts
{
    public interface IReactionContext
    {
        //hier komen de methodes van reactioncontextsql
        void PostReaction(Reaction reaction);
        List<Reaction> GetAllCommentsWithQuestionID(int id);
    }
}
