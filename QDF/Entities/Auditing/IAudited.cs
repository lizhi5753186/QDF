namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 创建和更新审计跟踪接口
    /// </summary>
    public interface IAudited :ICreationAudited, IModificationAudited
    {
         
    }
}