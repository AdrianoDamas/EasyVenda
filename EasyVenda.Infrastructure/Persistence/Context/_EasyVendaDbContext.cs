using _EasyVenda.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace _EasyVenda.Infrastructure.Persistence.Context
{
    public partial class _EasyVendaDbContext : DbContext
    {
        public _EasyVendaDbContext()
        {
        }

        public _EasyVendaDbContext(DbContextOptions<_EasyVendaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Venda> Vendas { get; set; }
        public virtual DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venda>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Venda>()
                .HasMany(v => v.Itens)
                .WithOne(i => i.Venda)
                .HasForeignKey(i => i.VendaId)
                .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<ItemVenda>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<ItemVenda>()
                .Property(i => i.ValorTotal)
                .HasComputedColumnSql("[Quantidade] * ([ValorUnitario] - [Desconto])");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Venda>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.ValorTotal = entry.Entity.Itens
                        .Where(i => !i.Cancelado) // Ignora itens cancelados
                        .Sum(i => i.ValorTotal);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
