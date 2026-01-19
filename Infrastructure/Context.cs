using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Context: DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PharmacyStore> PharmacyStores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
              Initial Catalog=Pharmacy; Integrated Security=True;");
            optionsBuilder.LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging(); //loguj sve što radiš uz console writeline
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                   
                    bi.WithOwner().HasForeignKey(i => i.IdBill);
                    bi.HasKey(i => new { i.Rb, i.IdBill });
                    bi.Property(i => i.Rb).ValueGeneratedOnAdd();
                });


            //modelBuilder.Entity<BillItem>()
            //    .HasKey(bi => new { bi.IdBill, bi.Rb });

            //modelBuilder.Entity<BillItem>()
            //    .HasOne<Bill>()
            //    .WithMany()
            //    .HasForeignKey(bi => bi.IdBill);

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
