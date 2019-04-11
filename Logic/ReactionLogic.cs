using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Models;

namespace Logic
{
    class ReactionLogic
    {
        static ReactionContextSQL reactionRepo = new ReactionContextSQL();

        // hier komt de verbinding tussen de repos en de view. zie category repository
        public static void PostReaction(Reaction reaction)
        {
            reactionRepo.PostReaction(reaction);
        }

        public static List<Reaction> GetAllCommentsWithQuestionID(int id) => reactionRepo.GetAllCommentsWithQuestionID(id);


    }
}
