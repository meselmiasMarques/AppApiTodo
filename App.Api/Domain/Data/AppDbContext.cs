using Microsoft.EntityFrameworkCore;

namespace App.Api.Domain.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=dbtask;Integrated Security=True;Encrypt=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category"); // Define o nome da tabela
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)")
                    .HasMaxLength(100);
            });
            #endregion 

            #region Todo
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("Todo"); // Define o nome da tabela
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)")
                    .HasMaxLength(100);
                entity.Property(e => e.Description)
                    .HasColumnType("VARCHAR(500)")
                    .HasMaxLength(500);
                entity.Property(e => e.Status)
                    .IsRequired();
                entity.HasOne(e => e.Category) // Relacionamento com Category
                    .WithMany()
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Todos) // Adicionando navegação inversa
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User"); // Define o nome da tabela
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Name)
                    .HasColumnType("VARCHAR(100)")
                    .IsRequired();
                entity.Property(e => e.Password)
                    .HasColumnType("VARCHAR(50)")
                    .IsRequired();
                entity.Property(e => e.Role)
                    .HasColumnType("VARCHAR(50)");
                entity.HasMany(u => u.Todos)
                    .WithOne(t => t.User)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // Adicionando DeleteBehavior
            });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
