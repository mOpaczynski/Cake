using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace CakeDemo.DataAccess
{
    public class CakeDemoContext : DbContext
    {
        public CakeDemoContext() : base("TestConnection")
        {
        }

        public DbSet<Vanilla> Vanillas { get; set; }

        public DbSet<Chocolate> Chocolates { get; set; }

        public DbSet<Cherry> Cherries { get; set; }
    }

    public class Vanilla
    {
        [Key]
        public int SpecialId { get; set; }

        public string Orchid { get; set; }

        public string Harvest { get; set; }

    }

    public class Chocolate
    {
        [Key]
        public int SpecialId { get; set; }

        public string Type { get; set; }

        public string Cacao { get; set; }

    }

    public class Cherry
    {
        [Key]
        public int SpecialId { get; set; }

        public string Redness { get; set; }

        public string Tastiness { get; set; }

    }
}
