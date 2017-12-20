using Microsoft.EntityFrameworkCore;

namespace CFKC.VPV.Entities
{
    public partial class CodeForKcDbContext : DbContext
    {
        public virtual DbSet<Parcel> Parcel { get; set; }
        public virtual DbSet<Geometry> Geometry { get; set; }
        public virtual DbSet<Park> Park { get; set; }


        public CodeForKcDbContext(DbContextOptions options) : base(options) { }

        public CodeForKcDbContext(string connectionString) : base(BuildFromConnectionString(connectionString)) { }

        private static DbContextOptions BuildFromConnectionString(string connectionString) =>
            new DbContextOptionsBuilder()
            .UseSqlServer(connectionString, opt => opt.UseRowNumberForPaging())
            .Options;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkProximity>();

            modelBuilder.Entity<MatrixAnswers>();

            modelBuilder.Entity<Geometry>(entity =>
            {
                entity.Property(e => e.FormattedAddress).HasMaxLength(40);

                entity.HasOne(d => d.Parcel)
                    .WithOne(p => p.Geometry)
                    .HasForeignKey<Geometry>(d => d.ParcelId);
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.HasIndex(e => e.GeometryId)
                    .HasName("IX_Parcel");

                entity.HasIndex(e => new { e.FormattedAddress, e.ParcelId })
                    .HasName("ui_ukAddr")
                    .IsUnique();

                entity.Property(e => e.Apn)
                    .HasColumnName("APN")
                    .HasMaxLength(50);

                entity.Property(e => e.FormattedAddress).HasMaxLength(50);

                entity.Property(e => e.Fraction).HasMaxLength(50);

                entity.Property(e => e.OwnerCity).HasMaxLength(50);

                entity.Property(e => e.OwnerFormattedAddress).HasMaxLength(50);

                entity.Property(e => e.OwnerName).HasMaxLength(80);

                entity.Property(e => e.OwnerPostalCode).HasMaxLength(50);

                entity.Property(e => e.OwnerState).HasMaxLength(50);

                entity.Property(e => e.Zone).HasMaxLength(50);
            });

            modelBuilder.Entity<Park>(entity =>
            {
                entity.Property(e => e.BenefitDistrict).HasMaxLength(10);

                entity.Property(e => e.BookComment).HasMaxLength(300);

                entity.Property(e => e.BookName).HasMaxLength(100);

                entity.Property(e => e.Council).HasMaxLength(20);

                entity.Property(e => e.InKcmo).HasColumnName("InKCMO");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Region).HasMaxLength(20);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);
            });


        }
        
    }
}
