namespace SOLA.DataAccess.Base
{
    public interface IConfig
    {
        string ConnectionString { get; set; }
        string ProviderName { get; set; }
    }
}
