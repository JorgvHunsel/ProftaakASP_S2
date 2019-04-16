using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;
using Data.Interfaces;
using Models;

namespace Logic
{
    class ReactionLogic
    {
        private readonly IReactionContext _reaction;

        public ReactionLogic(IReactionContext reaction)
        {
            _reaction = reaction;
        }

        // hier komt de verbinding tussen de repos en de view. zie category repository
        public void PostReaction(Reaction reaction)
        {
            _reaction.PostReaction(reaction);
        }

        public List<Reaction> GetAllCommentsWithQuestionID(int id) => _reaction.GetAllCommentsWithQuestionID(id);


    }
}
