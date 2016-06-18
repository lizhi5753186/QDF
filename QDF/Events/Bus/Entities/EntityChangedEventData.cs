﻿using System;

namespace QDF.Events.Bus.Entities
{
    /// <summary>
    /// Used to pass data for an event that is related to with a changed <see cref="IEntity"/> object.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    [Serializable]
    public class EntityChangedEventData<TEntity> : EntityEventData<TEntity>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="entity">Changed entity in the event</param>
        public EntityChangedEventData(TEntity entity) 
            : base(entity)
        {

        }
    }
}