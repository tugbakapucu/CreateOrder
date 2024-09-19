namespace Domain
{
    public interface IDbContextHandler
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
