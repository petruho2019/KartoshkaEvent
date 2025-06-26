namespace KartoshkaEvent.Application.Contracts.Interfaces
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
