using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMTool.Services.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Db.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        private const string TableName = "EMPLOYEE";

        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable(TableName, DbContants.DbSchema);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Visa)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnType("char");

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            builder.Property(x => x.BirthDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.HasMany(x => x.LeadingGroups)
                .WithOne(x => x.Leader)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.LeaderId);
        }
    }
}
