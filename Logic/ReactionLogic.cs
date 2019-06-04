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
    public class ReactionLogic
    {
        private readonly IReactionContext _reaction;

        public ReactionLogic(IReactionContext reaction)
        {
            _reaction = reaction;
        }

        public void PostReaction(Reaction reaction)
        {
            _reaction.PostReaction(reaction);
        }

        public List<Reaction> GetAllCommentsWithQuestionId(int id) => _reaction.GetAllReactions(id);
    }
}
