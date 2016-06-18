using System;

namespace QDF.Entities
{
    /// <summary>
    /// 主键是int的实体抽象类
    /// </summary>
    [Serializable]
    public abstract class Entity : EntityWithPrimaryKey<int>, IEntity
    {
    }
}