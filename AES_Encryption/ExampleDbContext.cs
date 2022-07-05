namespace AES_Encryption
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.DataEncryption;
    using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
    public class ExampleDbContext : DbContext
    {
        private readonly byte[] _encryptionKey = new byte[16];
        private readonly byte[] _encryptionIV = new byte[16];
        private readonly IEncryptionProvider _provider;


        public ExampleDbContext()
        {
            this._provider = new AesProvider(_encryptionKey, _encryptionIV);
        }
        public DbSet<User>  Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-O2CP5S2\\SQLEXPRESS;Database=Encryption Store;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.ID).HasName("PK_User_Id");
                entity.Property(p => p.ID).HasColumnType("int").IsRequired().ValueGeneratedOnAdd();

            });
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
