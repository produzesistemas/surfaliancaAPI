﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardModel>().HasKey(c => c.Id);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelBoardTypes);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelBottoms);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelConstructions);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelLaminations);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelShapers);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelSizes);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelTails);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelWidths);
            modelBuilder.Entity<BoardType>().HasKey(c => c.Id);
            modelBuilder.Entity<BoardModelBoardType>().HasKey(c => c.Id);
            modelBuilder.Entity<BoardModelBoardType>().HasOne(c => c.BoardType);

            modelBuilder.Entity<Bottom>().HasKey(c => c.Id);
            modelBuilder.Entity<Bottom>().HasMany(c => c.BoardModelBottoms)
                .WithOne(b => b.Bottom)
                .HasForeignKey(c => c.BottomId);

            modelBuilder.Entity<Construction>().HasKey(c => c.Id);
            modelBuilder.Entity<Construction>().HasMany(c => c.BoardModelConstructions)
                .WithOne(b => b.Construction)
                .HasForeignKey(c => c.ConstructionId);

            modelBuilder.Entity<Lamination>().HasKey(c => c.Id);
            modelBuilder.Entity<Lamination>().HasMany(c => c.BoardModelLaminations)
                .WithOne(b => b.Lamination)
                .HasForeignKey(c => c.LaminationId);

            modelBuilder.Entity<Shaper>().HasKey(c => c.Id);
            modelBuilder.Entity<Shaper>().HasMany(c => c.BoardModelShapers)
                .WithOne(b => b.Shaper)
                .HasForeignKey(c => c.ShaperId);

            modelBuilder.Entity<Size>().HasKey(c => c.Id);
            modelBuilder.Entity<Size>().HasMany(c => c.BoardModelSizes)
                .WithOne(b => b.Size)
                .HasForeignKey(c => c.SizeId);

            modelBuilder.Entity<Tail>().HasKey(c => c.Id);
            modelBuilder.Entity<Tail>().HasMany(c => c.BoardModelTails)
                .WithOne(b => b.Tail)
                .HasForeignKey(c => c.TailId);

            modelBuilder.Entity<Width>().HasKey(c => c.Id);
            modelBuilder.Entity<Width>().HasMany(c => c.BoardModelWidths)
                .WithOne(b => b.Width)
                .HasForeignKey(c => c.WidthId);

            modelBuilder.Entity<Product>().HasKey(c => c.Id);
            modelBuilder.Entity<Paint>().HasKey(c => c.Id);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<TypeEmail> TypeEmail { get; set; }
        public DbSet<StatusPaymentOrder> StatusPaymentOrder { get; set; }
        public DbSet<StatusOrder> StatusOrder { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<FinSystem> FinSystem { get; set; }
        public DbSet<FinColor> FinColor { get; set; }
        public DbSet<FinSystemColor> FinSystemColor { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamImage> TeamImage { get; set; }
        public DbSet<BoardType> BoardType { get; set; }
        public DbSet<Shaper> Shaper { get; set; }
        public DbSet<Construction> Construction { get; set; }
        public DbSet<Lamination> Lamination { get; set; }
        public DbSet<Tail> Tail { get; set; }
        public DbSet<Bottom> Bottom { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Width> Width { get; set; }
        public DbSet<Paint> Paint { get; set; }
        public DbSet<BoardModel> BoardModel { get; set; }
        public DbSet<BoardModelBoardType> BoardModelBoardType { get; set; }
        public DbSet<BoardModelShaper> BoardModelShaper { get; set; }
        public DbSet<BoardModelBottom> BoardModelBottom { get; set; }
        public DbSet<BoardModelConstruction> BoardModelConstruction { get; set; }
        public DbSet<BoardModelLamination> BoardModelLamination { get; set; }
        public DbSet<BoardModelSize> BoardModelSize { get; set; }
        public DbSet<BoardModelTail> BoardModelTail { get; set; }
        public DbSet<BoardModelWidth> BoardModelWidth { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<TypeSale> TypeSale { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }

    }
}
