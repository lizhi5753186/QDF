using QDF.Exceptions;

namespace QDF.Session
{
    /// <summary>
    /// Extension methods for <see cref="IQdfSession"/>.
    /// </summary>
    public static class QdfSessionExtensions
    {
        /// <summary>
        /// 获得当前User Id
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static long GetUserId(this IQdfSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new QdfException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }
    }
}