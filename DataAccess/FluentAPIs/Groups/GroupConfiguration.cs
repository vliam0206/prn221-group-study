using Domain.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FluentAPIs.Groups;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(nameof(Group));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.CreationDate).HasDefaultValueSql("getutcdate()");
        // assign relationship
        builder.HasMany(x => x.AccountInGroups).WithOne(a => a.Group).HasForeignKey(a => a.GroupId);
        builder.HasMany(x => x.Posts).WithOne(p => p.Group).HasForeignKey(p => p.GroupId);
        builder.HasMany(x => x.Tags).WithOne(t => t.Group).HasForeignKey(t => t.GroupId);
    }
}
