using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2312590_NNTDan_Lab07.Models
{
    public class RestaurantContext : DbContext
    {
        static RestaurantContext()
        {
            // Use existing database without altering schema (keep data)
            Database.SetInitializer<RestaurantContext>(null);
        }

        public RestaurantContext() : base("RestaurantContext")
        {
        }

        public DbSet<Category> Categories
        {
            get; set;
        }
        public DbSet<Food> Foods
        {
            get; set;
        }
        public DbSet<Role> Roles
        {
            get; set;
        }
        public DbSet<Account> Accounts
        {
            get; set;
        }
        public DbSet<Bill> Bills
        {
            get; set;
        }
        public DbSet<BillDetail> BillDetails
        {
            get; set;
        }
        public DbSet<DiningTable> Tables
        {
            get; set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names to match existing DB
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            // Map CLR types to existing tables
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Bill>().ToTable("Bills");
            modelBuilder.Entity<BillDetail>().ToTable("BillDetails");
            modelBuilder.Entity<DiningTable>().ToTable("Tables");

            // Column mappings
            modelBuilder.Entity<Food>()
                .Property(f => f.FoodCategoryId)
                .HasColumnName("FoodCategoryID");

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .HasColumnName("RoleName");

            modelBuilder.Entity<Bill>()
                .Property(b => b.TableId)
                .HasColumnName("TableID");

            modelBuilder.Entity<Bill>()
                .Property(b => b.StaffId)
                .HasColumnName("StaffID");

            modelBuilder.Entity<BillDetail>()
                .Property(d => d.BillId)
                .HasColumnName("BillID");

            modelBuilder.Entity<BillDetail>()
                .Property(d => d.FoodId)
                .HasColumnName("FoodID");

            // Relationships
            modelBuilder.Entity<Food>()
                .HasRequired<Category>(f => f.Category)
                .WithMany()
                .HasForeignKey(f => f.FoodCategoryId)
                .WillCascadeOnDelete(true);

            // Account - Role many-to-many via AccountRoles
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Roles)
                .WithMany(r => r.Accounts)
                .Map(m =>
                {
                    m.ToTable("AccountRoles");
                    m.MapLeftKey("AccountID");
                    m.MapRightKey("RoleID");
                });

            // Bills relationships
            modelBuilder.Entity<Bill>()
                .HasRequired(b => b.Table)
                .WithMany()
                .HasForeignKey(b => b.TableId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bill>()
                .HasOptional(b => b.Staff)
                .WithMany()
                .HasForeignKey(b => b.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BillDetail>()
                .HasRequired(d => d.Bill)
                .WithMany(b => b.Details)
                .HasForeignKey(d => d.BillId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<BillDetail>()
                .HasRequired(d => d.Food)
                .WithMany()
                .HasForeignKey(d => d.FoodId)
                .WillCascadeOnDelete(false);
        }

    }
}
