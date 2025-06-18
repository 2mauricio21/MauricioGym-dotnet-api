namespace MauricioGym.Infra.SQLServer.Interfaces
{
    public interface IDbContext<TConnection> : IDisposable
    {
        TConnection Connection { get; }
    }
}
