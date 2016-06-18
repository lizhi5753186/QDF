namespace QDF.Entities.Auditing
{
    /// <summary>
    /// 所有跟踪接口
    /// </summary>
    public interface IFullAudited : IAudited, IDeletionAudited
    {
         
    }
}