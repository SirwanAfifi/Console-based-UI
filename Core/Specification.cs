using System;
using System.Linq.Expressions;
using TestingGuiCS.Models;

namespace TestingGuiCS.Core
{
    public abstract class Specification<T>
    {
        public bool IsSatisifiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }

    public sealed class MovieForKidsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating <= MpaaRating.PG;
        }
    }
    
    public sealed class AvailableOnCdSpecification : Specification<Movie>
    {
        private const int MonthBeforeDvdIsOut = 6;
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.ReleaseDate <= DateTime.Now.AddMonths(-MonthBeforeDvdIsOut);
        }
    }
}