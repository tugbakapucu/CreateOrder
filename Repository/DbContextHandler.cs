using Domain;

namespace Repository
{
    public class DbContextHandler : IDbContextHandler
    {
        private readonly CaseAktifDbContext _dbContext;

        public DbContextHandler(CaseAktifDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
