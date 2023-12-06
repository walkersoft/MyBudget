using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudget.Application.Entities;

namespace MyBudget.Data.EntityConfig
{
    internal class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(x => x.ExpenseType).IsRequired();
            
            builder.Property(x => x.Source)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasPrecision(2);

            builder.HasOne<ExpenseCategory>()
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
