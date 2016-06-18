namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 创建对象跟踪
    /// </summary>
    public interface ICreationAudited : IHasCreationTime
    {
        /// <summary>
        /// 创建用户Id
        /// </summary>
        int? CreateUserId { get; set; }
    }
}