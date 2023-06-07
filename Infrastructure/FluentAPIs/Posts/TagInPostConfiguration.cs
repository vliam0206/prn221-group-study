using Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FluentAPIs.Posts;

public class TagInPostConfiguration : IEntityTypeConfiguration<TagInPost>
{
    public void Configure(EntityTypeBuilder<TagInPost> builder)
    {
        builder.ToTable(nameof(TagInPost));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
        // assign relationship
        builder.HasOne(x => x.Tag).WithMany(t => t.TagInPosts)
            .HasForeignKey(x => x.TagID)
            .OnDelete(DeleteBehavior.Restrict); // Add this line to change the delete behavior;
        builder.HasOne(x => x.Post).WithMany(p => p.TagInPosts).HasForeignKey(x => x.PostId);
    }
}
