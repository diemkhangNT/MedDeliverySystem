using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Pharmacy.Application.Pharmacy.Dtos;
using Pharmacy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pharmacy.Application.Pharmacy.Handlers
{
    public class GetListPharmacyHandler : IGetListPharmacyHandler
    {
        private readonly IPharmacyRepository _pharmacyRepository;

        public GetListPharmacyHandler(IPharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        public async Task<List<PharmacyDto>> GetListPharmacyAsync()
        {
            var listPharmacies = await _pharmacyRepository.GetAllOfPharmacies(new(pharmacy => !pharmacy.IsDeleted));
            var response = listPharmacies.Select(pharmacy => new PharmacyDto(pharmacy)).ToList();
            return response;
        }
    }
}
