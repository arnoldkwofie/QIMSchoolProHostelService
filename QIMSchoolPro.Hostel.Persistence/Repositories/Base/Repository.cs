using Qface.Application.Shared.Common.Models;
using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Persistence.Interfaces.Base;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Qface.Application.Shared.Common.Extensions;



namespace QIMSchoolPro.Hostel.Persistence.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : AuditableEntityBase
        {
            private DbSet<T> _entities;
            //private readonly IIdentityService _identityService;

            public Repository(HostelDbContext context)
            {
                Context = context;
                //_identityService = identityService;
            }

            public virtual IQueryable<T> Table => Entities;
            public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();
            protected readonly HostelDbContext Context;

            public virtual DbSet<T> Entities
            {
                get
                {
                    if (_entities == null)
                        _entities = Context.Set<T>();
                    return _entities;
                }
            }

            public virtual IQueryable<T> GetBaseQuery()
            {
                return Entities;
            }

            public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
            {
                IQueryable<T> queryable = Entities;
                foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                {
                    queryable = queryable.Include(includeProperty);
                }

                return queryable;
            }
            public async Task<T> GetAsync(int id)
            {
                var keyProperty = Context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];
                return await GetBaseQuery().FirstOrDefaultAsync(e => EF.Property<int>(e, keyProperty.Name) == id);
            }

            public async Task<List<T>> GetAllAsync()
            {
                return await GetBaseQuery().ToListAsync();
            }

            public async Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, CancellationToken cancellationToken)
            {


                var whereQuerable = GetBaseQuery()
                                .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

                var pagedModel = await whereQuerable.PageBy(x => paginated.Take, paginated)
                                  .ToListAsync(cancellationToken);

                var totalRecords = await whereQuerable.CountAsync(cancellationToken: cancellationToken);


                return new PaginatedList<T>(data: pagedModel,
                                                  totalCount: totalRecords,
                                                  currentPage: paginated.PageNumber,
                                                  pageSize: paginated.PageSize);
            }


            public async Task<PaginatedList<T>> GetPage(PaginatedCommand paginated, IQueryable<T> query, CancellationToken cancellationToken)
            {
                try
                {

                    var whereQuerable = query
                                    .WhereIf(!string.IsNullOrEmpty(paginated.Search), GetSearchCondition(paginated.Search));

                    var pagedModel = await whereQuerable.PageBy(x => paginated.Take, paginated)
                                      .ToListAsync(cancellationToken);

                    var totalRecords = await whereQuerable.CountAsync(cancellationToken: cancellationToken);


                    return new PaginatedList<T>(data: pagedModel,
                                                      totalCount: totalRecords,
                                                      currentPage: paginated.PageNumber,
                                                      pageSize: paginated.PageSize);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


            public virtual Expression<Func<T, bool>> GetSearchCondition(string search)
            {
                return x => x.Audit.CreatedBy.Contains(search);
            }

            public virtual Expression<Func<T, bool>> GetSearchCondition(T item, string search)
            {
                return x => x.Audit.CreatedBy.Contains(search);
            }

            public async Task InsertAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    await Entities.AddAsync(entity, cancellationToken);
                    if (autoCommit)
                        await Context.SaveChangesAsync(cancellationToken);
                    Context.Entry(entity).State = EntityState.Detached;
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }


            public async Task<int> InsertReturnIdAsync(T entity, bool autoCommit = true)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    await Entities.AddAsync(entity);
                    if (autoCommit)
                        await Context.SaveChangesAsync();

                    // Get the value of the ID property after the entity has been added to the context
                    var idProperty = entity.GetType().GetProperty("Id");
                    int insertedId = (int)idProperty.GetValue(entity);

                    return insertedId;

                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }


            public async Task InsertAsync(T entity, bool autoCommit = true)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    await Entities.AddAsync(entity);
                    if (autoCommit)
                        await Context.SaveChangesAsync();
                    Context.Entry(entity).State = EntityState.Detached;

                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }


            public async Task InsertAsync(IEnumerable<T> entities, bool autoCommit = true)
            {
                try
                {
                    if (entities == null)
                        throw new ArgumentNullException("entities");

                    foreach (var entity in entities)
                        await Entities.AddAsync(entity);
                    if (autoCommit)
                        await Context.SaveChangesAsync();
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }

            public async Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true)
            {
                try
                {
                    if (entities == null)
                        throw new ArgumentNullException("entities");

                    await Entities.AddRangeAsync(entities, cancellationToken);
                    if (autoCommit)
                        await Context.SaveChangesAsync();
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }

            public async Task UpdateAsync(T entity, bool autoCommit = true)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    Entities.Update(entity);
                    if (autoCommit)
                        await Context.SaveChangesAsync();
                    Context.Entry(entity).State = EntityState.Detached;
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }
            public async Task UpdateAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    Entities.Update(entity);
                    if (autoCommit)
                        await Context.SaveChangesAsync(cancellationToken);
                    Context.Entry(entity).State = EntityState.Detached;
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }

            public async Task DeleteAsync(T entity, CancellationToken cancellationToken, bool autoCommit = true)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");

                    Entities.Remove(entity);
                    if (autoCommit)
                        await Context.SaveChangesAsync(cancellationToken);
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }
            public async Task DeleteAsync(T entity, bool autoCommit = true)
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity");
                    Entities.Remove(entity);
                    if (autoCommit)
                        await Context.SaveChangesAsync();
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }

            public async Task SoftDeleteAsync(T entity, bool autoCommit = true)
            {
                //var userId = _identityService.GetUserId();
                var userId = "akwofie1@umat.edu.gh";
                entity.Audit.SoftRemove(userId);
                await UpdateAsync(entity, autoCommit);

            }
            public async Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken, bool autoCommit = true)
            {
                try
                {
                    if (entities == null)
                        throw new ArgumentNullException("entities");

                    Entities.RemoveRange(entities);
                    if (autoCommit)

                        await Context.SaveChangesAsync(cancellationToken);
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }
            public async Task DeleteAsync(IEnumerable<T> entities, bool autoCommit = true)
            {
                try
                {
                    if (entities == null)
                        throw new ArgumentNullException("entities");

                    Entities.RemoveRange(entities);
                    if (autoCommit)
                        await Context.SaveChangesAsync();
                }
                catch (DbEntityValidationException dbEx)
                {
                    var msg = string.Empty;

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                        foreach (var validationError in validationErrors.ValidationErrors)
                            msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                    var fail = new Exception(msg, dbEx);
                    //Debug.WriteLine(fail.Message, fail);
                    throw fail;
                }
                catch (Exception ex) { throw ex; }
            }
            public async Task CommitAsync(CancellationToken cancellationToken)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
            public async Task CommitAsync()
            {
                await Context.SaveChangesAsync();
            }

     }

    
}
