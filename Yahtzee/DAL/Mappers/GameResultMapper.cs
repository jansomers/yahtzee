using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Yahtzee.Models;

namespace Yahtzee.DAL.Mappers
{
    public class GameResultMapper : ComplexTypeConfiguration<GameResult>
    {
        public static void MapRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameResult>().HasRequired(g => g.Game).WithMany().HasForeignKey(g => g.GameId);
        }
    }
}