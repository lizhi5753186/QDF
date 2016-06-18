using System;

namespace QDF.Exceptions
{
    /// <summary>
    /// Qdf 框架的异常类
    /// </summary>
    [Serializable]
    public class QdfException : Exception
    {
        #region Ctor
        public QdfException()
        {

        }

        public QdfException(string message)
            : base(message)
        {

        }

        public QdfException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        #endregion
    }
}