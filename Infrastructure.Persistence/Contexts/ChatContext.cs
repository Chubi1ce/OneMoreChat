using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Persistence.Contexts
{
    public class ChatContext:DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Username=postgres;Password=example;Database=AppDB");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigurateUsers(builder);
            ConfigurateMessages(builder);
        }

        private static void ConfigurateMessages(ModelBuilder builder)
        {
            builder.Entity<MessageEntity>().HasKey(x => x.Id);
            builder.Entity<MessageEntity>().Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Entity<MessageEntity>().HasOne<UserEntity>().WithMany().HasForeignKey(x => x.SenderId);
            builder.Entity<MessageEntity>().HasOne<UserEntity>().WithMany().HasForeignKey(x => x.RecipientId);
        }

        private static void ConfigurateUsers(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().HasKey(x => x.Id);
            builder.Entity<UserEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<UserEntity>().HasIndex(x => x.Name).IsUnique();

        }
    }
}
