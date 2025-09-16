using EduLink.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EduLink.Repository.Repositories
{
    public static class SpecificationEvalutor<T>where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> specification)
        {
            var Query = inputQuery;
            if (specification.Criteria != null)
            {
                Query = Query.Where(specification.Criteria);
            }

            //Check if OrderBy is not null and then apply it
            if (specification.OrderBy != null)
            {
                Query = Query.OrderBy(specification.OrderBy);
            }
            Query
               = specification.Includes.Aggregate(Query, (current, include) => current.Include(include));
            //Apply advanced includes
            Query = specification.IncludeExpressions
           .Aggregate(Query, (current, include) => include(current));
            return Query;
        }
    }
}
