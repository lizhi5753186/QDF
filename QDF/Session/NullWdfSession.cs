namespace QDF.Session
{
    public class NullQdfSession : IQdfSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullQdfSession Instance { get { return SingletonInstance; } }
        private static readonly NullQdfSession SingletonInstance = new NullQdfSession();

        public int? UserId { get { return null; } }


        private NullQdfSession()
        {
        }
    }
}