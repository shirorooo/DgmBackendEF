using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DgmBackendEF.Data.Models
{
    public class PersonContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionstring;
        public PersonContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetConnectionString("default");
        }
        public DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionstring);
        }
    }
}
