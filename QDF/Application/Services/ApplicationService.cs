using QDF.Session;

namespace QDF.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for applicaion services
    /// </summary>
    public abstract class ApplicationService : QdfServiceBase, IApplicationService
    {
        /// <summary>
        /// Gets current session information.
        /// </summary>
        public IQdfSession QdfSession { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApplicationService()
        {
            QdfSession = NullQdfSession.Instance;
        }
    }
}