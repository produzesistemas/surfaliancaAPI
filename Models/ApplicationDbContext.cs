using Microsoft.AspNetCore.Identity;
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
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelDimensions);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.BoardModelColors);
            modelBuilder.Entity<BoardType>().HasKey(c => c.Id);

            modelBuilder.Entity<Bottom>().HasKey(c => c.Id);

            modelBuilder.Entity<Construction>().HasKey(c => c.Id);
            //modelBuilder.Entity<Construction>().HasMany(c => c.BoardModelConstructions)
            //    .WithOne(b => b.Construction)
            //    .HasForeignKey(c => c.ConstructionId);

            modelBuilder.Entity<Lamination>().HasKey(c => c.Id);
            //modelBuilder.Entity<Lamination>().HasMany(c => c.BoardModelLaminations)
            //    .WithOne(b => b.Lamination)
            //    .HasForeignKey(c => c.LaminationId);

            modelBuilder.Entity<Size>().HasKey(c => c.Id);

            modelBuilder.Entity<Tail>().HasKey(c => c.Id);

            modelBuilder.Entity<Width>().HasKey(c => c.Id);

            
            modelBuilder.Entity<Paint>().HasKey(c => c.Id);

            modelBuilder.Entity<Order>().HasKey(c => c.Id);
            modelBuilder.Entity<OrderProduct>().HasKey(c => c.Id);
            modelBuilder.Entity<OrderProductOrdered>().HasKey(c => c.Id);
            modelBuilder.Entity<OrderTracking>().HasKey(c => c.Id);

            modelBuilder.Entity<Order>().HasMany(c => c.OrderProduct);
            modelBuilder.Entity<Order>().HasMany(c => c.OrderProductOrdered);
            modelBuilder.Entity<Order>().HasMany(c => c.OrderTracking);
            modelBuilder.Entity<OrderProductOrdered>().HasOne(c => c.Finishing);
            modelBuilder.Entity<OrderProductOrdered>().HasOne(c => c.Paint);


            modelBuilder.Entity<Product>().HasKey(c => c.Id);
            modelBuilder.Entity<Product>().HasMany(c => c.OrderProducts)
                .WithOne(b => b.Product)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<StatusOrder>().HasMany(c => c.OrderTrackings)
                .WithOne(b => b.StatusOrder)
                .HasForeignKey(c => c.StatusOrderId);

            modelBuilder.Entity<StatusPaymentOrder>().HasMany(c => c.OrderTrackings)
                .WithOne(b => b.StatusPaymentOrder)
                .HasForeignKey(c => c.StatusPaymentOrderId);

            modelBuilder.Entity<Width>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Width).HasForeignKey(c => c.WidthId);
            modelBuilder.Entity<BoardModel>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.BoardModel).HasForeignKey(c => c.BoardModelId);
            modelBuilder.Entity<Tail>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Tail).HasForeignKey(c => c.TailId);
            modelBuilder.Entity<Size>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Size).HasForeignKey(c => c.SizeId);
            modelBuilder.Entity<Lamination>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Lamination).HasForeignKey(c => c.LaminationId);
            modelBuilder.Entity<Construction>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Construction).HasForeignKey(c => c.ConstructionId);
            modelBuilder.Entity<Bottom>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Bottom).HasForeignKey(c => c.BottomId);
            modelBuilder.Entity<BoardType>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.BoardType).HasForeignKey(c => c.BoardTypeId);

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
        public DbSet<FinSystemColor> FinSystemColor { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamImage> TeamImage { get; set; }
        public DbSet<BoardType> BoardType { get; set; }
        public DbSet<Construction> Construction { get; set; }
        public DbSet<Lamination> Lamination { get; set; }
        public DbSet<Tail> Tail { get; set; }
        public DbSet<Bottom> Bottom { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Width> Width { get; set; }
        public DbSet<Paint> Paint { get; set; }
        public DbSet<BoardModel> BoardModel { get; set; }
        public DbSet<BoardModelColors> BoardModelColors { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<TypeSale> TypeSale { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderEmail> OrderEmail { get; set; }
        public DbSet<OrderTracking> OrderTracking { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<OrderProductOrdered> OrderProductOrdered { get; set; }
        public DbSet<PaymentCondition> PaymentCondition { get; set; }

        public DbSet<Finishing> Finishing { get; set; }

    }
}
