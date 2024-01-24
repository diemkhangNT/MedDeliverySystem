using Pharmacy.Application.Pharmacy.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Pharmacy.Handlers
{
    public interface IGetListPharmacyHandler
    {
        Task<List<PharmacyDto>> GetListPharmacyAsync();
    }
}
