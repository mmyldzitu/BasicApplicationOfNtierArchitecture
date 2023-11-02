using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtierArchitecture.Entities.Concrete;

namespace TodoAppNtierArchitecture.DataAccess.Configurations
{
    class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Definition).HasMaxLength(300);
            builder.Property(x => x.Definition).IsRequired();

            builder.Property(x => x.isCompleted).IsRequired(true);
        }
    }
}
