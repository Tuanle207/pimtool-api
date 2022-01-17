using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMTool.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Db.Configuration
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        private const string TableName = "PROJECT";
        
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.ToTable(TableName, DbContants.DbSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProjectNumber)
                .IsRequired()
                .IsFixedLength()
                .HasColumnType("smallint")
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(x => x.Customer)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnType("char")
                .IsFixedLength()
                .HasMaxLength(3);

            builder.Property(x => x.StartDate)
               .IsRequired()
               .HasColumnType("date");

            builder.Property(x => x.EndDate)
               .HasColumnType("date");

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.HasMany(x => x.Employees)
                .WithMany(x => x.Projects)
                .UsingEntity(x => x.ToTable("PROJECT_EMPLOYEE"));
        }
    }
}
