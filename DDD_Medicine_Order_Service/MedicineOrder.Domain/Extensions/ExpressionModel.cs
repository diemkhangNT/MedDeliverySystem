using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Extensions
{
    public class ExpressionModel<TSourse>
    {
        public ExpressionStarter<TSourse> RootExpression { get; private set; }

        public ExpressionModel(Expression<Func<TSourse, bool>> rootExpression)
        => RootExpression = PredicateBuilder.New(rootExpression);

        //compare...
        public void CombineExpressionsWithOperatorAnd(Expression<Func<TSourse, bool>> newPre)
        => RootExpression = RootExpression.And(newPre);

        public Expression<Func<TSourse, bool>> CombineExpressionsWithOperatorOr(Expression<Func<TSourse, bool>> newPre)
        => RootExpression = RootExpression.Or(newPre);
    }
}
