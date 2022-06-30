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
            modelBuilder.Entity<Coupon>().HasKey(c => c.Id);
            modelBuilder.Entity<Bottom>().HasKey(c => c.Id);
            modelBuilder.Entity<Blog>().HasKey(c => c.Id);
            modelBuilder.Entity<TypeBlog>().HasKey(c => c.Id);
            modelBuilder.Entity<Blog>().HasOne(c => c.TypeBlog);
            modelBuilder.Entity<TypeBlog>().HasMany(c => c.Blogs).WithOne(b => b.TypeBlog).HasForeignKey(c => c.TypeBlogId);

            modelBuilder.Entity<Team>().HasKey(c => c.Id);
            modelBuilder.Entity<TeamImage>().HasKey(c => c.Id);
            modelBuilder.Entity<TeamImage>().HasOne(c => c.Team);
            modelBuilder.Entity<Team>().HasMany(c => c.teamImages).WithOne(b => b.Team).HasForeignKey(c => c.TeamId);



            modelBuilder.Entity<Construction>().HasKey(c => c.Id);



            //modelBuilder.Entity<Construction>().HasMany(c => c.BoardModelConstructions)
            //    .WithOne(b => b.Construction)
            //    .HasForeignKey(c => c.ConstructionId);

            modelBuilder.Entity<Lamination>().HasKey(c => c.Id);
            modelBuilder.Entity<State>().HasKey(c => c.Id);
            //modelBuilder.Entity<Lamination>().HasMany(c => c.BoardModelLaminations)
            //    .WithOne(b => b.Lamination)
            //    .HasForeignKey(c => c.LaminationId);

            modelBuilder.Entity<Tail>().HasKey(c => c.Id);
           
            modelBuilder.Entity<Paint>().HasKey(c => c.Id);
            modelBuilder.Entity<Stringer>().HasKey(c => c.Id);
            modelBuilder.Entity<Order>().HasKey(c => c.Id);
            modelBuilder.Entity<OrderProduct>().HasKey(c => c.Id);
            modelBuilder.Entity<OrderProductOrdered>().HasKey(c => c.Id);
            modelBuilder.Entity<OrderTracking>().HasKey(c => c.Id);

            modelBuilder.Entity<Order>().HasMany(c => c.OrderProduct);
            modelBuilder.Entity<Order>().HasMany(c => c.OrderProductOrdered);
            modelBuilder.Entity<Order>().HasMany(c => c.OrderTracking);
            //modelBuilder.Entity<OrderProductOrdered>().HasOne(c => c.Finishing);
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

            modelBuilder.Entity<BoardModel>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.BoardModel).HasForeignKey(c => c.BoardModelId);
            modelBuilder.Entity<Tail>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Tail).HasForeignKey(c => c.TailId);
            modelBuilder.Entity<Lamination>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Lamination).HasForeignKey(c => c.LaminationId);
            modelBuilder.Entity<Construction>().HasMany(c => c.OrderProductOrdereds).WithOne(b => b.Construction).HasForeignKey(c => c.ConstructionId);
            modelBuilder.Entity<Coupon>().HasMany(c => c.Orders).WithOne(b => b.Coupon).HasForeignKey(c => c.CouponId);

            modelBuilder.Entity<ShippingCompany>().HasKey(c => c.Id);
            modelBuilder.Entity<ShippingCompanyState>().HasKey(c => c.Id);
            modelBuilder.Entity<ShippingCompany>().HasMany(c => c.ShippingCompanyStates)
            .WithOne(b => b.ShippingCompany)
            .HasForeignKey(c => c.ShippingCompanyId);
            modelBuilder.Entity<ShippingCompanyState>().HasOne(c => c.ShippingCompany);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<TypeEmail> TypeEmail { get; set; }
        public DbSet<StatusPaymentOrder> StatusPaymentOrder { get; set; }
        public DbSet<StatusOrder> StatusOrder { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<FinSystem> FinSystem { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamImage> TeamImage { get; set; }
        public DbSet<Construction> Construction { get; set; }
        public DbSet<ShippingCompany> ShippingCompany { get; set; }
        public DbSet<ShippingCompanyState> ShippingCompanyState { get; set; }
        public DbSet<Lamination> Lamination { get; set; }
        public DbSet<Tail> Tail { get; set; }
        public DbSet<TailReinforcement> TailReinforcement { get; set; }
        public DbSet<Paint> Paint { get; set; }
        public DbSet<Stringer> Stringer { get; set; }
        public DbSet<BoardModel> BoardModel { get; set; }
        public DbSet<BoardModelDimensions> BoardModelDimensions { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<TypeSale> TypeSale { get; set; }
        public DbSet<ProductStatus> ProductStatus { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderEmail> OrderEmail { get; set; }
        public DbSet<OrderTracking> OrderTracking { get; set; }
        public DbSet<OrderEvaluation> OrderEvaluation { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<OrderProductOrdered> OrderProductOrdered { get; set; }
        public DbSet<PaymentCondition> PaymentCondition { get; set; }
        public DbSet<Finishing> Finishing { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<TypeBlog> TypeBlog { get; set; }
        public DbSet<Bottom> Bottom { get; set; }

    }
}
