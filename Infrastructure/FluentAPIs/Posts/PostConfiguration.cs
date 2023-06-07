using Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.Posts;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable(nameof(Post));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
        // assign relationship
        builder.HasOne(x => x.Group).WithMany(g => g.Posts).HasForeignKey(x => x.GroupId);
        builder.HasOne(x => x.AccountCreated).WithMany(a => a.Posts).HasForeignKey(x => x.AccountCreatedID);
        builder.HasMany(x => x.Attachments).WithOne(a => a.Post).HasForeignKey(a => a.PostId);
        builder.HasMany(x => x.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId);
        builder.HasMany(x => x.Likes).WithOne(l => l.Post).HasForeignKey(l => l.PostId);
        builder.HasMany(x => x.TagInPosts).WithOne(t => t.Post).HasForeignKey(t => t.PostId);
    }
}
