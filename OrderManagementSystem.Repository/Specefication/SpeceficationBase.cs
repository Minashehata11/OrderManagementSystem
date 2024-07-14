using System.Linq.Expressions;

namespace OrderManagementSystem.Repository.Specefication
{
    public class SpeceficationBase<T> : ISpecefication<T>
    {
        public SpeceficationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();


        protected void AddInclude(Expression<Func<T, object>> IncludeExpression)
           => Includes.Add(IncludeExpression);


    }
}
