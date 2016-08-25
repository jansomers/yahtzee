using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Yahtzee.Models;

namespace Yahtzee.DAL.Mappers
{
    public static class GameMapper
    {
        public static void MapRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOptional(gr => gr.GameResultA)
                .WithMany()
                .HasForeignKey(gr => gr.GameResultAId);

            modelBuilder.Entity<Game>()
                .HasOptional(gr => gr.GameResultB)
                .WithMany()
                .HasForeignKey(gr => gr.GameResultBId);

            

        }

        public static void MapPropConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(g => g.GameName).HasMaxLength(20);
            
        }
    }
}