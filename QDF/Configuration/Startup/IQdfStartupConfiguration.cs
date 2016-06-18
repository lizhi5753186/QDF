using QDF.Dependency;
using QDF.Events.Bus;

namespace QDF.Configuration.Startup
{
    /// <summary>
    /// Used to configure Bdf and mudules on startup.
    /// </summary>
    public interface IQdfStartupConfiguration : IDictionaryBasedConfig
    {
        /// <summary>
        /// Gets the IOC manager associated with this configuration.
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// Used to configure <see cref="IEventBus"/>.
        /// </summary>
        IEventBusConfiguration EventBus { get; }

        /// <summary>
        /// Used to configure caching.
        /// </summary>
        //ICachingConfiguration Caching { get; }

        /// <summary>
        /// Gets/sets default connection string used by ORM module.
        /// It can be name of a connection string in application's config file or can be full connection string.
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// Used to configure unit of work defaults.
        /// </summary>
        //IUnitOfWorkDefaultOptions UnitOfWork { get; }
    }
}