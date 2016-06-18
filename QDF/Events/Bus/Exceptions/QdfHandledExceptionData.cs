using System;

namespace QDF.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by Bdf infrastructure.
    /// </summary>
    public class QdfHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
         public QdfHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}