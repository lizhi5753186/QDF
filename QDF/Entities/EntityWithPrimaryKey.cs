using System;

namespace QDF.Entities
{
    [Serializable]
    public abstract class EntityWithPrimaryKey<TPrimaryKey> : IEntityWithPrimaryKey<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }

        #region Object Members
        public override bool Equals(object obj)
        {
            if (!(obj is EntityWithPrimaryKey<TPrimaryKey>))
            {
                return false;
            }

            // Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (EntityWithPrimaryKey<TPrimaryKey>)obj;

            // Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityWithPrimaryKey<TPrimaryKey> left, EntityWithPrimaryKey<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(EntityWithPrimaryKey<TPrimaryKey> left, EntityWithPrimaryKey<TPrimaryKey> right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}]", GetType().Name, Id);
        }
        #endregion 
    }
}