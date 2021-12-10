using HerkansingBackEndAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace HerkansingBackEndAPI.Data
{
    public class CursusContext : DbContext
    {
        public CursusContext(DbContextOptions<CursusContext> options) : base(options)
        {
        }

        public DbSet<Cursus> Cursussen { get; set; }
        public DbSet<CursusInstantie> CursusInstanties { get; set; }

    }
}
