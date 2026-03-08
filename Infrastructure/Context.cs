using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PharmacyStore> PharmacyStores { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=Pharmacy;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medicine>()
                .HasKey(m => m.IdMedicine);

            modelBuilder.Entity<Patient>()
                .HasKey(p => p.IdPatient);

            modelBuilder.Entity<PharmacyStore>()
                .HasKey(ps => ps.IdPharmacy);

            modelBuilder.Entity<Bill>()
                .HasKey(b => b.IdBill);

            modelBuilder.Entity<Bill>()
                 .OwnsMany(b => b.BillItems, bi =>
                  {
                      bi.ToTable("BillItems");
                      bi.WithOwner().HasForeignKey(i => i.IdBill);

                  bi.HasKey(i => new { i.IdBill, i.Rb });

                   bi.Property(i => i.Rb)
                   .ValueGeneratedNever();
                    });


            modelBuilder.Entity<Bill>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(b => b.IdPatient);

            modelBuilder.Entity<Bill>()
                .HasOne<PharmacyStore>()
                .WithMany()
                .HasForeignKey(b => b.IdPharmacy);
        }
    }
}
