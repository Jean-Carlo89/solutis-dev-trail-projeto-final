using BankSystem.API.model;
using Microsoft.EntityFrameworkCore;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {
    }

    public DbSet<BankAccountModel> Accounts { get; set; }

    public DbSet<ClientModel> Clients { get; set; }

    public DbSet<TransactionModel> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BankAccountModel>()
                    .HasKey(a => a.Id);

        modelBuilder.Entity<BankAccountModel>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Number).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Balance).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Status).IsRequired();

            entity.HasOne(c => c.Client)
                  .WithMany(a => a.Accounts)
                  .HasForeignKey(c => c.ClientId)
                 .OnDelete(DeleteBehavior.Restrict);


            entity.HasMany(a => a.Transactions)
                  .WithOne(t => t.SourceAccount)
                  .HasForeignKey(t => t.SourceAccountId)
                  .OnDelete(DeleteBehavior.Restrict);


            entity.HasMany(a => a.DestinationTransactions)
                  .WithOne(t => t.DestinationAccount)
                  .HasForeignKey(t => t.DestinationAccountId)
                  .OnDelete(DeleteBehavior.Restrict);

        });

        modelBuilder.Entity<ClientModel>((entity) =>
        {

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();


            entity.Property(c => c.Name).IsRequired().HasMaxLength(150);


            entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(c => c.Email).IsUnique();


            entity.Property(c => c.Cpf).IsRequired().HasMaxLength(11);
            entity.HasIndex(c => c.Cpf).IsUnique();


            entity.Property(c => c.DateOfBirth).IsRequired();
        });
        modelBuilder.Entity<TransactionModel>(entity =>
                {

                    entity.HasKey(t => t.Id);
                    entity.Property(t => t.Id).ValueGeneratedOnAdd();


                    entity.Property(t => t.Amount)
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();

                    entity.Property(t => t.Type)
                        .IsRequired();

                    entity.Property(t => t.Description)
                        .HasMaxLength(200)
                        .IsRequired();

                    entity.Property(t => t.CreatedAt)
                        .IsRequired();

                    entity.Property(t => t.SourceAccountId)
                        .IsRequired(false);

                    entity.Property(t => t.DestinationAccountId)
                        .IsRequired(false);


                    entity.HasOne(t => t.SourceAccount)
                        .WithMany(b => b.Transactions)
                        .HasForeignKey(t => t.SourceAccountId)
                        .OnDelete(DeleteBehavior.SetNull);


                    entity.HasOne(t => t.DestinationAccount)
                        .WithMany(b => b.DestinationTransactions)
                        .HasForeignKey(t => t.DestinationAccountId)
                        .OnDelete(DeleteBehavior.Restrict);


                    entity.HasIndex(t => t.SourceAccountId);
                    entity.HasIndex(t => t.DestinationAccountId);
                    entity.HasIndex(t => t.CreatedAt);
                    entity.HasIndex(t => t.Type);
                });
    }


}



