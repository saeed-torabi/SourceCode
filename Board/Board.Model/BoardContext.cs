namespace Board.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class BoardContext : DbContext, IContext
    {
        public BoardContext()
            : base("name=BoardContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.Database.CommandTimeout = 120;
        }

        public virtual DbSet<AdImage> AdImages { get; set; }
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>()
                .HasMany(e => e.AdImages)
                .WithRequired(e => e.Ad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Ads)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Ads)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);
        }


        public Task<int> SaveChangesAsync(int? userId)
        {
            InitSaveChanges(userId);
            return base.SaveChangesAsync();
        }

        public int SaveChanges(int? userId)
        {
            InitSaveChanges(userId);
            return base.SaveChanges();

        }

        private void InitSaveChanges(int? userId)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditable
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));


            //int? userId = null;

            //if (!string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name))
            //if (HttpContext.Current != null)
            //{                
            //    userId = (int?)HttpContext.Current.Session["UserId"];

            //}

            foreach (var entry in modifiedEntries)
            {
                IAuditable entity = entry.Entity as IAuditable;
                if (entity != null)
                {
                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = userId;
                        entity.CreateDate = DateTime.Now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreateDate).IsModified = false;
                        entity.UpdatedBy = userId;
                        entity.UpdateDate = DateTime.Now;
                    }
                }
            }
        }
    }
}
