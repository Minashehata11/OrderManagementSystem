using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Repository.Specefication
{
    public  class SpecefiationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> Inputquery, ISpecefication<TEntity> specs)
        {
            var query = Inputquery;
            if (specs.Criteria != null)
                query = query.Where(specs.Criteria);
           
            query = specs.Includes.Aggregate(query, (current, includeExprision) => current.Include(includeExprision));

            return query;
        }
    }
}
