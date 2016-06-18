using QDF.Dependency;

namespace QDF.Configuration.Startup
{
    public class QdfStartupConfiguration : DictionayBasedConfig, IQdfStartupConfiguration
    {
        public IIocManager IocManager { get; private set; }

        public IEventBusConfiguration EventBus { get; private set; }
       
        public string DefaultNameOrConnectionString { get;set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocManager"></param>
        public QdfStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Initialize()
        {
            EventBus = IocManager.Resolve<IEventBusConfiguration>();
        }
    }
}