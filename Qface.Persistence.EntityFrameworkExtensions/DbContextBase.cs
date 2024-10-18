

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;
using Qface.Domain.Shared.ValueObjects;

namespace Qface.Persistence.EntityFrameworkExtensions
{
    public abstract class DbContextBase : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public DbContextBase(DbContextOptions options)
            : base(options)
        {
        }
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public int EnableIdentityInsert<T>() => SetIdentityInsert<T>(enable: true);
        public int DisableIdentityInsert<T>() => SetIdentityInsert<T>(enable: false);
        public async Task EnableIdentityInsertAsync<T>() => await SetIdentityInsertAsync<T>(true);
        public async Task DisableIdentityInsertAsync<T>() => await SetIdentityInsertAsync<T>(false);

        private async Task SetIdentityInsertAsync<T>(bool enable)
        {
            var entityType = Model.FindEntityType(typeof(T));
            var value = enable ? "ON" : "OFF";
            //await Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
        }
        private int SetIdentityInsert<T>(bool enable)
        {
            var entityType = Model.FindEntityType(typeof(T));
            var value = enable ? "ON" : "OFF";
            //return Database.ExecuteSqlRaw(
            //    $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
            return 1;
        }

        protected void SetAuditableEntity(string userId)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntityBase>())
            {
                var audit = new Audit
                {
                    CreatedBy = userId,
                    Created = SystemClock.Now,
                    EntityStatus = EntityStatus.Normal,
                    EntityStatusCreated = SystemClock.Now,
                    EntityStatusCreateBy = userId,
                    LastModifiedBy = userId,
                    LastModified = SystemClock.Now,
                    EntityStatusLastModified = SystemClock.Now,
                    EntityStatusLastModifiedBy = userId,


                };
                entry.Entity.Audit = entry.Entity.Audit ?? audit;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Audit = audit;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Audit.LastModifiedBy = userId;
                        entry.Entity.Audit.LastModified = SystemClock.Now;
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        entry.Entity.Audit.EntityStatus = EntityStatus.Normal;
                        entry.Entity.Audit.EntityStatusCreated = SystemClock.Now;
                        entry.Entity.Audit.EntityStatusCreateBy = userId;
                        break;
                }

            }
        }


        //public async Task<IDbContextTransaction> BeginTransactionAsync()
        //{
        //    if (_currentTransaction != null) return null;
        //    _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        //    return _currentTransaction;
        //}

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
