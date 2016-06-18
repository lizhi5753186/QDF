namespace QDF.Entities
{
    /// <summary>
    /// 实体接口
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntityWithPrimaryKey<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}