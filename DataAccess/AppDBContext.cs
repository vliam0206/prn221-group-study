using Domain.Entities;
using Domain.Entities.Groups;
using Domain.Entities.Posts;
using DataAccess.FluentAPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataAccess;

public class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }
    public AppDBContext(DbContextOptions options) : base(options)
    {
    }    

    #region DB Sets
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountInGroup> AccountInGroups { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagInPost> TagInPosts { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    #endregion

    #region Connect DB
    protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
    {
        if (!optionsbuilder.IsConfigured)
        {
            optionsbuilder.UseSqlServer(GetConnectionStrings());
        }
    }
    private string GetConnectionStrings()
    {
        IConfiguration config = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
        return config["ConnectionStrings:DefaultDB"];
    }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);            
    }
    public override int SaveChanges()
    {
        // Get the added entities of AccountInGroup table
        var addedEntities = ChangeTracker.Entries<AccountInGroup>()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity);

        foreach (var addedEntity in addedEntities)
        {
            // Find the corresponding Group entity
            var group = Groups.Find(addedEntity.GroupId);

            if (group != null)
            {
                // Increment the NumOfMember property
                group.NumOfMember++;
            }
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Get the added entities of AccountInGroup table
        var addedEntities = ChangeTracker.Entries<AccountInGroup>()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity);

        foreach (var addedEntity in addedEntities)
        {
            // Find the corresponding Group entity
            var group = Groups.Find(addedEntity.GroupId);

            if (group != null)
            {
                // Increment the NumOfMember property
                group.NumOfMember++;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
