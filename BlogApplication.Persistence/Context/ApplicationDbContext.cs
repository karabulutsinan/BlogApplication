using BlogApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Persistence.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies();
			base.OnConfiguring(optionsBuilder);
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<PostTag> PostTags { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Configurations.ConfigureUser(modelBuilder);
			Configurations.ConfigureCategory(modelBuilder);
			Configurations.ConfigurePost(modelBuilder);
			Configurations.ConfigureTag(modelBuilder);
			Configurations.ConfigurePostTag(modelBuilder);
			Configurations.ConfigureComment(modelBuilder);
		}


	}
}
