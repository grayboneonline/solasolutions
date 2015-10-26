namespace SOLA.Cache.Contracts
{
    public interface ICustomerDataSource
    {
        string SiteName { get; set; }
        string ServerName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}