using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Yahtzee.Models;

namespace Yahtzee.DAL.Mappers
{
    public class ChatMessageMapper
    {

        public static void MapRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>().HasRequired<Game>(g => g.Game).WithMany(c => c.ChatMessages).WillCascadeOnDelete();
        }
    }
}