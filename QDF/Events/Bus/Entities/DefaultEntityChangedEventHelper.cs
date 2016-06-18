﻿using System;
using QDF.Dependency;

namespace QDF.Events.Bus.Entities
{
    /// <summary>
    /// Used to trigger entity change events.
    /// </summary>
    public class DefaultEntityChangedEventHelper : ITransientDependency, IEntityChangedEventHelper
    {
        public IEventBus EventBus { get; set; }

       // private readonly IUnitOfWorkManager _unitOfWorkManager;

        //public DefaultEntityChangedEventHelper(IUnitOfWorkManager unitOfWorkManager)
        //{
        //    _unitOfWorkManager = unitOfWorkManager;
        //    EventBus = NullEventBus.Instance;
        //}

        public void TriggerEntityCreatedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityCreatedEventData<>), entity);
        }

        public void TriggerEntityUpdatedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityUpdatedEventData<>), entity);
        }

        public void TriggerEntityDeletedEvent(object entity)
        {
            TriggerEntityChangeEvent(typeof(EntityDeletedEventData<>), entity);
        }

        private void TriggerEntityChangeEvent(Type genericEventType, object entity)
        {
            var entityType = entity.GetType();
            var eventType = genericEventType.MakeGenericType(entityType);

            //if (_unitOfWorkManager == null || _unitOfWorkManager.Current == null)
            //{
            //    EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, new[] { entity }));
            //    return;
            //}

            //_unitOfWorkManager.Current.Completed += (sender, args) => EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, new[] { entity }));
        }
    }
}