using Microsoft.EntityFrameworkCore;
using System;

namespace URLShortener.Entity
{
    public class ShortnerURlContext:DbContext
    {
        public ShortnerURlContext(){}
        public ShortnerURlContext(DbContextOptions<ShortnerURlContext> options) : base(options){}
       public  DbSet<URLShortenerEntity> URLShorteners { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseSqlServer(@"workstation id=testShortenURL.mssql.somee.com;packet size=4096;user id=MichaelMMM_SQLLogin_1;pwd=a6zgr8ew77;data source=testShortenURL.mssql.somee.com;persist security info=False;initial catalog=testShortenURL;TrustServerCertificate=True");
            base.OnConfiguring(OptionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   modelBuilder.Entity<URLShortenerEntity>().Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            modelBuilder.Entity<URLShortenerEntity>().HasIndex(e => e.ShortURl).IsUnique();

        }
    }
}
