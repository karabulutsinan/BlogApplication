using BlogApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Persistence.Context
{
    public static class Configurations
    {
        public static void ConfigureCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>()
                .Property(c => c.Name).HasDefaultValue(true);
        }

        public static void ConfigurePost(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasKey(p => p.Id);

            modelBuilder.Entity<Post>()
                .Property(p => p.Title).HasDefaultValue(true);

            modelBuilder.Entity<Post>().HasOne(x => x.Category).WithMany(x => x.Posts).HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<Post>().HasMany(X => X.Tags).WithMany(x => x.Posts).UsingEntity<PostTag>();

            modelBuilder.Entity<Post>().HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>().HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.Restrict);
        }

        public static void ConfigureTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasKey(x => x.Id);
        }

        public static void ConfigurePostTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId, x.TagId });
        }

        public static void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasMany(x => x.Comments).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }

        public static void ConfigureComment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasKey(x => x.Id);
            modelBuilder.Entity<Comment>().HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
