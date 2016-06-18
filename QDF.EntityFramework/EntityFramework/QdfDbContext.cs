using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using QDF.Configuration.Startup;
using QDF.Entities;
using QDF.Entities.Auditing;
using QDF.Events.Bus.Entities;
using QDF.Extensions;
using QDF.Session;
using QDF.Timing;

namespace QDF.EntityFramework.EntityFramework
{
    /// <summary>
    /// Base class for all DbContext classes in the application.
    /// </summary>
    public abstract class QdfDbContext : DbContext
    {
        /// <summary>
        /// Used to get current session values.
        /// </summary>
        public IQdfSession QdfSession { get; set; }

        /// <summary>
        /// Used to trigger entity change events.
        /// </summary>
        public IEntityChangedEventHelper EntityChangedEventHelper { get; set; }

        /// <summary>
        /// Reference to the logger.
        /// </summary>
        //public ILogger Logger { get; set; }

        /// <summary>
        /// Constructor.
        /// Uses <see cref="IQdfStartupConfiguration.DefaultNameOrConnectionString"/> as connection string.
        /// </summary>
        protected QdfDbContext()
        {
            //Logger = NullLogger.Instance;
            QdfSession = NullQdfSession.Instance;
            EntityChangedEventHelper = NullEntityChangedEventHelper.Instance;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected QdfDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //Logger = NullLogger.Instance;
            QdfSession = NullQdfSession.Instance;
            EntityChangedEventHelper = NullEntityChangedEventHelper.Instance;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected QdfDbContext(DbCompiledModel model)
            : base(model)
        {
            //Logger = NullLogger.Instance;
            QdfSession = NullQdfSession.Instance;
            EntityChangedEventHelper = NullEntityChangedEventHelper.Instance;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected QdfDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            //Logger = NullLogger.Instance;
            QdfSession = NullQdfSession.Instance;
            EntityChangedEventHelper = NullEntityChangedEventHelper.Instance;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected QdfDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
            //Logger = NullLogger.Instance;
            QdfSession = NullQdfSession.Instance;
            EntityChangedEventHelper = NullEntityChangedEventHelper.Instance;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected QdfDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            //Logger = NullLogger.Instance;
            QdfSession = NullQdfSession.Instance;
            EntityChangedEventHelper = NullEntityChangedEventHelper.Instance;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected QdfDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            //Logger = NullLogger.Instance;
            QdfSession = NullQdfSession.Instance;
            EntityChangedEventHelper = NullEntityChangedEventHelper.Instance;
        }

        public virtual void Initialize()
        {
            Database.Initialize(false);
        }

        public override int SaveChanges()
        {
            try
            {
                ApplyQdfConcepts();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                ApplyQdfConcepts();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }

        protected virtual void ApplyQdfConcepts()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreationAuditProperties(entry);
                        EntityChangedEventHelper.TriggerEntityCreatedEvent(entry.Entity);
                        break;
                    case EntityState.Modified:
                        SetModificationAuditProperties(entry);

                        if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
                        {
                            if (entry.Entity is IDeletionAudited)
                            {
                                SetDeletionAuditProperties(entry.Entity.As<IDeletionAudited>());
                            }

                            EntityChangedEventHelper.TriggerEntityDeletedEvent(entry.Entity);
                        }
                        else
                        {
                            EntityChangedEventHelper.TriggerEntityUpdatedEvent(entry.Entity);
                        }
                        break;
                    case EntityState.Deleted:
                        HandleSoftDelete(entry);
                        EntityChangedEventHelper.TriggerEntityDeletedEvent(entry.Entity);
                        break;
                }
            }
        }

        protected virtual void SetCreationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IHasCreationTime)
            {
                entry.Cast<IHasCreationTime>().Entity.CreateTime = ClockManager.Now;
            }

            if (entry.Entity is ICreationAudited)
            {
                entry.Cast<ICreationAudited>().Entity.CreateUserId = QdfSession.UserId;
            }
        }

        protected virtual void SetModificationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IModificationAudited)
            {
                var auditedEntry = entry.Cast<IModificationAudited>();

                auditedEntry.Entity.UpdateTime = ClockManager.Now;
                auditedEntry.Entity.UpdateUserId = QdfSession.UserId;
            }
        }

        protected virtual void HandleSoftDelete(DbEntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            var softDeleteEntry = entry.Cast<ISoftDelete>();

            softDeleteEntry.State = EntityState.Unchanged;
            softDeleteEntry.Entity.IsDeleted = true;

            if (entry.Entity is IDeletionAudited)
            {
                SetDeletionAuditProperties(entry.Cast<IDeletionAudited>().Entity);
            }
        }

        protected virtual void SetDeletionAuditProperties(IDeletionAudited entity)
        {
            entity.DeleteTime = ClockManager.Now;
            entity.DeleteUserId = QdfSession.UserId;
        }

        private void LogDbEntityValidationException(DbEntityValidationException exception)
        {
            //Logger.Error("There are some validation errors while saving changes in EntityFramework:");
            //foreach (var ve in exception.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
            //{
            //    Logger.Error(" - " + ve.PropertyName + ": " + ve.ErrorMessage);
            //}
        }
    }
}