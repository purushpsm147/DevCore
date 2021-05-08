using Microsoft.Extensions.Logging;

namespace SGRE.TSA.ExternalServices
{
    public interface IExternalServiceFactory
    {
        IExternalService<T> CreateExternalService<T>(ILogger logger) where T : class;
    }
}
