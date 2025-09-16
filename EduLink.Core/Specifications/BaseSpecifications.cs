using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; set; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }

        //Get All Constructor
        public BaseSpecifications()
        {
            
        }
        //Get ById Constructor
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }


    }
}
