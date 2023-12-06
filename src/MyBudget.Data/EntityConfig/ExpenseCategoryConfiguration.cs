using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudget.Application.Entities;

namespace MyBudget.Data.EntityConfig
{
    internal class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(60)
                .IsRequired();

            builder.HasMany(x => x.Expenses)
                .WithOne();

            builder.Navigation(x => x.Expenses).AutoInclude();
        }
    }
}
