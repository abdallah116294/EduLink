using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Core.Specifications
{
    public interface ISpecifications<T>where T : class
    {
        //Criteria for filtering
        public Expression<Func<T, bool>> Criteria { get; set; }
        //Includes for related entities
        public List<Expression<Func<T, object>>> Includes { get; set; }
        List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; set; }
        //OrderBy for sorting
        public Expression<Func<T, object>> OrderBy { get; set; }
    }
}
