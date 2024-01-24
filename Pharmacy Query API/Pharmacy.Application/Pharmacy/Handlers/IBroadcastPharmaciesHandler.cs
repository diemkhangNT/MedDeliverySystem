using Pharmacy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Pharmacy.Handlers
{
    public interface IBroadcastPharmaciesHandler
    {
        Task BroadcastPharmacies();
    }
}
