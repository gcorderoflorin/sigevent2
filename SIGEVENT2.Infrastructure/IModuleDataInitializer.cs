using Microsoft.Extensions.DependencyInjection;

namespace SIGEVENT2.Infrastructure;

public interface IModuleDataInitializer
{
    Task EnsureInitialData(IServiceScope seviceScope);
}