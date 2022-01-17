using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMTool.Services.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Db.Configuration
{
    internal class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
    {
        private const string TableName = "GROUP";

        public void Configure(EntityTypeBuilder<GroupEntity> builder)
        {
            builder.ToTable(TableName, DbContants.DbSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
