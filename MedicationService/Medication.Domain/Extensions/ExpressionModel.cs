using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Domain.Extensions
{
    public class ExpressionModel<TSource>
    {
        public ExpressionStarter<TSource> RootExpression { get; private set; }

        public ExpressionModel(Expression<Func<TSource, bool>> rootExpression)
            => RootExpression = PredicateBuilder.New(rootExpression);

        public void CombineExpressionWithOperatorAnd(Expression<Func<TSource, bool>> newPre)
            => RootExpression = RootExpression.And(newPre);

        public void CombineExpressionWithOperatorOr(Expression<Func<TSource, bool>> newPre)
            => RootExpression = RootExpression.Or(newPre);

    }
}
