using Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FluentAPIs.Posts;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(Comment));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
        // assign relationship
        builder.HasOne(x => x.Post).WithMany(p => p.Comments).HasForeignKey(x => x.PostId);
        builder.HasOne(x => x.AccountCreated).WithMany(a => a.Comments).HasForeignKey(x => x.AccountCreatedID).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.AccountReplied).WithMany(a => a.ReplyComments).HasForeignKey(x => x.AccountRepliedId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.CommentReplied).WithMany(c => c.ReplyComments).HasForeignKey(x => x.CommentRepliedId);
        builder.HasMany(x => x.ReplyComments).WithOne(c => c.CommentReplied).HasForeignKey(x => x.CommentRepliedId);
    }
}
