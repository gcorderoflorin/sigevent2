namespace SIGEVENT2.Data;

public interface IModuleDataInitializer {
    Task EnsureInitialData (IServiceScope seviceScope);
}