using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FluentAPIs;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(nameof(Account));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();
        // assign relationship
        builder.HasMany(x => x.AccountInGroups).WithOne(a => a.Account).HasForeignKey(a => a.AccountId);
        //builder.HasMany(x => x.Posts).WithOne(p => p.AccountCreated).HasForeignKey(p => p.AccountCreatedID).OnDelete(DeleteBehavior.Restrict);
        //builder.HasMany(x => x.Comments).WithOne(c => c.AccountCreated).HasForeignKey(c => c.AccountCreatedID).OnDelete(DeleteBehavior.Restrict);
        // Configure the first relationship: Account -> Comments
        builder.HasMany(a => a.Comments)
            .WithOne(c => c.AccountCreated)
            .HasForeignKey(c => c.AccountCreatedID)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure the second relationship: Account -> ReplyComments
        builder.HasMany(a => a.ReplyComments)
            .WithOne(c => c.AccountReplied)
            .HasForeignKey(c => c.AccountRepliedId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.ReplyComments).WithOne(c => c.AccountReplied).HasForeignKey(c => c.AccountRepliedId);
        builder.HasMany(x => x.Likes).WithOne(l => l.AccountCreated).HasForeignKey(l => l.AccountCreatedID);
        builder.HasMany(x => x.Notifications).WithOne(n => n.AccountRecieved).HasForeignKey(n => n.AccountRecievedId);
    }
}
