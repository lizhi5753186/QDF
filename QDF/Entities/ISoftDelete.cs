namespace QDF.Entities
{
    /// <summary>
    /// 假删除接口
    /// 如果实体支持假删除的话，就必须实现该接口
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}