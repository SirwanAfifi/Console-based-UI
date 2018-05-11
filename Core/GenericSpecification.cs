using System;
using System.Linq.Expressions;

namespace TestingGuiCS.Core
{
    public class GenericSpecification<T>
    {
        public Expression<Func<T, bool>> Expression;

        public GenericSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisifiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}