using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudget.Application.Entities;

namespace MyBudget.Data.EntityConfig
{
    internal class ExpenseEntryConfiguration : IEntityTypeConfiguration<ExpenseEntry>
    {
        public void Configure(EntityTypeBuilder<ExpenseEntry> builder)
        {
            builder.Property(x => x.CreationDate).IsRequired();
            
            builder.Property(x => x.Amount)
                .HasPrecision(2)
                .IsRequired();

            builder.HasOne(x => x.Expense);

            builder.Navigation(x => x.Expense).AutoInclude();
        }
    }
}
