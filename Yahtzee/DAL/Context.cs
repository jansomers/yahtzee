using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Yahtzee.DAL.Mappers;
using Yahtzee.Models;

namespace Yahtzee.DAL
{
    public class Context : IdentityDbContext<User>
    {
        
        public DbSet<Game> Games { get; set; }
       

        public DbSet<GameResult> GameResults { get; set; }
        public Context() : base("name=DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        public static Context Create()
        {
            return new Context();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UserMapper.MapRelations(modelBuilder);
            GameMapper.MapRelations(modelBuilder);
            GameMapper.MapPropConfig(modelBuilder);
            GameResultMapper.MapRelations(modelBuilder);
            ChatMessageMapper.MapRelations(modelBuilder);



        }

    }
}