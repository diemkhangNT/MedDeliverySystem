using MedicineOrder.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.Interfaces
{
    public interface IBaseRepository<T>
        where T : class
    {
        Task InsertItem(T entity);
        void UpdateItem(T entity);
        void DeleteItem(T entity);
        Task<IEnumerable<T>> GetAllOfItems(ExpressionModel<T> expressionModel, bool? isTrackingForUpdate = true);
        Task<T> GetSingleItem(ExpressionModel<T> expressionModel, bool? isTrackingForUpdate = true);
    }
}
