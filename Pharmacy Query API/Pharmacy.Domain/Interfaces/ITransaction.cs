using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Interfaces
{
    public interface ITransaction
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
