using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fontys.PTS2.Prototype.Classes;
using Fontys.PTS2.Prototype.Data;

namespace Fontys.PTS2.Prototype.Logic
{
    class ReactionLogic
    {
        static ReactionRepository reactionRepo = new ReactionRepository();

        // hier komt de verbinding tussen de repos en de view. zie category repository
        public static void PostReaction(Reaction reaction)
        {
            reactionRepo.PostReaction(reaction);
        }

        public static List<Reaction> GetAllCommentsWithQuestionID(int id) => reactionRepo.GetAllCommentsWithQuestionID(id);


        public static List<ListViewItem> LoadReactionsToList(List<Reaction> reactionList)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            foreach (Reaction reaction in reactionList)
            {
                //Standard adding item, only add the primary key as main item
                ListViewItem item = new ListViewItem(reaction.VolunteerName);
                item.SubItems.Add(reaction.Description);
                item.SubItems.Add(reaction.TimeStamp.ToString());
                item.SubItems.Add(reaction.QuestionId.ToString());
                item.SubItems.Add(reaction.ReactionId.ToString());
                item.SubItems.Add(reaction.SenderId.ToString());
                items.Add(item);
                
            }
            return items;
        }
    }
}
