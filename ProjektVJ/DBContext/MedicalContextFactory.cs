using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Medical.DBContext
{
    public class MedicalContextFactory
        : IDesignTimeDbContextFactory<MedicalContext>
    {
        public MedicalContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<MedicalContext>()
               .UseNpgsql("Host=localhost;Port=5432;Database=medical;Username=postgres;Password=password;")


                .Options;

            return new MedicalContext(options);
        }
    }
}
