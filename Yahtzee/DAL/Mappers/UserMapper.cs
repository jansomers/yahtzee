using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Yahtzee.Models;

namespace Yahtzee.DAL.Mappers
{
    public static class UserMapper
    {
        
        

        public static void MapRelations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(m => m.Games).WithRequired(m => m.PlayerA).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>()
                .HasMany(m => m.Invitations)
                .WithRequired(m => m.PlayerB)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<User>()
                .HasMany(x => x.Friends)
                .WithMany().Map(x => x.ToTable("User_Friends"));
        }
    }
}