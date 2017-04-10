using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityExample.Context
{
    public class ExampleDbContext : IdentityDbContext<ApplicationUser>
    {

        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {

        }
    }
}
