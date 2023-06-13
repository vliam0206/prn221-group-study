using Domain.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FluentAPIs.Groups;

public class AccountInGroupConfiguration : IEntityTypeConfiguration<AccountInGroup>
{
    public void Configure(EntityTypeBuilder<AccountInGroup> builder)
    {
        builder.ToTable(nameof(AccountInGroup));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
        // assign relationship
        builder.HasOne(x => x.Account).WithMany(a => a.AccountInGroups).HasForeignKey(x => x.AccountId);
        builder.HasOne(x => x.Group).WithMany(g => g.AccountInGroups).HasForeignKey(x => x.GroupId);
    }
}
