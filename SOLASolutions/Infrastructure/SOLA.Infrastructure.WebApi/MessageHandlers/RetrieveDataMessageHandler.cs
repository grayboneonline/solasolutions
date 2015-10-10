using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SOLA.Infrastructure.WebApi.MessageHandlers
{
    public class RetrieveDataMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var site = request.RequestUri.Host.Split('.')[0];
            request.Properties.Add(WebApiContants.RequestKeyCustomerSite, site);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
