using Application.Commons;
using Domain.Entities;
using Domain.Entities.Groups;
using Domain.Entities.Posts;
using Infrastructure.FluentAPIs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Infrastructure;

public class AppDBContext : DbContext
{
    #region Fields
    private readonly AppConfiguration _configuration;
    #endregion

    public AppDBContext(DbContextOptions options, AppConfiguration configuration) : base(options)
    {
        _configuration = configuration;
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
            optionsbuilder.UseSqlServer(_configuration.ConnectionStrings.DefaultDB);
        }
    }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);            
    }
}
