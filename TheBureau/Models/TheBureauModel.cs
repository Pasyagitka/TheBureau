using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TheBureau.Models
{
    public partial class TheBureauModel : DbContext
    {
        public TheBureauModel()
            : base("name=TheBureauConnection")
        {
        }

        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Brigade> Brigades { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestEquipment> RequestEquipments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Сlient> Сlient { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessory>()
                .Property(e => e.equipmentId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Accessory>()
                .Property(e => e.price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brigade>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Brigade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.contactNumber)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.id)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Accessories)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.RequestEquipments)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.RequestEquipments)
                .WithRequired(e => e.Request)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestEquipment>()
                .Property(e => e.equipmentId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role1)
                .HasForeignKey(e => e.role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Сlient>()
                .Property(e => e.contactNumber)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Сlient>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Сlient)
                .HasForeignKey(e => e.clientId)
                .WillCascadeOnDelete(false);
        }
    }
}
