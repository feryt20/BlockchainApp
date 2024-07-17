using BlockchainApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlockchainApp.Db
{
    public class BlockchainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Block> Blocks { get; set; }
        //public DbSet<SmartContract> SmartContracts { get; set; }

        public BlockchainContext(DbContextOptions<BlockchainContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Address)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Block)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BlockId)
                .IsRequired(false); // Make the foreign key optional

            modelBuilder.Entity<Block>()
                .HasMany(b => b.Transactions)
                .WithOne(t => t.Block);
        }
    }
}

//add-migration init1 -c BlockchainContext -o Migrations\DbMain
//update-database -Context BlockchainContext